using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Mogym.Application.Interfaces;
using Mogym.Application.Interfaces.ILog;
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

        public void Update(TrainerProfile trainerInfo)
        {
            try
            {
                _unitOfWork.TrainerProfileRepository.Update(trainerInfo,false);
                _unitOfWork.UserRepository.Update(trainerInfo.User);
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
