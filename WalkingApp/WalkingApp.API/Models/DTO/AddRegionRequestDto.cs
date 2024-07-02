using System.ComponentModel.DataAnnotations;

namespace WalkingApp.API.Models.DTO
{
    public class AddRegionRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage ="Code has to be 3 characters long!")]
        [MaxLength(3, ErrorMessage = "Code has to be 3 characters long!")]
        public string Code { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Name max length is 100 characters!")]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
