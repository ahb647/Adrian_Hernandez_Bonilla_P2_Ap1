using Adrian_Hernandez_Bonilla_P2_Ap1.Context;
using System.Linq.Expressions;


namespace Adrian_Hernandez_Bonilla_P2_Ap1.Services
{
    public class ModeloServices
    {

        private readonly Contexto _contexto;

        public ModeloServices(Contexto contexto)
        {
            _contexto = contexto;
        }



        public async Task<bool> Guardar(Modelo modelo)
        {


        }



        public async Task<bool> Existe(int Id)
        {

         

        }



        public async Task<bool> Insertar(Modelo modelo)
        {

          

        }



        public async Task<bool> Modificar(Modelo modelo)

        {

        }

        public async Task<Modelo> Buscar(int Id)
        {
        }



        public async Task<bool> Eliminar(int Id)
        {


        

        }




        public async Task<List<Modelo>> Listar(Expression<Func<Modelo, bool>> criterio)
        {


 
        }

    }
}