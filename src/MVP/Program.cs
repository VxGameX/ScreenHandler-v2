using MVP.Configuration;


var screenHanlderBuilder = new ScreenHandlerBuilder();

screenHanlderBuilder.AddFile("/Users/omar.nunez/Downloads/MVP/configFile.json")
    .SetEntryPoint("{{EntryPoint}}");

var screenHandler = screenHanlderBuilder.Build();


screenHandler.ShowScreen("mainScreen");
