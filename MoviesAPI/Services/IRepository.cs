using MoviesAPI.Entities;

namespace MoviesAPI.Services
{
    public interface IRepository
    {
        void AddGenre(Genre genre);
        List<Genre> GetAllGenres();
        Genre GetGenreById(int id);
    }
}
