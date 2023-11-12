﻿using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;

namespace Mogym
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
    }
}