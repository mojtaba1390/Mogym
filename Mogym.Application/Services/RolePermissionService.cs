using Mogym.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Mogym.Application.Records.RolePermission;
using Mogym.Domain.Entities;
using Mogym.Infrastructure;

namespace Mogym.Application.Services
{
    public class RolePermissionService: IRolePermissionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public RolePermissionService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork=unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<RolePermissionRecord>> GetAll()
        {
            try
            {
                return await _unitOfWork.RolePermissionRepository
                    .GetAll()
                    .Include(x=>x.RolePermission_Permission)
                    .ProjectTo<RolePermissionRecord>(_mapper.ConfigurationProvider)
                    .ToListAsync();
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        public async Task DeleteByPermissionIdAndRoleId(int permissionId, int roleId)
        {
            var entity = await _unitOfWork.RolePermissionRepository
                .Where(x => x.PermissionId == permissionId && x.RoleId == roleId).FirstOrDefaultAsync();
             _unitOfWork.RolePermissionRepository.Delete(entity);
        }

        public async Task AddByPermissionIdAndRoleId(int permissionId, int roleId)
        {
            var entity = new RolePermission()
            {
                RoleId = roleId,
                PermissionId = permissionId
            };
            await _unitOfWork.RolePermissionRepository.AddAsync(entity);
        }
    }
}
