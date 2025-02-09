
namespace EyelashesAPI.TelegramBot
{
    public interface ITelegramMessageService
    {
        Task SendMessageAsync(long chatId, string message, CancellationToken cancellationToken, bool isOrderRequest = false, string orderDetails = "");
    }
}