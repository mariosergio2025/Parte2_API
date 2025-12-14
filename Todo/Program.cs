using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Todo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder();
            // 1 - Configurar a Injeção de Dependencia: sempre realizada no builder.Services
            builder.Services.AddControllers();


            var app = builder.Build();
            // 2 - Aplicar/ usar as dependencia 
            app.MapControllers();

            app.Run();

        }
    }
}
