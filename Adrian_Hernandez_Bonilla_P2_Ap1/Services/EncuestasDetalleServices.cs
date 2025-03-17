using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Adrian_Hernandez_Bonilla_P2_Ap1.Models;

namespace Adrian_Hernandez_Bonilla_P2_Ap1.Services
{
    public class EncuestasDetalleService(IDbContextFactory<Contexto> DbFactory)
    {
        public async Task<List<Ciudades>> Listar(Expression<Func<Ciudades, bool>> criterio)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.ciudades
                .AsNoTracking()
                .Where(criterio)
                .ToListAsync();
        }

        public async Task<bool> Eliminar(int detalleId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            var detalle = await contexto.EncuestasDetalle
                .Include(d => d.Encuestas)
                .FirstOrDefaultAsync(d => d.DetalleId == detalleId);

            if (detalle != null)
            {
                await AfectarMonto(detalle, false);
                await RevertirAfectarCiudad(detalle);
                contexto.EncuestasDetalle.Remove(detalle);
                await contexto.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task AfectarMonto(EncuestasDetalle detalle, bool Suma)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            var encuesta = await contexto.Encuestas
                .FirstOrDefaultAsync(e => e.EncuestaId == detalle.EncuestaId);

            if (encuesta != null)
            {
                if (Suma)
                {
                    encuesta.Monto -= detalle.Monto;
                }
                else
                {
                    encuesta.Monto += detalle.Monto;
                }

                await contexto.SaveChangesAsync();
            }
        }

        public async Task RevertirAfectarCiudad(EncuestasDetalle detalle)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            var ciudad = await contexto.ciudades.SingleOrDefaultAsync(c => c.CiudadId == detalle.CiudadId);
            if (ciudad != null)
            {
                ciudad.Monto -= detalle.Monto;
                await contexto.SaveChangesAsync();
            }
        }
    }
}
