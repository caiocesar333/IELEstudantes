using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using IEL.Models;
using System.Collections.Generic;

namespace IEL.Models
{
    public class EstudantesDbContext : IdentityDbContext<ApplicationUser>
    {
        public EstudantesDbContext(DbContextOptions<EstudantesDbContext> options) : base(options)
        {
        }

        public DbSet<Estudante> Estudantes { get; set; }
    }
}
