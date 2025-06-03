using System.ComponentModel.DataAnnotations;

namespace SimpleTodoApi.Models.DTO
{
    public class TodoAddRequest
    {
        [Required(ErrorMessage = "Name invalid")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Due date invalid")]
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }
        [Required(ErrorMessage = "Invalid - Please mark if completed")]
        public bool IsCompleted { get; set; }
    }    
}