using GET_Biblioteka.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GET_Biblioteka.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Knjiga> Knjige { get; set; }
        public DbSet<IznajmljenaKnjiga> IznajmljeneKnjige { get; set; }
        public DbSet<Rezervacija> Rezervacije { get; set; }
    }
}