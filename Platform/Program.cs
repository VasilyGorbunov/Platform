using Platform;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


//app.Use(async (context, next) =>
//{
//    await next();
//    await context.Response.WriteAsync($"\nStatusCode: {context.Response.StatusCode}");
//});

//app.Use(async (context, next) =>
//{
//    if (context.Request.Path == "/short")
//    {
//        await context.Response.WriteAsync($"Request Short Circuited");
//    } 
//    else
//    {
//        await next();
//    }
//});

//app.Use(async (context, next) =>
//{
//    if(context.Request.Method == HttpMethods.Get && context.Request.Query["custom"] == "true")
//    {
//        context.Response.ContentType = "text/plain";
//        await context.Response.WriteAsync("Custom middleware \n");
//    }

//    await next();
//});

app.Map("/branch", branch =>
{
    branch.UseMiddleware<QueryStringMiddleware>();

    branch.Run(async (context) =>
    {
        await context.Response.WriteAsync($"Branch Middleware");
    });

    //branch.Use(async (HttpContext context, Func<Task> next) =>
    //{
    //    await context.Response.WriteAsync($"Branch Middleware");
    //});
});

app.UseMiddleware<QueryStringMiddleware>();

app.MapGet("/", () => "Hello World!");

app.Run();
