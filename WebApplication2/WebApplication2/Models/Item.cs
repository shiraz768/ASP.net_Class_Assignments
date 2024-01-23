using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebApplication2.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Name")]
        [Required]
        public string? Name { get; set; }
        [DisplayName("Category")]

        public string? Category { get; set; }
        [DisplayName("Description")]
        [Required]
        public string? Description { get; set; }
        [DisplayName("Image")]

        public string? Path { get; set; }
        [NotMapped]
        [DisplayName("Choose image")]
        public IFormFile image { get; set; }
    }
}
