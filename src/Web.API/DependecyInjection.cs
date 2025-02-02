using Microsoft.OpenApi.Models;
using Web.API.Middlewares;

namespace Web.API;

public static class DependecyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddCors(options => options.AddPolicy("MyAllowSpecificOrigins", builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .WithExposedHeaders("Content-Disposition", "downloadfilename");
        }));

        services.AddControllers();
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(options => 
        {
            var jwtSecurityScheme = new OpenApiSecurityScheme
            {
                BearerFormat = "JWT",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Description = "Please insert JWT with Bearer into field",
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            };
            options.AddSecurityDefinition("Bearer", jwtSecurityScheme);
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { jwtSecurityScheme , Array.Empty<string>() }
            });
        });

        services.AddTransient<GloblalExceptionHandlingMiddleware>();

        return services;
    }
}
