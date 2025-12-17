using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Mime;
using Todo.DbContexts;

namespace Todo.Controllers
{
    [Route("/api/todo")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public class ApiControllers : ControllerBase
    {
        private readonly ApiDbContext apiDbContext;

        public ApiControllers(ApiDbContext apiDbContext) 
        {
        this.apiDbContext = apiDbContext;
        }
        /// <summary>
        /// Retorna uma lista com todas as tarefas
        /// </summary>
        /// <reponse code="200">Requisição bem Sucessidida, retorna lista de tarefas</reponse>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TodoItem>))]
        public ActionResult GetAll()
        {
            return Ok(apiDbContext.Todos);

        }
        /// <summary>
        /// Retorna os dados detalhados de uma tarefa em particular
        /// </summary>
        /// <param name="id">Ídentifica a Todo Item cujos dados devem ser retornados</param>
        /// <reponse code="404">Requisição bem Sucessidida, Id não encontrado</reponse>
        /// <reponse code="200">Requisição bem Sucessidida, retorna a tarefa de Id tarefas</reponse>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TodoItem))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(void))]
        public ActionResult GetbyId(int id)
        {
            var todo = apiDbContext.Todos.Find(id);

            if (todo == null)
                return NotFound();

            return Ok(todo);

        }
        /// <summary>
        /// Acrescenta um item na lista de tarefas
        /// </summary>
        /// <param name="item">Dados da tarefa a ser criada</param>
        /// <reponse code="201">Requisição bem Sucessidida</reponse>
        /// <reponse code="409">Conflito</reponse>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created,Type = typeof(TodoItem))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(void))]
        public ActionResult Create([FromBody] TodoItem item)
        {

            apiDbContext.Todos.Add(item);
            apiDbContext.SaveChanges();

            return CreatedAtAction(nameof(GetbyId), new { id = item.Id, item });

        }
        /// <summary>
        /// Delete uma tarefa especificada
        /// </summary>
        /// <param name="id">Id da tarefa para apagar</param>
        /// <response code="204">Tarefa removida com sucesso</response>
        /// <response code="404">Tarefa não encontrada</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(void))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(void))]
        public ActionResult DeletebyId(int id)
        {
            var todo = apiDbContext.Todos.Find(id);
            if (todo == null)
                return NotFound();
            apiDbContext.Todos.Remove(todo);
            apiDbContext.SaveChanges();
            return NoContent();

        }
        /// <summary>
        /// Atualiza os dados de uma tarefa
        /// </summary>
        /// <param name="id">Id do Todo Item a ser atualizado</param>
        /// <param name="item">Novo dados do Todo Item</param>
        /// <response code="400">Os Ids não correspondem</response>
        /// <response code="404">Tarefa não encontrada</response>
        /// <response code="204">Tarefa realizada com sucesso</response>
        /// 
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type=typeof(void))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(void))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(void))]
        public ActionResult Updade(int id, [FromBody] TodoItem item)
        {
            if (id != item.Id)
                return BadRequest();
            var existing = apiDbContext.Todos.Find(id);
            if (existing == null)
                return NotFound();
            existing.Title = item.Title;
            existing.Description = item.Description;
            existing.IsFinished = item.IsFinished;

            apiDbContext.Todos.Update(existing);
            apiDbContext.SaveChanges();

            return NoContent();

        }

    }
}
