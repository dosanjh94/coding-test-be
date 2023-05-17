using Microsoft.AspNetCore.Authentication;
using AspNetCore.Authentication.ApiKey;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using ToolsBazaar.Domain;
using ToolsBazaar.Domain.CustomerAggregate;
using ToolsBazaar.Domain.OrderAggregate;
using ToolsBazaar.Domain.ProductAggregate;
using ToolsBazaar.Domain.Services;
using ToolsBazaar.Persistence;
using ToolsBazaar.Web.Authentication;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<ICustomerSpendingService, CustomerSpendingService>();

var app = builder.Build();

app.UseRouting();

var requestCulture = new RequestCulture("en-US");
requestCulture.Culture.DateTimeFormat.ShortDatePattern = "MM-dd-yyyy";
app.UseRequestLocalization(new RequestLocalizationOptions
                           {
                               DefaultRequestCulture = requestCulture
                           });

app.UseMiddleware<ApiKeyAuthMiddleware>();
app.UseAuthorization();

app.MapControllerRoute("default",
                       "{controller}/{action=Index}/{id?}");

app.Run();