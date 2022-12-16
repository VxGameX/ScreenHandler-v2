using ScreenHandler.Settings;

namespace ScreenHandler.Handlers;

public class MessageHandler : IMessageHandler
{
    public Message Message { get; set; } = null!;

    public static IMessageHandlerBuilder CreateBuilder(string path) => new MessageHandlerBuilder(path);

    public void ShowMessage(string messageId)
    {
        
    }
}
