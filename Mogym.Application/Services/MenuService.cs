using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Mogym.Application.Interfaces;
using Mogym.Application.Interfaces.ILog;
using Mogym.Application.Records.Menu;
using Mogym.Application.Records.Permission;
using Mogym.Common;
using Mogym.Domain.Entities;
using Mogym.Infrastructure;

namespace Mogym.Application.Services
{
    public class MenuService:IMenuService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPermissionService _permissionService;
        private readonly ISeriLogService _logger;
        private readonly IHttpContextAccessor _accessor;
        public MenuService(IUnitOfWork unitOfWork,IMapper mapper,IPermissionService permissionService,ISeriLogService logger,IHttpContextAccessor accessor)
        {
            _unitOfWork=unitOfWork;
            _mapper = mapper;
            _permissionService=permissionService;
            _logger=logger;
            _accessor=accessor;
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

        public async Task<MenuRecord> GetMenuByEnglishName(string englishName)
        {
            try
            {
                var menu = await _unitOfWork.MenuRepository.Find(x => x.EnglishName.Trim() == englishName.Trim())
                    .FirstOrDefaultAsync();
                if (menu is not null)
                    return  _mapper.Map<MenuRecord>(menu);
            }
            catch (Exception ex)
            {
                var message = $"GetMenuByEnglishName in Menu Service,name="+englishName;
                _logger.LogError(message, ex);
                throw ex;
            }

            return null;
        }


        /// <summary>
        /// در زمان ثبت منو، به ازای هر منو به طور خودکار دسترسی هم ثبت میشه
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void AddMenu(CreateMenuRecord model)
        {
            try
            {
                var menu = _mapper.Map<Menu>(model);

                 _unitOfWork.MenuRepository.Add(menu );

                var permission = _mapper.Map<Permission>(model);
                _unitOfWork.PermissionRepository.Add(permission);

            }
            catch (Exception ex)
            {
                var message = $"AddMenu in Menu Service,obj=" + JsonSerializer.Serialize(model);
                _logger.LogError(message, ex);
                throw ex;
            }
        }

        //TODO Vital:لیست با سرچ و ریلیتد به صورت جنریک در اولویت همه ی کارهاس
        public async Task<List<MenuRecord>> GetAllWithRelated()
        {
            try
            {
                var menus = _unitOfWork.MenuRepository.GetAll().Include(x=>x.Menu_Menu);
                return _mapper.Map<List<MenuRecord>>(menus);

            }
            catch (Exception ex)
            {
                var message = $"GetAllWithRelated in Menu Service";
                _logger.LogError(message, ex);
                throw ex;
            }
        }

        public async Task<List<MenuRecord>> GetActiveUserMenus()
        {
            try
            {
                var userId = _accessor.GetUser();
                var userWithRelateds = await _unitOfWork.UserRepository.Find(x => x.Id == userId)
                    .Include(x => x.UserRoles)
                    .ThenInclude(x => x.UserRole_Role)
                    .ThenInclude(x => x.RolePermissions)
                    .ThenInclude(x => x.RolePermission_Permission)
                    .FirstOrDefaultAsync();
                if (userWithRelateds is not null)
                {
                    var permissions = userWithRelateds.UserRoles
                        .Select(x => x.UserRole_Role.RolePermissions
                            .Select(z => z.RolePermission_Permission).ToList()).FirstOrDefault();

                    var allMenus= _unitOfWork.MenuRepository.GetAll().Include(x => x.Menu_Menu).ToList();

                    var menus = allMenus.IntersectBy(permissions.Select(z=>z.EnglishName), x => x.EnglishName);
                    return _mapper.Map<List<MenuRecord>>(menus);

                }

                ArgumentNullException.ThrowIfNull(userWithRelateds);

            }
            catch (Exception ex)
            {
                var message = $"GetAllWithRelated in Menu Service";
                _logger.LogError(message, ex);
                throw ex;
            }

            return null;
        }
    }
}
