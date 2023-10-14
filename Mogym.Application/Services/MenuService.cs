using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Mogym.Application.Interfaces;
using Mogym.Application.Records.Menu;
using Mogym.Infrastructure;

namespace Mogym.Application.Services
{
    public class MenuService:IMenuService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public MenuService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork=unitOfWork;
            _mapper = mapper;
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
    }
}
