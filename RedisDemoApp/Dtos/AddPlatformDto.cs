using System.ComponentModel.DataAnnotations;

namespace RedisDemoApp.Dtos
{
    public class AddPlatformDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
