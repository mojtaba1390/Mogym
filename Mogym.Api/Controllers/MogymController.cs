using Microsoft.AspNetCore.Mvc;
using Mogym.Application.ModelExtended;
using Mogym.Application.Records.User;

namespace Mogym.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MogymController : ControllerBase
    {
        [HttpGet("GetUsersFromGroup")]
        public async Task<IActionResult> GetUsersFromGroup()
        {
            try
            {
                string apiKey = "YOUR_API_KEY";
                long chatId = 11;

                var myBot = new MogymBot(apiKey);
                await myBot.GetGroupParticipantsUsernames(chatId);
                return null;
            }
            catch (Exception e)
            {
                return Problem();
            }


        }
    }
}
