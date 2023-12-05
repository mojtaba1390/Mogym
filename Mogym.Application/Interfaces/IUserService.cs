﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Mogym.Application.Records.User;
using Mogym.Domain.Entities;

namespace Mogym.Application.Interfaces
{
    public interface IUserService
    {
        bool IsExistMobile(string mobile);
        Task AddAsync(LoginRecord loginRecord);
        Task<ConfirmSmsRecord> LoginAsync(LoginRecord loginRecord);
        bool IsExistMobileWithConfirmSmsCode(string mobile, string confirmSmsCode);
        Task<UserRecord> GetUserWithRoleAndPermission(string mobile);
        Task<ConfirmSmsRecord> SignUpTrainer(SignUpTrainerRecord signUpTrainerRecord);
        User? GetCurrentUserRols();
        Task SignUp(SignupRecord signupRecord);
    }
}
