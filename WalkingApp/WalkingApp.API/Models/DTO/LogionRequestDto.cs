using System.ComponentModel.DataAnnotations;

namespace WalkingApp.API.Models.DTO
{
    public class LogionRequestDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
