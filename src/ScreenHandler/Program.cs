using ScreenHandler.Handlers;

var ordersFormBuilder = FormHandler.CreateBuilder("/Users/omar.nunez/Projects/ScreenHandler/ScreenHandler/src/ScreenHandler/Test Files/form.json");
// ordersFormBuilder.SectionsSettings()
//     .SetEntryPoint("2")
//     .SetNextSection("3")
//     .SetNextSection("1")
//     .SaveOrderSettings();

var ordersForm = ordersFormBuilder.Build();

ordersForm.Run();

// ordersForm.ReviewAnswers();
