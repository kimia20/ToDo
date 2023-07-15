using Infrastructure.Identity.DbContext;
using Infrastructure.Persistence.Commons;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebEndpoints.ToDo.Api.Extensions.Middleware;

namespace WebEndpoints.ToDo.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void CustomExceptionMiddleware(this IApplicationBuilder app) =>
          app.UseMiddleware<ExceptionMiddleware>();
        public static IApplicationBuilder UseCustomizedDataStore(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            scope.ServiceProvider.GetService<ToDoDbContext>()?.Database.Migrate();
            return app;
        }
        public static IApplicationBuilder UseCustomizedSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger()
                .UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint($"/swagger/v1/swagger.json", "ToDo V1");
                });
            return app;
        }
        public static IApplicationBuilder UseCustomizedCors(this IApplicationBuilder app)
        {
            app.UseCors(cors =>
                            cors
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .SetIsOriginAllowed(_ => true)
                            .AllowCredentials()
                        );
            return app;
        }

        public static void UpdateDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope();
            using var toDoDbContext = serviceScope.ServiceProvider.GetService<ToDoDbContext>();
            toDoDbContext.Database.Migrate();
            //using var logContext = serviceScope.ServiceProvider.GetService<LogDbContext>();
            //logContext.Database.Migrate();
            using var IdentityContext = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
            IdentityContext.Database.Migrate();
        }
    }
}
