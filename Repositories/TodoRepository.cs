using SimpleTodoApi.RepositoryContracts;
using SimpleTodoApi.DatabaseContext;
using SimpleTodoApi.Models;
using SimpleTodoApi.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace SimpleTodoApi.Repository
{
    public class TodoRepository : ITodoRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TodoRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Todo> AddTodo(TodoAddRequest todoAddRequest)
        {
            var todo = new Todo
            {
                Name = todoAddRequest.Name,
                DueDate = todoAddRequest.DueDate,
                IsCompleted = todoAddRequest.IsCompleted
            };

            try
            {
                _dbContext.Todos.Add(todo);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Create failed", ex);
            }

            return todo;
        }

        public async Task<List<Todo>> GetAllTodos()
        {
            return await _dbContext.Todos.ToListAsync();
        }

        public async Task<Todo> GetTodoById(long id)
        {
            var todo = await _dbContext.Todos.FindAsync(id);

            return todo ?? throw new KeyNotFoundException($"Object [{id}] not found");
        }

        public async Task<bool> UpdateTodo(long id, TodoUpdateRequest todoUpdateRequest)
        {

            var existingTodo = await _dbContext.Todos.FindAsync(id);
            int propertiesModified = 0;

            if (existingTodo == null)
            {
                throw new KeyNotFoundException($"Object [{id}] not found");
            }

            // Apply updates to todo/properties (if valid)
            if (!string.IsNullOrEmpty(todoUpdateRequest!.Name) && todoUpdateRequest.Name != existingTodo.Name)
            {
                existingTodo.Name = todoUpdateRequest.Name;
                _dbContext.Entry(existingTodo).Property(todo => todo.Name).IsModified = true;
                propertiesModified++;
            }

            if (todoUpdateRequest.DueDate.HasValue && todoUpdateRequest.DueDate.Value != existingTodo.DueDate)
            {
                existingTodo.DueDate = todoUpdateRequest.DueDate.Value;
                _dbContext.Entry(existingTodo).Property(todo => todo.DueDate).IsModified = true;
                propertiesModified++;
            }

            if (todoUpdateRequest.IsCompleted.HasValue && todoUpdateRequest.IsCompleted != existingTodo.IsCompleted)
            {
                existingTodo.IsCompleted = todoUpdateRequest.IsCompleted.Value;
                _dbContext.Entry(existingTodo).Property(todo => todo.IsCompleted).IsModified = true;
                propertiesModified++;
            }

            if (propertiesModified > 0)
            {
                try
                {
                    await _dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    throw new Exception("Update failed", ex);
                }
            }
           
            return true;
        }

        public async Task<bool> DeleteTodoById(long id)
        {
            var todo = await _dbContext.Todos.FindAsync(id);

            if (todo == null)
            {
                throw new KeyNotFoundException($"Object [{id}] not found");
            }

            try
            {
                _dbContext.Todos.Remove(todo);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                throw new Exception("Remove failed", ex);
            }
        }
    }
}