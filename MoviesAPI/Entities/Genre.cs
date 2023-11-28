using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        [StringLength(40)]
        public required string Name { get; set; }
    }
}
