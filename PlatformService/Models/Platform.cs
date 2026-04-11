using System.ComponentModel.DataAnnotations;

namespace PlatformsService.Models
{
    public class Platform
    {
        [Key]
        [Required]
        public int Id { get; set; }
        // The 'required' attribute indicates that this property must have a value when saving to the database
        [Required] 
        // The 'required' keyword indicates that this property must be set during object initialization
        public required string Name { get; set; }
        [Required]
        public int Publisher { get; set; }
        [Required]
        public int Cost { get; set; }

    }
}