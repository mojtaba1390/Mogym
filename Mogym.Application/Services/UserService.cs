using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mogym.Application.Interfaces;
using Mogym.Application.Interfaces.ILog;
using Mogym.Application.Records.User;
using Mogym.Domain.Entities;
using Mogym.Infrastructure;

namespace Mogym.Application.Services
{
    public class UserService:IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISeriLogService _logger;
        public UserService(IUnitOfWork unitOfWork,IMapper mapper, ISeriLogService logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public bool IsExistMobile(string mobile)
        {
            return _unitOfWork.UserRepository.Find(x => x.Mobile.Trim() == mobile.Trim()).Any();
        }

        public async Task<User> AddAsync(RegisterUserRecord registerUser)
        {
            try
            {
                var user = _mapper.Map<User>(registerUser);
                return await _unitOfWork.UserRepository.AddAsync(user);
            }
            catch(Exception ex)
            {
                var message = $"AddAsync in User Service,entity:" + registerUser;
                _logger.LogError(message,ex);
            }

            return null;
        }
    }
}
