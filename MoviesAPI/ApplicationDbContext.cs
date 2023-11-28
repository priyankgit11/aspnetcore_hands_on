using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Diagnostics.CodeAnalysis;
using MoviesAPI.Entities;

namespace MoviesAPI
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext([NotNullAttribute] DbContextOptions options) : base(options) 
        {
            
        }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Person> People { get; set; }
    }
}
