using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Mogym.Application.Interfaces;
using Mogym.Application.Interfaces.ILog;
using Mogym.Domain.Entities;
using Mogym.Infrastructure;

namespace Mogym.Application.Services
{
    public class UserRoleService:IUserRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISeriLogService _logger;
        public UserRoleService(IUnitOfWork unitOfWork, IMapper mapper, ISeriLogService logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public void Add(UserRole userRole,bool saveChanges)
        {
            try
            {
                _unitOfWork.UserRoleRepository.Add(userRole,saveChanges);
            }
            catch (Exception ex)
            {
                var message = $"Add in UserRole Service,object=" + JsonSerializer.Serialize(userRole);
                _logger.LogError(message, ex);
            }
        }
    }
}
