namespace CompanyASP.Middlewares
{
    public class KeyMiddleware
    {
        private RequestDelegate _next;

        public KeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var key = context.Request.Query["key"];
            //if (key == "qwerty")
            {
                await _next.Invoke(context);
            }
        }
    }
}
