using SimpleTodoApi.Models;
using SimpleTodoApi.Models.DTO;

namespace SimpleTodoApi.RepositoryContracts
{
  /// <summary>
  /// Represents data access logic for managing Todo entity
  /// </summary>
  public interface ITodoRepository
  {
    /// <summary>
    /// Adds a todo object to the data store
    /// </summary>
    Task<Todo> AddTodo(TodoAddRequest todo);


    /// <summary>
    /// Returns all todos in the data store
    /// </summary>
    Task<List<Todo>> GetAllTodos();


    /// <summary>
    /// Returns a todo object based on the given id
    /// </summary>
    Task<Todo> GetTodoById(long id);


    /// <summary>
    /// Deletes a todo object based on the todo id
    /// </summary>
    Task<bool> DeleteTodoById(long id);


    /// <summary>
    /// Updates a todo object (todo name and other details) based on the given todo id
    /// </summary>
    Task<bool> UpdateTodo(long id, TodoUpdateRequest todoUpdateRequest);
  }
}