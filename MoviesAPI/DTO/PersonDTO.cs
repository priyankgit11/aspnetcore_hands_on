using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.DTO
{
    public class PersonDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Biography { get; set; }
        public DateTime DateOfBirty { get; set; }
        public string Picture { get; set; }
    }
}
