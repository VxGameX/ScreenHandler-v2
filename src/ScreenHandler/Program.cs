using ScreenHandler.ScreenHandler;

var builder = new ScreenHandlerBuilder();

builder.RegisterSingleScreen("/Users/omar.nunez/Projects/ScreenHandler/ScreenHandler/src/ScreenHandler/Test Files/form.json")
    .RegisterMultipleScreens("/Users/omar.nunez/Projects/ScreenHandler/ScreenHandler/src/ScreenHandler/Test Files/multipleScreens.json")
    .SetEntryPoint("mainScreen");

var app = builder.Build();

app.Start()
    .NextScreen("randomScreen")
    .NextScreen("entrevistadoScreen");
