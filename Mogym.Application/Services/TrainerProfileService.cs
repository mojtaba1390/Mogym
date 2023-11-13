using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Mogym.Application.Interfaces;
using Mogym.Application.Interfaces.ILog;
using Mogym.Application.Records.Profile;
using Mogym.Domain.Entities;
using Mogym.Infrastructure;

namespace Mogym.Application.Services
{
    public class TrainerProfileService:ITrainerProfileService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISeriLogService _logger;

        public TrainerProfileService(IUnitOfWork unitOfWork, IMapper mapper, ISeriLogService logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<TrainerProfile?> GetByUserName(string username)
        {
            try
            {
                return await _unitOfWork.TrainerProfileRepository
                    .Find(x => x.User.UserName == username.Trim() && x.User.UserRoles.Any(z => z.RoleId == 3))
                    .AsNoTracking()
                    .Include(x => x.User)
                    .ThenInclude(x=>x.UserRoles)
                    .FirstOrDefaultAsync();


            }
            catch (Exception ex)
            {
                var message = $"GetByUserName in TrainerProfile Service";
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }

            return null;
        }

        public async Task Update(TrainerProfileRecord trainerInfoRecord)
        {
            try
            {
                var trainerProfileOld = _unitOfWork.TrainerProfileRepository.Find(x => x.Id == trainerInfoRecord.Id)
                    .Include(x => x.User)
                    .FirstOrDefault();

                var trainerInfo = _mapper.Map(trainerInfoRecord, trainerProfileOld);
                var user = _mapper.Map(trainerInfoRecord, trainerProfileOld.User);

                 _unitOfWork.TrainerProfileRepository.Update(trainerInfo,false);
                 _unitOfWork.UserRepository.Update(user);
            }
            catch (Exception ex)
            {
                var message = $"Update in TrainerProfile Service";
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }
        }
    }
}
