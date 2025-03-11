
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Adrian_Hernandez_Bonilla_P2_Ap1.Models;

namespace Adrian_Hernandez_Bonilla_P2_Ap1.Services
{
    public class CiudadesServices(IDbContextFactory<Contexto> DbFactory)
    {
        private readonly IDbContextFactory<Contexto> _dbFactory = DbFactory;

        private async Task<bool> Existe(int ciudadId)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.ciudades.AnyAsync(c => c.CiudadId == ciudadId);
        }

        private async Task<bool> Insertar(Ciudades ciudad)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            contexto.ciudades.Add(ciudad);
            return await contexto.SaveChangesAsync() > 0;
        }

        private async Task<bool> Modificar(Ciudades ciudad)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            contexto.Entry(ciudad).State = EntityState.Modified;
            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<bool> Guardar(Ciudades ciudad)
        {
            if (ciudad.CiudadId == 0)
                return await Insertar(ciudad);
            else
                return await Modificar(ciudad);
        }

        public async Task<bool> Eliminar(int ciudadId)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            var ciudad = await contexto.ciudades.FindAsync(ciudadId);
            if (ciudad == null)
                return false;

            contexto.ciudades.Remove(ciudad);
            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<Ciudades?> Buscar(int ciudadId)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.ciudades.FindAsync(ciudadId);
        }

        public async Task<List<Ciudades>> Listar(Expression<Func<Ciudades, bool>> criterio)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.ciudades
                .Where(criterio)
                .ToListAsync();
        }
    }
}