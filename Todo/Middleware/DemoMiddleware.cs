using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Todo.Middleware
{
    // O que faz uma classe ser um middleware?
    // 1 - ter um construtor que recebe um RequestDelegate - Referenca para o proximo gominho
    // 2 - ter um metodo chamado InvokeAsync que recebe um HttpContext como parametro
    public class DemoMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<DemoMiddleware> logger;

        public DemoMiddleware(RequestDelegate next, ILogger<DemoMiddleware> logger) 
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Adiciona a logica do Middleware
            // - Verificar se o usuario tem a api contratada no plano dele
            // - verificar se o dado já esta cache

            // antes a requisição para proximo
            logger.LogError("logica que é executada antes que a request seguir para o proximo");

            await next(context);
            // depois que a requisição voltar para o proximo
            logger.LogError("logica que é executada depois, quando a request volta");

        }
    }
}
