﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mogym.Application.Interfaces;
using Mogym.Application.Interfaces.ILog;
using Mogym.Application.Records.User;
using Mogym.Application.Records.UserRole;
using Mogym.Common;
using Mogym.Common.ModelExtended;
using Mogym.Domain.Entities;
using Mogym.Infrastructure;

namespace Mogym.Application.Services
{
    public class UserService:IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISeriLogService _logger;
        private readonly IRoleService _roleService;
        private readonly IUserRoleService _userRoleService;
        private readonly IHttpContextAccessor _accessor;
        private readonly ISmsLogService _smsLogService;
        private readonly IEmailSender _emailSender;
        public UserService(IUnitOfWork unitOfWork,
            IMapper mapper,
            ISeriLogService logger,
            IRoleService roleService,
            IUserRoleService userRoleService,
            IHttpContextAccessor accessor,
            ISmsLogService smsLogService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _roleService = roleService;
            _userRoleService = userRoleService;
            _accessor = accessor;
            _smsLogService = smsLogService;
        }

        public bool IsExistMobile(string mobile)
        {
            return  _unitOfWork.UserRepository.Find(x => x.Mobile.Trim() == mobile.Trim()).Any()  ;
        }

        public async Task AddAsync(LoginRecord loginRecord)
        {
            try
            {
                var user = _mapper.Map<User>(loginRecord);
                 await _unitOfWork.UserRepository.AddAsync(user);

            }
            catch(Exception ex)
            {
                var message = $"AddAsync in User Service,entity:" + JsonSerializer.Serialize(loginRecord);
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }

        }


        /// <summary>
        /// لاگین یوزر برای بار دوم به بعد که با تولید کد تائید اس ام اس هست 
        /// </summary>
        /// <param name="loginRecord"></param>
        /// <returns>ConfirmSmsRecord</returns>
        public async Task<ConfirmSmsRecord> LoginAsync(LoginRecord loginRecord)
        {
            
            try
            {
                var entity = await _unitOfWork.UserRepository.Find(x => x.Mobile.Trim() == loginRecord.Mobile.Trim()).FirstOrDefaultAsync();
                if (entity != null)
                {
                    entity.SmsConfirmCode = new Random().Next(10000, 99999).ToString();
                    //entity.SmsConfirmCode = "12345";
                    _unitOfWork.UserRepository.Update(entity);

                    await _smsLogService.SendConfirmSmsCode(entity.Mobile,entity.SmsConfirmCode);

                    return _mapper.Map<ConfirmSmsRecord>(entity);
                }

                var newUser = _mapper.Map<User>(loginRecord);
                var userRole = _mapper.Map<UserRole>(newUser);

                newUser.UserRoles.Add(userRole);
                await _unitOfWork.UserRepository.AddAsync(newUser);

                await _smsLogService.SendConfirmSmsCode(newUser.Mobile,newUser.SmsConfirmCode);

                return _mapper.Map<ConfirmSmsRecord>(newUser);


            }
            catch (Exception ex)
            {
                var message = $"Login in User Service,entity:" + JsonSerializer.Serialize(loginRecord);
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }

            return null;
        }

        public async Task<UserRecord> Login(LoginRecord loginRecord)
        {
            try
            {
                var hashPassword = Common.Helper.HashString(loginRecord.Password);
                var user =await  _unitOfWork.UserRepository
                    .Find(x => x.Email.Trim() == loginRecord.Email && x.Password == hashPassword)
                    .Include(x => x.UserRoles)
                    .ThenInclude(x => x.UserRole_Role)
                    .ThenInclude(x => x.RolePermissions)
                    .ThenInclude(x => x.RolePermission_Permission)
                    .FirstOrDefaultAsync();
                if (user is not null)
                {
                    return _mapper.Map<UserRecord>(user);

                }
            }
            catch (Exception ex)
            {
                var message = $"Login in User Service,entity:" + JsonSerializer.Serialize(loginRecord);
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }

            return null;
        }

        /// <summary>
        /// چک کردن موبایل با کد تائید
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="confirmSmsCode"></param>
        /// <returns>bool</returns>
        public bool IsExistMobileWithConfirmSmsCode(string mobile, string confirmSmsCode)
        {
            try
            {
                return _unitOfWork.UserRepository.Find(x => x.Mobile == mobile && x.SmsConfirmCode == confirmSmsCode)
                    .Any();
            }
            catch (Exception ex)
            {
                var message = $"IsExistMobileWithConfirmSmsCode in User Service,mobile=" + mobile + ";confirmSmsCode=" +
                              confirmSmsCode;
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }

            return false;
        }

