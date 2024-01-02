using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace Mogym.Application.ModelExtended
{
    public class MogymBot
    {
        private readonly TelegramBotClient _botClient;

        public MogymBot(string apiKey)
        {
            _botClient = new TelegramBotClient(apiKey);
        }

        public async Task GetGroupParticipantsUsernames(long chatId)
        {
            var members = await _botClient.GetChatAdministratorsAsync(chatId);

            foreach (var member in members)
            {
                Console.WriteLine(member.User.Username);
            }
        }
    }
}
