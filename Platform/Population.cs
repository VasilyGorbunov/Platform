namespace Platform
{
    public class Population
    {
        private RequestDelegate? _next;

        public Population() { }

        public Population(RequestDelegate? next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string[] parts = context.Request.Path.ToString().Split('/', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 2 && parts[0].Equals("population"))
            {
                string city = parts[1];
                int? pop = 0;

                switch (city.ToLower())
                {
                    case "london":
                        pop = 8_136_000;
                        break;
                    case "paris":
                        pop = 2_141_000;
                        break;
                    case "monaco":
                        pop = 39_000;
                        break;
                }

                if(pop.HasValue)
                {
                    await context.Response.WriteAsync($"City: {city}, Population: {pop}");
                    return;
                }
            }

            if(_next != null)
            {
                await _next(context);
            }
        }
    }
}