        /// <summary>
        /// بر اساس وضعیت منتظر تائید اس ام اس و یا تائید اطلاعات رو برمیگردونه
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns>UserRecord</returns>
        public async Task<UserRecord> GetUserWithRoleAndPermission(string mobile)
        {
            try
            {
                var entityInWaitingForConfirmSmsCode = await _unitOfWork.UserRepository.Find(x => x.Mobile == mobile &&
                    x.Status==EnumStatus.WaitingForSmsConfirm).FirstOrDefaultAsync();

                if (entityInWaitingForConfirmSmsCode!=null)
                    return await UpdateEntityToActiveForAuthentication(entityInWaitingForConfirmSmsCode);

                var entityInActiveMode= await _unitOfWork.UserRepository.Find(x => x.Mobile == mobile &&
                    x.Status == EnumStatus.Active).FirstOrDefaultAsync();

                if (entityInActiveMode != null)
                {
                    var entity = GetEntityWithRoleAndPermission(entityInActiveMode);
                    return _mapper.Map<UserRecord>(entity);
                }

            }
            catch (Exception ex)
            {
                var message = $"GetUserWithRoleAndPermission in User Service,mobile=" + mobile ;
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }

            return null;
        }

        public async Task<ConfirmSmsRecord> SignUpTrainer(SignUpTrainerRecord signUpTrainerRecord)
        {
            try
            {
                var newTrainer = _mapper.Map<User>(signUpTrainerRecord);
                var userRoleRecord = _mapper.Map<CreateTrainerUserRoleRecord>(newTrainer);
                var userRole = _mapper.Map<UserRole>(userRoleRecord);

                newTrainer.UserRoles.Add(userRole);

                await _unitOfWork.UserRepository.AddAsync(newTrainer);


                /////
                var trainerProfile = new TrainerProfile();
                trainerProfile.UserId = newTrainer.Id;
                /////
                await _unitOfWork.TrainerProfileRepository.AddAsync(trainerProfile);

                return _mapper.Map<ConfirmSmsRecord>(newTrainer);


            }
            catch (Exception ex)
            {
                var message = $"SignUpTrainer in User Service,entity:" + JsonSerializer.Serialize(signUpTrainerRecord);
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }
        }

        public User? GetCurrentUserRols()
        {
            var userId = _accessor.GetUser();
            return _unitOfWork.UserRepository.Find(x => x.Id == userId)
                .AsNoTracking()
                .Include(x => x.UserRoles)
                .ThenInclude(x => x.UserRole_Role).FirstOrDefault();
        }

        public async Task<UserRecord> SignUp(SignupRecord signupRecord)
        {
            try
            {
                var newUser = _mapper.Map<User>(signupRecord);
                var userRoleRecord = _mapper.Map<CreateAthleteUserRoleRecord>(newUser);
                var userRole = _mapper.Map<UserRole>(userRoleRecord);

                newUser.UserRoles.Add(userRole);

                await _unitOfWork.UserRepository.AddAsync(newUser);

                var entity = GetEntityWithRoleAndPermission(newUser);
                return _mapper.Map<UserRecord>(entity);
            }
            catch (Exception ex)
            {
                var message = $"SignUp in User Service,object=" + JsonSerializer.Serialize(signupRecord);
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }
        }

        public bool IsThereAnyEmailAddress(string email)
        {
            return _unitOfWork.UserRepository.Find(x => x.Email.Trim() == email.Trim()).Any();
        }

