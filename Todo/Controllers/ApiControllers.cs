using Microsoft.AspNetCore.Mvc;

namespace Todo.Controllers
{
    [Route("/api/todo")]
    public class ApiControllers : ControllerBase
    {

        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok();

        }
        [HttpGet("{id}")]
        public ActionResult GetbyId(int id)
        {
            return Ok();

        }

        [HttpPost("{id}")]
        public ActionResult Create([FromBody] TodoItem item)
        {
            
            return Ok();

        }

        [HttpDelete("{id}")]
        public ActionResult DeletebyId(int id)
        {
            return Ok();

        }
        [HttpPut("{id}")]
        public ActionResult Updade(int id, TodoItem item)
        {
            return Ok();

        }

    }
}
