using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using Todo.DbContexts;
using Todo.Middleware;

namespace Todo
{
    internal class Program
    {
        const string CORS_POLICY_NAME = "PoliticaPadrao"; //boa pratica para não ocorrer erro
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
            // 


            // CORS - CROSS ORIGIN RESOURCE SHARING - compartilha recurso entre sites diferentes
            builder.Services.AddCors(option =>
            {
                option.AddPolicy(CORS_POLICY_NAME, policy =>
                {
                    policy.AllowAnyHeader();
                    // policy.AllowAnyOrigin(); permite todas as origem para restringir
                    policy.WithOrigins("http://www.uol.com.br", "https://localhost/3000"); //coloca quantos quiser separado por virgulas

                    policy.AllowAnyMethod();
                });
            });

            var app = builder.Build();
            //Adicionando um Middleware
            app.UseMiddleware<MaintenanceMiddleware>();
            app.UseMiddleware<DemoMiddleware>();
            app.UseCors(CORS_POLICY_NAME);
            // para utilizar Swagger
            app.UseSwagger();
            app.UseSwaggerUI();
            // 2 - Aplicar/ usar as dependencia 
            app.MapControllers();

            app.Run();

        }
    }
}
