using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Mogym.Application.Interfaces;
using Mogym.Application.Interfaces.ILog;
using Mogym.Application.Records.Role;
using Mogym.Infrastructure;

namespace Mogym.Application.Services
{
    public class RoleService:IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISeriLogService _logger;
        public RoleService(IUnitOfWork unitOfWork, IMapper mapper, ISeriLogService logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public RoleRecord GetRoleByName(string roleName)
        {
            try
            {
                var role = _unitOfWork.RoleRepository.Find(x => x.EnglishName.Trim() == roleName.Trim()).FirstOrDefault();
                return _mapper.Map<RoleRecord>(role);
            }
            catch (Exception ex)
            {
                var message = $"GetRoleByName in Role Service,roleName=" + roleName;
                _logger.LogError(message, ex);
            }

            return null;
        }
    }
}
