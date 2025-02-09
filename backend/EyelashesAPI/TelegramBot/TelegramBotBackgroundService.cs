using EyelashesAPI.TelegramBot.Options;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace EyelashesAPI.TelegramBot
{
    public class TelegramBotBackgroundService : BackgroundService
    {
        private readonly ILogger<TelegramBotBackgroundService> _logger;
        private readonly ITelegramBotClient _botClient;
        private readonly IServiceProvider _serviceProvider;

        public TelegramBotBackgroundService(ITelegramBotClient botClient,
            ILogger<TelegramBotBackgroundService> logger,
            IServiceProvider serviceProvider)
        {
            _botClient = botClient;
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = Array.Empty<UpdateType>()
            };

            _logger.LogInformation("Телеграм-бот запущен...");

            await _botClient.ReceiveAsync(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandleErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: stoppingToken);
        }

        private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var handler = update switch
            {
                { Message: { } message } => HandleMessageAsync(message, cancellationToken),
                { CallbackQuery: { } callbackQuery } => HandleCallbackQueryAsync(callbackQuery, cancellationToken),
                _ => Task.CompletedTask
            };

            try
            {
                await handler;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при обработке обновления");
            }
        }

        private Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            string errorMessage = exception switch
            {
                ApiRequestException apiEx => $"Ошибка Telegram API: [{apiEx.ErrorCode}] {apiEx.Message}",
                _ => $"Произошла ошибка: {exception.Message}"
            };

            _logger.LogError(exception, errorMessage);

            return Task.CompletedTask;
        }

        private async Task HandleMessageAsync(Message message, CancellationToken cancellationToken)
        {
            if (message.Text is null)
                return;

            long chatId = message.Chat.Id;
            _logger.LogInformation("Получено сообщение: {Text} от {ChatId}", message.Text, chatId);

            try
            {
                switch (message.Text)
                {
                    case "Мои заказы за сегодня":
                        var orders = GetOrdersForToday(chatId);  // Метод для получения заказов за сегодня
                        var ordersMessage = orders.Any()
                            ? $"Ваши заказы на сегодня:\n{string.Join("\n", orders)}"
                            : "У вас нет заказов на сегодня.";

                        await _botClient.SendTextMessageAsync(chatId, ordersMessage, cancellationToken: cancellationToken);
                        break;

                    case "Другие опции":
                        await _botClient.SendTextMessageAsync(chatId, "Выберите другую опцию.", cancellationToken: cancellationToken);
                        break;

                    default:
                        // Отправка клавиатуры с кнопками
                        var replyKeyboard = new ReplyKeyboardMarkup(new[]
                        {
                            new KeyboardButton("Мои заказы за сегодня"), // Кнопка для получения заказов
                            new KeyboardButton("Другие опции") // Еще одна кнопка для других действий
                        })
                        {
                            ResizeKeyboard = true, // Делает клавиатуру компактной
                            OneTimeKeyboard = false // Не скрывать клавиатуру после первого нажатия
                        };

                        await _botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Выберите действие:",
                            replyMarkup: replyKeyboard,
                            cancellationToken: cancellationToken
                        );
                        break;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при обработке сообщения");
            }
        }

        private IEnumerable<string> GetOrdersForToday(long chatId)
        {
            // Здесь будет логика получения заказов за сегодня
            // Например, это может быть запрос к базе данных
            var orders = new List<string>
            {
                "Заказ 1: Услуга по наращиванию ресниц",
                "Заказ 2: Стрижка и укладка"
            };

            return orders; // В реальной ситуации это будет зависеть от вашей бизнес-логики
        }

        private async Task HandleCallbackQueryAsync(CallbackQuery callbackQuery, CancellationToken cancellationToken)
        {
            if (callbackQuery.Data is null)
                return;

            long chatId = callbackQuery.Message?.Chat.Id ?? 0;
            string action = callbackQuery.Data;

            _logger.LogInformation("Получен callback-запрос: {Action} от {ChatId}", action, chatId);

            try
            {
                await _botClient.AnswerCallbackQueryAsync(callbackQuery.Id, cancellationToken: cancellationToken);

                if (action == "confirm")
                {
                    await _botClient.SendTextMessageAsync(chatId, "Заявка подтверждена.", cancellationToken: cancellationToken);
                }
                else if (action == "cancel")
                {
                    await _botClient.SendTextMessageAsync(chatId, "Заявка отменена.", cancellationToken: cancellationToken);
                }

                await _botClient.EditMessageReplyMarkupAsync(
                    chatId: chatId,
                    messageId: callbackQuery.Message?.MessageId ?? 0,
                    replyMarkup: new InlineKeyboardMarkup(new InlineKeyboardButton[0]) // Пустая клавиатура
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при обработке callback-запроса");
            }
        }
    }
}
