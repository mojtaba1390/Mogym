using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Mogym.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Application
{
    public static class Helper
    {


        public static string GetModelSateErroMessage(this ModelStateDictionary modelState)
        {
            return string.Join("; ", modelState.Values
                .SelectMany(x => x.Errors)
                .Select(x => x.ErrorMessage));
        }
        public static int GetUser(this IHttpContextAccessor httpContextAccessor)
        {
            var user = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return int.Parse(user);
        }
        public static string GetUserFullName(this IHttpContextAccessor httpContextAccessor)
        {
            return httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.GivenName)?.Value ?? "";
        }
        public static string GetUserName(this IHttpContextAccessor httpContextAccessor)
        {
            return httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Name)?.Value ?? "";
        }
        public static string GetCurrentUserMobile(this IHttpContextAccessor httpContextAccessor)
        {
            return httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.MobilePhone)?.Value ?? "";
        }

        public static string RandomStringCode(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }



    }
}
