using System;
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
        Task<UserRecord> Login(LoginRecord loginRecord);
        bool IsExistMobileWithConfirmSmsCode(string mobile, string confirmSmsCode);
        Task<UserRecord> GetUserWithRoleAndPermission(string mobile);
        Task<ConfirmSmsRecord> SignUpTrainer(SignUpTrainerRecord signUpTrainerRecord);
        User? GetCurrentUserRols();
        Task<UserRecord> SignUp(SignupRecord signupRecord);
        bool IsThereAnyEmailAddress(string email);
        Task CreateTrainerNew(SignUpTrainerRecordNew signUpTrainerRecordNew);
        Task ChangePassword(string password);
        Task<UserRecord> CreateTrainer(SignUpTrainerRecordNew signUpTrainerRecordNew);
        Task<UserRecord> LoginMobile(LoginRecord loginRecord);
    }
}
