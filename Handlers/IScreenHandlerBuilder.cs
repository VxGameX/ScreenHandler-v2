using ConsoleScreenHandler.Models;

namespace ConsoleScreenHandler.Handlers;

public interface IScreenHandlerBuilder
{
    IScreenHandler Build();
    IScreenHandlerBuilder ParseScreen(string screenPath);
    IScreenHandlerBuilder SetAnswerValidation(Func<Section, string, bool> answerValidation);
    IScreenHandlerBuilder SetLabelOutput(Func<Section, string> labelOutput);
    IScreenHandlerBuilder SetNotValidAnswerResponse(Func<Section, string, string> notValidAnswerResponse);
    IScreenHandlerBuilder SetForegroundColor(ConsoleColor foregroundColor);
    IScreenHandlerBuilder SetBackgroundColor(ConsoleColor backgroundColor);
    IScreenHandlerBuilder SetScreenPause(System.Action screenPause);
    IScreenHandlerBuilder SetTitleDisplay(Func<string, string> titleDisplay);
}
