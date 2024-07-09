using System.ComponentModel.DataAnnotations;

namespace RedisDemoApp.Models
{
    public class Platform
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
