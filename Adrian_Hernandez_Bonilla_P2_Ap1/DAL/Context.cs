using Adrian_Hernandez_Bonilla_P2_Ap1.Models;
using Microsoft.EntityFrameworkCore;
namespace Adrian_Hernandez_Bonilla_P2_Ap1.Context
{

    public class Contexto : DbContext
    {

        public Contexto(DbContextOptions<Contexto> options) : base(options) { }

        public DbSet<Ciudades> ciudades { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Ciudades>().HasData(new List<Ciudades>()
        {
            new Ciudades(){CiudadId = 1, NombreCiudad = "Villa Arriba", Monto = 0 },
            new Ciudades(){CiudadId = 2, NombreCiudad = "San Francisco", Monto = 0 },            
            new Ciudades(){CiudadId = 3, NombreCiudad = "Cotui", Monto = 0 },       
            });
        }



    }


}