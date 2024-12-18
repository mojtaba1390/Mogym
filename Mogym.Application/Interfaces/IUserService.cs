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
        Task<OTPRecord> LoginAsync(OTPLoginRecord otpLoginRecord);
        Task<UserRecord> Login(LoginRecord loginRecord);
        bool IsExistMobileWithConfirmSmsCode(string mobile, string confirmSmsCode);
        Task<UserRecord> GetUserWithRoleAndPermission(string mobile);
        Task<OTPRecord> SignUpTrainer(SignUpTrainerRecord signUpTrainerRecord);
        User? GetCurrentUserRols();
        Task<UserRecord> SignUp(SignupRecord signupRecord);
        bool IsThereAnyEmailAddress(string email);
        Task CreateTrainerNew(SignUpTrainerRecordNew signUpTrainerRecordNew);
        Task ChangePassword(string password);
        Task<UserRecord> CreateTrainer(SignUpTrainerRecordNew signUpTrainerRecordNew);
        Task<UserRecord> LoginMobile(LoginRecord loginRecord);
        Task<int> GetTrainerCountForIndexPage();
        Task<int> GetUserCountForIndexPage();
        Task<string> SendTrainerOtp(OTPLoginRecord otpLoginRecord);
        Task PreRegisterTrainer(Records.Profile.SignUpTrainerRecord signUpTrainerRecord);
    }
}
