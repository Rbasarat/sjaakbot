using Telegram.Bot;

namespace TelegramChatbot.Interfaces
{
    public interface IBotService
    {
        TelegramBotClient Client { get; }
    }
}