        public async Task CreateTrainerNew(SignUpTrainerRecordNew signUpTrainerRecordNew)
        {
            try
            {
                var trainer = _mapper.Map<User>(signUpTrainerRecordNew);
                var userRoleRecord = _mapper.Map<CreateTrainerUserRoleRecord>(trainer);
                var userRole = _mapper.Map<UserRole>(userRoleRecord);

                trainer.UserRoles.Add(userRole);

                await _unitOfWork.UserRepository.AddAsync(trainer);

                var trainerProfile = new TrainerProfile();
                trainerProfile.UserId = trainer.Id;
                trainerProfile.CartOwnerName = trainer.FirstName + " " + trainer.LastName;
                await _unitOfWork.TrainerProfileRepository.AddAsync(trainerProfile);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<UserRecord> CreateTrainer(SignUpTrainerRecordNew signUpTrainerRecordNew)
        {
            try
            {
                var trainer = _mapper.Map<User>(signUpTrainerRecordNew);
                var userRoleRecord = _mapper.Map<CreateTrainerUserRoleRecord>(trainer);
                var userRole = _mapper.Map<UserRole>(userRoleRecord);

                trainer.UserRoles.Add(userRole);

                await _unitOfWork.UserRepository.AddAsync(trainer);

                var trainerProfile = new TrainerProfile();
                trainerProfile.UserId = trainer.Id;
                trainerProfile.CartOwnerName = trainer.FirstName + " " + trainer.LastName;
                await _unitOfWork.TrainerProfileRepository.AddAsync(trainerProfile);

                var entity = GetEntityWithRoleAndPermission(trainer);
                return _mapper.Map<UserRecord>(entity);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UserRecord> LoginMobile(LoginRecord loginRecord)
        {
            try
            {
                var hashPassword = Common.Helper.HashString(loginRecord.Password);
                var user = await _unitOfWork.UserRepository
                    .Find(x => x.Mobile.Trim() == loginRecord.Mobile && x.Password == hashPassword)
                    .Include(x => x.UserRoles)
                    .ThenInclude(x => x.UserRole_Role)
                    .ThenInclude(x => x.RolePermissions)
                    .ThenInclude(x => x.RolePermission_Permission)
                    .FirstOrDefaultAsync();
                if (user is not null)
                {
                    return _mapper.Map<UserRecord>(user);

                }
            }
            catch (Exception ex)
            {
                var message = $"Login in User Service,entity:" + JsonSerializer.Serialize(loginRecord);
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }

            return null;
        }

        public async Task<int> GetTrainerCountForIndexPage()
        {
            return await _unitOfWork.UserRepository.Find(x => x.UserRoles.Any(z => z.UserRole_Role.EnglishName == "Trainer"))
                .Include(x => x.UserRoles)
                .ThenInclude(x => x.UserRole_Role)
                .CountAsync();

        }

        public async Task<int> GetUserCountForIndexPage()
        {
            return await _unitOfWork.UserRepository.Find(x => x.UserRoles.Any(z => z.UserRole_Role.EnglishName == "Athlete"))
                .Include(x => x.UserRoles)
                .ThenInclude(x => x.UserRole_Role)
                .CountAsync();
        }

        public async Task ChangePassword(string password)
        {
            try
            {
                var userId = _accessor.GetUser();
                var entity = _unitOfWork.UserRepository.GetById(userId);
                entity.Password = Common.Helper.HashString(password);
                await _unitOfWork.UserRepository.UpdateAsync(entity);

            }
            catch (Exception ex)
            {
                throw ex;
;           }
        }


        /// <summary>
        /// این متد برای تغییر وضعیت یوزر از منتظر تائید اس ام اس به تائید و اعطای رول ورزشکار به یوزر بعد از ثبت نام هست
        /// </summary>
        /// <param name="entityInWaitingForConfirmSmsCode"></param>
        /// <returns>UserRecord</returns>
        private async Task<UserRecord> UpdateEntityToActiveForAuthentication(User entityInWaitingForConfirmSmsCode)
        {
            try
            {
                entityInWaitingForConfirmSmsCode.Status = EnumStatus.Active;
                _unitOfWork.UserRepository.Update(entityInWaitingForConfirmSmsCode,false);


                var trainerRole = _roleService.GetRoleByName("Athlete");

                var userRole = new UserRole()
                {
                    RoleId = trainerRole.Id.Value,
                    UserId = entityInWaitingForConfirmSmsCode.Id
                };

                _userRoleService.Add(userRole,true);

                var entity = GetEntityWithRoleAndPermission(entityInWaitingForConfirmSmsCode);
                return   _mapper.Map<UserRecord>(entity);

            }
            catch (Exception ex)
            {
                var message = $"UpdateEntityToActiveForAuthentication in User Service,object=" + JsonSerializer.Serialize(entityInWaitingForConfirmSmsCode);
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }

            return null;
        }

        /// <summary>
        /// گرفتن اطلاعات کامل یوزر شامل نقش و دسترسی بر اساس آیدی یوزر
        /// </summary>
        /// <param name="user"></param>
        /// <returns>User</returns>
        private User GetEntityWithRoleAndPermission(User user)
        {
            return _unitOfWork.UserRepository.Find(x => x.Id == user.Id)
                .Include(x => x.UserRoles)
                .ThenInclude(x => x.UserRole_Role)
                .ThenInclude(x => x.RolePermissions)
                .ThenInclude(x=>x.RolePermission_Permission)
                .First();
        }
    }
}
