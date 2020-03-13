using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace TelegramChatbot.Interfaces
{
    public interface IUpdateService
    {
        Task EchoAsync(Update update);
    }
}
