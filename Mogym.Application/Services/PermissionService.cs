using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Mogym.Application.Interfaces;
using Mogym.Application.Interfaces.ILog;
using Mogym.Application.Records.Permission;
using Mogym.Domain.Entities;
using Mogym.Infrastructure;

namespace Mogym.Application.Services
{
    public class PermissionService:IPermissionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISeriLogService _logger;
        public PermissionService(IUnitOfWork unitOfWork, IMapper mapper, ISeriLogService logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PermissionRecord> GetPermissionByEnglishName(string englishName)
        {
            try
            {
                var permission = await _unitOfWork.PermissionRepository
                    .Find(x => x.EnglishName.Trim() == englishName.Trim()).FirstOrDefaultAsync();
                if(permission is not null)
                    return _mapper.Map<PermissionRecord>(permission);

            }
            catch (Exception ex)
            {
                var message = $"GetPermissionByEnglishName in Permission Service,name=" + englishName;
                _logger.LogError(message, ex);
                throw ex;
            }

            return null;
        }

        public async Task<List<PermissionRecord>> GetAll()
        {
            try
            {
                var permissions = _unitOfWork.PermissionRepository.GetAll();
                return _mapper.Map<List<PermissionRecord>>(permissions);
            }
            catch (Exception ex)
            {
                var message = $"GetAll in Permission Service" ;
                _logger.LogError(message, ex);
                throw ex;
            }

            return null;
        }

        public async Task<PermissionRecord> AddAsync(Permission permission, bool saveChanges)
        {
            try
            {
                 var entiry= _unitOfWork.PermissionRepository.AddAsync(permission, saveChanges);
                 return _mapper.Map<PermissionRecord>(entiry);
            }
            catch (Exception ex)
            {
                var message = $"AddAsync in Permission Service,obj=" + JsonSerializer.Serialize(permission);
                _logger.LogError(message, ex);
                throw ex;
            }
        }
    }
}
