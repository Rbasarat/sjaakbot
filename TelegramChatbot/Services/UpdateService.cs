using Serilog;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramChatbot.Interfaces;

namespace TelegramChatbot.Services
{
    public class UpdateService : IUpdateService
    {
        private readonly IBotService _botService;

        public UpdateService(IBotService botService)
        {
            _botService = botService;
        }

        public async Task EchoAsync(Update update)
        {
            if (update.Type != UpdateType.Message)
                return;

            var message = update.Message;

            Log.Information("Received Message from {0}", message.Chat.Id);

            switch (message.Type)
            {
                case MessageType.Text:
                    // Echo each Message
                    await _botService.Client.SendTextMessageAsync(message.Chat.Id, message.Text);
                    break;

                case MessageType.Photo:
                    // Download Photo
                    var fileId = message.Photo.LastOrDefault()?.FileId;
                    var file = await _botService.Client.GetFileAsync(fileId);

                    var filename = file.FileId + "." + file.FilePath.Split('.').Last();
                    using (var saveImageStream = System.IO.File.Open(filename, FileMode.Create))
                    {
                        await _botService.Client.DownloadFileAsync(file.FilePath, saveImageStream);
                    }

                    await _botService.Client.SendTextMessageAsync(message.Chat.Id, "Thx for the Pics");
                    break;
            }
        }
    }
}
