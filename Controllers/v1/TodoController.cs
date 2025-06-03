using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using SimpleTodoApi.Models;
using SimpleTodoApi.Models.DTO;
using SimpleTodoApi.ServiceContracts;

namespace SimpleTodoApi.Controllers.V1
{
    [ApiVersion("1.0")]
    public class TodosController : ApiControllerBase
    {
        private readonly ITodoService _todoService;

        public TodosController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        // GET: api/v1/Todos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Todo>>> GetAllTodos()
        {
            return await _todoService.GetAllTodos();
        }

        // GET: api/Todos/2
        [HttpGet("{id}")]
        public async Task<ActionResult<Todo>> GetTodo(long id)
        {
            var todo = await _todoService.GetTodoById(id);

            if (todo == null)
            {
                return NotFound();
            }

            return todo;
        }

        // PUT: api/Todos/2
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodo(long id, TodoUpdateRequest todoUpdateRequest)
        {
            if (todoUpdateRequest == null)
            {
                return BadRequest("FormBody empty");
            }

            var result = await _todoService.UpdateTodo(id, todoUpdateRequest);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/v1/Todos
        [HttpPost]
        public async Task<ActionResult<Todo>> CreateTodo(TodoAddRequest todoAddRequest)
        {
            var todo = await _todoService.AddTodo(todoAddRequest);

            if (todo == null)
            {
                return NotFound();
            }

            return CreatedAtAction(nameof(GetTodo), new { id = todo.Id }, todo);
        }

        // DELETE: api/v1/Todos/2
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(long id)
        {
            var result = await _todoService.DeleteTodoById(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}