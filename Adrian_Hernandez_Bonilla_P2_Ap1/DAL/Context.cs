using Adrian_Hernandez_Bonilla_P2_Ap1.Models;
using Microsoft.EntityFrameworkCore;
namespace Adrian_Hernandez_Bonilla_P2_Ap1.Context
{

    public class Contexto : DbContext
    {

        public Contexto(DbContextOptions<Contexto> options) : base(options) { }

        public DbSet<Modelo> modelos { get; set; }

    }


}