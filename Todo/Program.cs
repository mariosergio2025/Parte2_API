using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
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


            var app = builder.Build();
            // 2 - Aplicar/ usar as dependencia 
            app.MapControllers();

            app.Run();

        }
    }
}
