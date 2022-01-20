using System.ComponentModel.DataAnnotations;
using BusinessLogicLayer.Models.Base;

namespace BusinessLogicLayer.Models
{
    public class TodoItemDto: EntityDto<long>
    {
        [MaxLength(100)]
        [Required(ErrorMessage = "Name is required!")]
        public string Name { get; set; }
        
        public bool IsComplete { get; set; }
    }
}
