﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mogym.Application.Interfaces;
using Mogym.Application.Interfaces.ILog;
using Mogym.Application.Records.User;
using Mogym.Common;
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

        public UserService(IUnitOfWork unitOfWork,IMapper mapper, ISeriLogService logger, IRoleService roleService,IUserRoleService userRoleService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _roleService = roleService;
            _userRoleService = userRoleService;
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
                _logger.LogError(message,ex);
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
                    //TODO:خط زیر از کامنت در بیاد
                    //entity.SmsConfirmCode = new Random().Next(10000, 99999).ToString();
                    entity.SmsConfirmCode = "12345";
                    _unitOfWork.UserRepository.Update(entity);
                    return _mapper.Map<ConfirmSmsRecord>(entity);
                }

                var newUser = _mapper.Map<User>(loginRecord);
                await _unitOfWork.UserRepository.AddAsync(newUser);
                return _mapper.Map<ConfirmSmsRecord>(newUser);


            }
            catch (Exception ex)
            {
                var message = $"Login in User Service,entity:" + JsonSerializer.Serialize(loginRecord);
                _logger.LogError(message, ex);
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
                _logger.LogError(message, ex);
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
                var entityInWaitingForConfirmSmsCode = _unitOfWork.UserRepository.Find(x => x.Mobile == mobile && x.Status==EnumStatus.WaitingForSmsConfirm).FirstOrDefault();
                if (entityInWaitingForConfirmSmsCode!=null)
                    return await UpdateEntityToActiveForAuthentication(entityInWaitingForConfirmSmsCode);

                var entityInActiveMode= _unitOfWork.UserRepository.Find(x => x.Mobile == mobile && x.Status == EnumStatus.Active).FirstOrDefault();
                if (entityInActiveMode != null)
                {
                    var entity = GetEntityWithRoleAndPermission(entityInActiveMode);
                    return _mapper.Map<UserRecord>(entity);
                }

            }
            catch (Exception ex)
            {
                var message = $"GetUserWithRoleAndPermission in User Service,mobile=" + mobile ;
                _logger.LogError(message, ex);
                throw ex;
            }

            return null;
        }

        public async Task<ConfirmSmsRecord> SignUpTrainer(SignUpTrainerRecord signUpTrainerRecord)
        {
            try
            {
                var entity = await _unitOfWork.UserRepository.Find(x => x.Mobile.Trim() == signUpTrainerRecord.Mobile.Trim()).FirstOrDefaultAsync();
                if (entity != null)
                {
                    //TODO:خط زیر از کامنت در بیاد
                    //entity.SmsConfirmCode = new Random().Next(10000, 99999).ToString();
                    entity.SmsConfirmCode = "12345";
                    _unitOfWork.UserRepository.Update(entity);
                    return _mapper.Map<ConfirmSmsRecord>(entity);
                }

                var trainer = _mapper.Map<User>(signUpTrainerRecord);
                await _unitOfWork.UserRepository.AddAsync(trainer, false);



                return _mapper.Map<ConfirmSmsRecord>(trainer);


            }
            catch (Exception ex)
            {
                var message = $"SignUpTrainer in User Service,entity:" + JsonSerializer.Serialize(signUpTrainerRecord);
                _logger.LogError(message, ex);
                throw ex;
            }
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
                _logger.LogError(message, ex);
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
                .AsNoTracking()
                .First();
        }
    }
}
