using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace EyelashesAPI.TelegramBot
{
    public class TelegramMessageService : ITelegramMessageService
    {
        private readonly ITelegramBotClient _botClient;
        private readonly ILogger<TelegramMessageService> _logger;

        public TelegramMessageService(ITelegramBotClient botClient, ILogger<TelegramMessageService> logger)
        {
            _botClient = botClient;
            _logger = logger;
        }

        public async Task SendMessageAsync(long chatId, string message, CancellationToken cancellationToken, bool isOrderRequest = false, string orderDetails = "")
        {
            try
            {
                if (isOrderRequest)
                {
                    var inlineKeyboard = new InlineKeyboardMarkup(new[]
                    {
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("Подтвердить", "confirm"),
                            InlineKeyboardButton.WithCallbackData("Отменить", "cancel")
                        }
                    });

                    await _botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: $"Новая заявка на услугу:\n{orderDetails}",
                        replyMarkup: inlineKeyboard,
                        cancellationToken: cancellationToken
                    );
                }
                else
                {
                    await _botClient.SendTextMessageAsync(chatId, message, cancellationToken: cancellationToken);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при отправке сообщения");
            }
        }
    }
}
