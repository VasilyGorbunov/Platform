using Microsoft.Extensions.Options;
using Platform;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MessageOptions>(options =>
{
    options.CityName = "Krasnoyarsk";
});

var app = builder.Build();

app.UseMiddleware<LocationMiddleware>();

//app.MapGet("/location", async (HttpContext context, IOptions<MessageOptions> msgOpts) =>
//{
//    MessageOptions opts = msgOpts.Value;
//    await context.Response.WriteAsync($"{opts.CityName}, {opts.CountryName}");
//});


app.MapGet("/", () => "Hello World!");

app.Run();
