using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;
using Mogym.Application.Interfaces;
using Mogym.Domain.Entities;

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
        public static string GetCurrentUserMobile(this IHttpContextAccessor httpContextAccessor)
        {
            return httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.MobilePhone)?.Value ?? "";
        }
        public static string GetCurrentUserRoleName(this IHttpContextAccessor httpContextAccessor)
        {
            return httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Role)?.Value ?? "";
        }
        public static List<Role> GetCurrentUserRole(IServiceProvider sp)
        {
            using (var scope = sp.CreateScope())
            {
                var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
                var user = userService.GetCurrentUserRols();
                return user.UserRoles.Select(x => x.UserRole_Role).ToList();
            }
        }
        public static int? GetCurrentUserTrainerId(IServiceProvider sp)
        {
            using (var scope = sp.CreateScope())
            {
                var trainerProfileService = scope.ServiceProvider.GetRequiredService<ITrainerProfileService>();
                var trainer= trainerProfileService.GetCurrentUserTrainer();
                return trainer != null ? trainer.Id : null;
            }
        }
    }
}
