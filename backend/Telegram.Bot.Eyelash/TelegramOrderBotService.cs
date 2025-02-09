
using Telegram.Bot;

namespace Telegram.Bot.Eyelash
{
    public class TelegramOrderBotService: BackgroundService
    {

        private readonly ILogger<TelegramOrderBotService> _logger;
        private readonly TelegramOp _clientOptions;

        public TelegramOrderBotService(ILogger<TelegramOrderBotService> logger, IOptions<TelegramBotClientOptions> clientOptions) 
        {
            _logger = logger;
            _clientOptions = clientOptions;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken) 
        {
            Rece
        }

        //private readonly ITelegramBotClient _botClient;

        //public TelegramOrderBot(string token)
        //{
        //    _botClient = new TelegramBotClient(token);
        //}


        //public void StartReceiving(CancellationToken cancellationToken)
        //{
        //    _botClient.StartReceiving(
        //        updateHandler: HandleUpdateAsync,
        //        errorHandler: HandleErrorAsync,
        //        cancellationToken: cancellationToken);
        //}


        //private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        //{
        //    if (update.CallbackQuery != null)
        //    {
        //        await HandleCallbackQueryAsync(botClient,update.CallbackQuery, cancellationToken: cancellationToken);
        //    }
        //    else if (update.Message != null)
        //    {
        //        await HandleMessageAsync(update.Message, cancellationToken);
        //    }
        //}

        //public async Task HandleCallbackQueryAsync(ITelegramBotClient botClient, CallbackQuery callbackQuery, CancellationToken cancellationToken)
        //{
        //    if (callbackQuery.Data == "accept")
        //    {
        //        // Принять заказ
        //        await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Заказ принят.", cancellationToken: cancellationToken);
        //    }
        //    else if (callbackQuery.Data == "reject")
        //    {
        //        // Отклонить заказ
        //        await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Заказ отклонен.", cancellationToken: cancellationToken);
        //    }

        //    // Ответ на callback (убираем кнопки после их нажатия)
        //    await botClient.AnswerCallbackQueryAsync(callbackQuery.Id, cancellationToken: cancellationToken);

        //    // Убираем клавиатуру после нажатия
        //    var emptyKeyboard = new InlineKeyboardMarkup();
        //    await botClient.EditMessageReplyMarkupAsync(callbackQuery.Message.Chat.Id, callbackQuery.Message.MessageId, emptyKeyboard, cancellationToken: cancellationToken);
        //}

        //private async Task HandleMessageAsync(Message message, CancellationToken cancellationToken)
        //{
        //    if (message.Text != null)
        //    {
        //        await _botClient.SendTextMessageAsync(message.Chat.Id, "Вы отправили сообщение: " + message.Text, cancellationToken: cancellationToken);
        //    }
        //}

        //public async Task SendOrderRequestAsync(long chatId, string orderDetails, CancellationToken cancellationToken)
        //{
        //    // Создаем кнопки для принятия/отклонения
        //    var inlineKeyboard = new InlineKeyboardMarkup(new[]
        //    {
        //    new[]
        //    {
        //        InlineKeyboardButton.WithCallbackData("Принять", "accept"),
        //        InlineKeyboardButton.WithCallbackData("Отклонить", "reject")
        //    }
        //    });

        //    await _botClient.SendTextMessageAsync(chatId, $"Новый заказ: {orderDetails}", replyMarkup: inlineKeyboard, cancellationToken: cancellationToken);
        //}

        //// Обработчик ошибок
        //private Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        //{
        //    Console.WriteLine($"Ошибка: {exception.Message}");
        //    return Task.CompletedTask;
        //}
    }
}
