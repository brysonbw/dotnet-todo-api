using SimpleTodoApi.Models;
using SimpleTodoApi.Models.DTO;
using SimpleTodoApi.RepositoryContracts;
using SimpleTodoApi.ServiceContracts;

namespace SimpleTodoApi.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepository;

        public TodoService(ITodoRepository ITodoRepository)
        {
            _todoRepository = ITodoRepository;
        }

        public async Task<Todo> AddTodo(TodoAddRequest? todoAddRequest)
        {
            // Validate request model
            if (todoAddRequest == null)
            {
                throw new ArgumentNullException(nameof(todoAddRequest));
            }
            if (todoAddRequest.Name == null)
            {
                throw new ArgumentException(nameof(todoAddRequest.Name));
            }

            return await _todoRepository.AddTodo(todoAddRequest);
        }

        public async Task<bool> UpdateTodo(long id, TodoUpdateRequest todoUpdateRequest)
        {
            return await _todoRepository.UpdateTodo(id, todoUpdateRequest);
        }

        public async Task<List<Todo>> GetAllTodos()
        {
            return await _todoRepository.GetAllTodos();
        }

        public async Task<Todo?> GetTodoById(long id)
        {
            return await _todoRepository.GetTodoById(id);
        }

        public async Task<bool> DeleteTodoById(long id)
        {
            return await _todoRepository.DeleteTodoById(id);
        }
    }
}