using ScreenHandler.Configurators;
using ScreenHandler.Settings;

namespace ScreenHandler.Handlers;

public interface IMessageHandler
{
    void ShowMessage(string messageId);
}
