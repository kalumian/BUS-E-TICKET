using BUS_E_TICKET.Utilities;
using Core_Layer.Exceptions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
namespace BUS_E_TICKET.Extensions
{
    public static class CustomJwtAuthExtension
    {
        public static void AddCustomJwtAuth(this IServiceCollection services, ConfigurationManager configuration)
        {
            if (string.IsNullOrEmpty(ResponeHelper.GetTokenSecretKey(configuration)) ||
             string.IsNullOrEmpty(ResponeHelper.GetTokenIssuer(configuration)) ||
             string.IsNullOrEmpty(ResponeHelper.GetTokenAudience(configuration)))
            {
                throw new NotFoundException("JWT configuration is missing.");
            }
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o => { 
                o.RequireHttpsMetadata = false;
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters() { 
                ValidateIssuer = true,
                 ValidateLifetime = true,
                ValidIssuer =  ResponeHelper.GetTokenIssuer(configuration),
                ValidateAudience = true,
                ValidAudience = ResponeHelper.GetTokenAudience(configuration),
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ResponeHelper.GetTokenSecretKey(configuration)))
                };
            });

        }

        public static void AddSwaggarWithJtwConfig(this IServiceCollection services)
        {
 

            services.AddSwaggerGen(o => { 
                o.SwaggerDoc("v1", new OpenApiInfo() {
                    Version = "v1",
                    Title = "Test API",
                    Description = "This is a test API with JWT authentication",
                    Contact = new OpenApiContact
                    {
                        Name = "Al Mohamady",
                        Email = "ahmed@gmail.com",
                        Url = new Uri("https://mydomain")
                    }
                });
                // Configure JWT Bearer authentication
                o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your token in the text input below.\n\nExample: 'Bearer abc123def456'"
                });
                o.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new List<string>()
                    }
                });

            });
                
        }

    }
}
