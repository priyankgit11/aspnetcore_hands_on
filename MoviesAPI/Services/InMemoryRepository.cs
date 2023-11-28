using System.Collections.Generic;
using MoviesAPI.Entities;
namespace MoviesAPI.Services
{
    public class InMemoryRepository : IRepository
    {
        private List<Genre> _genres;
        private int _count;
        public InMemoryRepository()
        {
            _genres = new List<Genre>()
            {
                new Genre() { Id = 1, Name = "Comedy" },
                new Genre() { Id = 2, Name = "Action" }
            };
            _count = 12;
        }
        public List<Genre> GetAllGenres()
        {
            return _genres;
        }
        public Genre GetGenreById(int id)
        {
            var genre = _genres.Find(x => x.Id == id);
            return genre;
        }
        public void AddGenre(Genre genre)
        {
            _genres.Add(genre);
        }
    }
}
