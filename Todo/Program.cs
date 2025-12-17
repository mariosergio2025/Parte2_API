using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using Todo.DbContexts;

namespace Todo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder();
            // 1 - Configurar a Injeção de Dependencia: sempre realizada no builder.Services
            builder.Services.AddControllers();
            builder.Services.AddDbContext<ApiDbContext>();
            builder.Services.AddSwaggerGen((options) => 
            {
                options.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "Primeira API",
                    Version = "v1",
                    Description = "Aplicativo para gerenciar uma lista de tarefas",
                    Contact = new OpenApiContact()
                    {
                        Name = "Paulo",
                        Email = "ada@ada.com.br",
                        Url = new Uri("https://paulo.bio")
                    },
                    License = new OpenApiLicense() 
                    {
                        Name = "MIT license",
                        Url = new Uri("https://paulo.bio")

                    },
                    TermsOfService =new Uri("https://exemplo.com")
                    
                 });

                var xmlFile = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
                options.IncludeXmlComments(xmlFile);
            });

            var app = builder.Build();
            app.UseSwagger();
            app.UseSwaggerUI();
            // 2 - Aplicar/ usar as dependencia 
            app.MapControllers();

            app.Run();

        }
    }
}
