using System.ComponentModel.DataAnnotations;
using DataAccessLayer.Models.Base;

namespace DataAccessLayer.Models
{
    public class TodoItem : Entity<long>
    {
        [MaxLength(100)]
        [Required(ErrorMessage = "Name is required!")]
        public string Name { get; set; }
        
        public bool IsComplete { get; set; }
        
        public string Secret { get; set; }
    }
}