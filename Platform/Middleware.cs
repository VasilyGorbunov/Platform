namespace Platform
{
    public class QueryStringMiddleware
    {
        private RequestDelegate _next;

        public QueryStringMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Method == HttpMethods.Get && context.Request.Query["custom"] == "true")
            {
                if(!context.Response.HasStarted)
                {
                    context.Response.ContentType = "text/plain";
                }
                
                await context.Response.WriteAsync("Class middleware \n");
            }

            await _next(context);
        }
    }
}
