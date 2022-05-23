using FluentValidation;

namespace LinkShortener.Web.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (ValidationException validationException)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(validationException.Message);
            }
        }
    }
}
