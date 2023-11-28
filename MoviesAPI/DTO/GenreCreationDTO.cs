using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.DTO
{
    public class GenreCreationDTO
    {
        [Required(ErrorMessage = "The field with name {0} is required")]
        [StringLength(40)]
        public required string Name { get; set; }
    }
}
