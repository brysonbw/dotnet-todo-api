
using System.ComponentModel.DataAnnotations;

namespace SimpleTodoApi.Models.DTO
{
  public class TodoUpdateRequest
  {
    public string? Name { get; set; } 
    public DateTime? DueDate { get; set; }
    public bool? IsCompleted { get; set; }
  }
}