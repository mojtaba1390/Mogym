using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Mogym.Application.Interfaces;
using Mogym.Application.Interfaces.ILog;
using Mogym.Application.Records.Menu;
using Mogym.Application.Records.Permission;
using Mogym.Infrastructure;

namespace Mogym.Application.Services
{
    public class MenuService:IMenuService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPermissionService _permissionService;
        private readonly ISeriLogService _logger;

        public MenuService(IUnitOfWork unitOfWork,IMapper mapper,IPermissionService permissionService,ISeriLogService logger)
        {
            _unitOfWork=unitOfWork;
            _mapper = mapper;
            _permissionService=permissionService;
            _logger=logger;
        }
        public async Task<List<MenuRecord>> GetAllActiveMenuList()
        {
            try
            {
                var menus = _unitOfWork.MenuRepository.GetAll();
                return _mapper.Map<List<MenuRecord>>(menus);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<PermissionRecord>> GetAllPermissionListForCreateMenuParent()
        {
            try
            {
                var permissions=  _unitOfWork.PermissionRepository.GetAll();
                return  _mapper.Map<List<PermissionRecord>>(permissions);
            }
            catch (Exception ex)
            {
                var message = $"GetAllPermissionListForCreateMenuParent in Menu Service";
                _logger.LogError(message, ex);
                throw ex;
            }
        }
    }
}
