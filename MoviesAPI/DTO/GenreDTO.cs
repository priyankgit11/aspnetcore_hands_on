using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.DTO
{
    public class GenreDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }
}
