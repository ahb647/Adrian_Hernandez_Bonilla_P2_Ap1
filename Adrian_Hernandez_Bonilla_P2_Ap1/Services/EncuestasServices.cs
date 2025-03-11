using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Adrian_Hernandez_Bonilla_P2_Ap1.Models;

namespace Adrian_Hernandez_Bonilla_P2_Ap1.Services
{
    public class EncuestasServices(IDbContextFactory<Contexto> DbFactory)
    {
        private async Task<bool> Existe(int EncuestaId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Encuestas.AnyAsync(E => E.EncuestaId == EncuestaId);
        }

        private async Task<bool> Insertar(Encuestas encuestas)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            await AfectarCiudad(encuestas.encuestasDetalle.ToArray()); // ✅ Siempre suma el monto
            contexto.Encuestas.Add(encuestas);
            return await contexto.SaveChangesAsync() > 0;
        }

        private async Task AfectarCiudad(EncuestasDetalle[] detalles)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();

            foreach (var detalle in detalles)
            {
                var ciudad = await contexto.ciudades.SingleOrDefaultAsync(c => c.CiudadId == detalle.CiudadId);
                if (ciudad != null)
                {
                    ciudad.Monto += detalle.Monto; // ✅ Siempre suma el monto
                }
            }

            await contexto.SaveChangesAsync();
        }

        private async Task<bool> Modificar(Encuestas encuestas)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();

            var encuestaOriginal = await contexto.Encuestas
                .Include(e => e.encuestasDetalle)
                .FirstOrDefaultAsync(e => e.EncuestaId == encuestas.EncuestaId);

            if (encuestaOriginal == null)
                return false;

            // Eliminamos la lógica que restaba los montos

            // Eliminar detalles que ya no existen en la nueva lista
            foreach (var detalleOriginal in encuestaOriginal.encuestasDetalle)
            {
                if (!encuestas.encuestasDetalle.Any(d => d.DetalleId == detalleOriginal.DetalleId))
                {
                    contexto.EncuestasDetalle.Remove(detalleOriginal);
                }
            }

            // ✅ Siempre suma los montos de los nuevos detalles
            await AfectarCiudad(encuestas.encuestasDetalle.ToArray());

            // Actualizar encuesta
            contexto.Entry(encuestaOriginal).CurrentValues.SetValues(encuestas);

            // Actualizar detalles
            foreach (var detalle in encuestas.encuestasDetalle)
            {
                var detalleExistente = encuestaOriginal.encuestasDetalle
                    .FirstOrDefault(d => d.DetalleId == detalle.DetalleId);

                if (detalleExistente != null)
                {
                    contexto.Entry(detalleExistente).CurrentValues.SetValues(detalle);
                }
                else
                {
                    encuestaOriginal.encuestasDetalle.Add(detalle);
                }
            }

            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<bool> Guardar(Encuestas encuestas)
        {
            if (!await Existe(encuestas.EncuestaId))
                return await Insertar(encuestas);
            else
                return await Modificar(encuestas);
        }

        public async Task<bool> Eliminar(int EncuestaId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            var encuesta = await contexto.Encuestas
                .Include(e => e.encuestasDetalle)
                .FirstOrDefaultAsync(e => e.EncuestaId == EncuestaId);

            if (encuesta == null)
                return false;

            // Eliminamos la lógica que restaba los montos

            contexto.EncuestasDetalle.RemoveRange(encuesta.encuestasDetalle);
            contexto.Encuestas.Remove(encuesta);

            var cantidad = await contexto.SaveChangesAsync();
            return cantidad > 0;
        }

        public async Task<Encuestas?> Buscar(int EncuestaId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Encuestas
                .Include(e => e.encuestasDetalle)
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.EncuestaId == EncuestaId);
        }

        public async Task<Encuestas?> BuscarConDetalle(int EncuestaId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Encuestas
                .Include(e => e.encuestasDetalle)
                .ThenInclude(d => d.Ciudades)
                .FirstOrDefaultAsync(e => e.EncuestaId == EncuestaId);
        }

        public async Task<List<Encuestas>> Listar(Expression<Func<Encuestas, bool>> criterio)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Encuestas
                .Include(e => e.encuestasDetalle)
                .ThenInclude(d => d.Ciudades)
                .AsNoTracking()
                .Where(criterio)
                .ToListAsync();
        }
    }
}