
using ProjectManager.Exceptions;

namespace ProjectManager.Middleware
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
			try
			{
				await next.Invoke(context);
			}
			catch (NotFoundException ex)
			{
				context.Response.ContentType = "text/plain";
				context.Response.StatusCode = 404;
				await context.Response.WriteAsync(ex.Message);
			}
            catch (Exception)
            {
                context.Response.ContentType = "text/plain";
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Internal server error");
            }
        }
    }
}
