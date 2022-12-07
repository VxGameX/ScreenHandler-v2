using MVP.Configuration;

var builder = new ScreenHandlerBuilder();

builder.RegisterSingleScreen("/Users/omar.nunez/Downloads/MVP/singleScreen.json")
    .RegisterMultipleScreens("/Users/omar.nunez/Downloads/MVP/multipleScreens.json")
    .SetEntryPoint("mainScreen");

var screenHandler = builder.Build();

screenHandler.ShowScreen("mainScreen");
screenHandler.ShowScreen("randomScreen");
screenHandler.ShowScreen("entrevistadoScreen");
