using SimpleTodoApi.Models;
using SimpleTodoApi.Models.DTO;

namespace SimpleTodoApi.ServiceContracts
{
    /// <summary>
    /// Defines the contract for managing todos
    /// </summary>
    public interface ITodoService
    {
        /// <summary>
        /// Adds todo object to the list of todos
        /// </summary>
        Task<Todo> AddTodo(TodoAddRequest? TodoAddRequest);

        /// <summary>
        /// GETs list of all todos
        /// </summary>
        Task<List<Todo>> GetAllTodos();

        /// <summary>
        /// Returns a todo object based on the given id
        /// </summary>
        Task<Todo?> GetTodoById(long id);

        /// <summary>
        /// Updates the specified todo details based on the given id
        /// </summary>
        Task<bool> UpdateTodo(long id, TodoUpdateRequest todoUpdateRequest);

        /// <summary>
        /// Deletes a todo based on the given id
        /// </summary>
        Task<bool> DeleteTodoById(long id);
    }
}