using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Mogym.Api
{
    public static class Helper
    {
        public static string GetModelSateErroMessage(this ModelStateDictionary modelState)
        {
            return string.Join("; ", modelState.Values
                .SelectMany(x => x.Errors)
                .Select(x => x.ErrorMessage));
        }
    }
}
