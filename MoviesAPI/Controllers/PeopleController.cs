using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.DTO;
using MoviesAPI.Entities;

namespace MoviesAPI.Controllers
{
    [Route("api/people")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ApplicationDbContext context;
        public PeopleController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<List<PersonDTO>> Get()
        {
            var people = await context.People.ToListAsync();
            var peopleDTO = mapper.Map<List<PersonDTO>>(people);
            return peopleDTO;
        }
        [HttpGet("{id}", Name = "getPerson")]
        public async Task<ActionResult<PersonDTO>> Get(int id)
        {
            var person = await context.People.FirstOrDefaultAsync(x => x.Id == id);
            if (person == null) return NotFound();
            var personDTO = mapper.Map<PersonDTO>(person);
            return Ok(personDTO);
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PersonCreationDTO personCreationDTO)
        {
            var person = mapper.Map<Person>(personCreationDTO);
            context.Add(person);
            await context.SaveChangesAsync();
            var personDTO = mapper.Map<PersonDTO>(person);
            return new CreatedAtRouteResult("getPerson", new { id = person.Id }, personDTO);
        }
        [HttpPut("${id}")]
        public async Task<ActionResult> Put(int id, [FromBody] PersonCreationDTO personCreation)
        {
            var person = mapper.Map<Person>(personCreation);
            person.Id = id;
            context.Entry(person).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var person = await context.People.FirstOrDefaultAsync(gen => gen.Id == id);
            if (person  == null) return NotFound();
            context.Remove(person);
            await context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPatch("${id}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<PersonPatchDTO> patchDocument)
        {
            if(patchDocument == null) return BadRequest();
            var entityFromDB = await context.People.FirstOrDefaultAsync(x=>x.Id == id);
            if(entityFromDB == null) return NotFound();
            var entityDTO = mapper.Map<PersonPatchDTO> (entityFromDB);
            patchDocument.ApplyTo(entityDTO, ModelState);
            var isValid = TryValidateModel(entityDTO);
            if(!isValid) return BadRequest(ModelState);
            mapper.Map(entityDTO, entityFromDB);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
