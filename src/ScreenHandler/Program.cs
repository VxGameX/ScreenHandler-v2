using ScreenHandler.Handlers;

var ordersFormBuilder = FormHandler.CreateBuilder("/Users/omar.nunez/Projects/ScreenHandler/ScreenHandler/src/ScreenHandler/Test Files/form.json");
ordersFormBuilder.

var ordersForm = ordersFormBuilder.Build();

ordersForm.Start();
