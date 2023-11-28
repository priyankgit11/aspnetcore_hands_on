using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Services;
using MoviesAPI.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.DTO;
using AutoMapper;

namespace MoviesAPI.Controllers
{
    [Route("api/genres")]
    public class GenresController : ControllerBase
    {

        public ApplicationDbContext Context { get; }
        private readonly IMapper mapper;

        public GenresController(ApplicationDbContext context, IMapper mapper)
        {
            Context = context;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<List<GenreDTO>> Get()
        {
            var genre = await Context.Genres.ToListAsync();
            var genresDTO = mapper.Map<List<GenreDTO>>(genre);
            return genresDTO;
        }
        [HttpGet("{id:int}", Name = "getGenre")]
        public async Task<ActionResult<GenreDTO>> Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var genre = await Context.Genres.FirstOrDefaultAsync(x => x.Id == id);
            if (genre == null)
            {
                return NoContent();
            }
            var genreDTO = mapper.Map<GenreDTO>(genre);
            return Ok(genre);
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] GenreCreationDTO genreCreation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var genre = mapper.Map<Genre>(genreCreation);
            Context.Add(genre);
            await Context.SaveChangesAsync();
            var genreDTO = mapper.Map<GenreDTO>(genre);
            return new CreatedAtRouteResult("getGenre", new { Id = genreDTO.Id }, genreDTO);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id,[FromBody] GenreCreationDTO genreCreation)
        {
            var genre = mapper.Map<Genre>(genreCreation);
            genre.Id = id;
            Context.Entry(genre).State = EntityState.Modified;
            await Context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var genre = await Context.Genres.FirstOrDefaultAsync(gen => gen.Id == id);
            if(genre==null) return NotFound();
            Context.Remove(genre);
            await Context.SaveChangesAsync();
            return NoContent();
        }
    }
}
