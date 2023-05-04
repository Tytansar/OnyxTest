using Microsoft.AspNetCore.Localization;
using ToolsBazaar.Domain.CustomerAggregate;
using ToolsBazaar.Domain.ProductAggregate;
using ToolsBazaar.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();

var app = builder.Build();

app.UseRouting();

var requestCulture = new RequestCulture("en-US");
requestCulture.Culture.DateTimeFormat.ShortDatePattern = "MM-dd-yyyy";
app.UseRequestLocalization(new RequestLocalizationOptions
                           {
                               DefaultRequestCulture = requestCulture
                           });

app.MapControllerRoute("default",
                       "{controller}/{action=Index}/{id?}");

app.Run();