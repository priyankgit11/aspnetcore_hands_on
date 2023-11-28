using AutoMapper;
using MoviesAPI.DTO;
using MoviesAPI.Entities;

namespace MoviesAPI.Helpers
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles() 
        {
            CreateMap<Genre, GenreDTO>().ReverseMap();
            CreateMap<GenreCreationDTO, Genre>();
            CreateMap<Person,PersonDTO>().ReverseMap();
            CreateMap<PersonCreationDTO, Person>();
            CreateMap<Person,PersonPatchDTO>().ReverseMap();
        }
    }
}
