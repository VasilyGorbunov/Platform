namespace Platform
{
    public class QueryStringMiddleware
    {
        private RequestDelegate? _next;

        public QueryStringMiddleware() { }

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

            if(_next != null)
            {
                await _next(context);
            }       
        }
    }

    public class LocationMiddleware
    {
        private RequestDelegate _next;
        private MessageOptions _options;

        public LocationMiddleware(RequestDelegate next, MessageOptions options)
        {
            _next = next;
            _options = options;
        }

        public async Task Invoke(HttpContext context)
        {
            if(context.Request.Path == "/location")
            {
                await context.Response.WriteAsync($"{_options.CityName}, {_options.CountryName}");
            }
            else
            {
                await _next(context);
            }
        }
    }
}
