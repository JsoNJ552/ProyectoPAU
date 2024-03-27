using Microsoft.EntityFrameworkCore;
using ProyectoPAU.Models;

namespace ProyectoPAU.Services.CategoriasService
{
    public class CategoriasService : ICategoriasService
    {
        private readonly TiendauContext _context;

        public CategoriasService(TiendauContext context)
        {
            _context = context;
            

        }
        public Task EditarrProducto(CategoriaProducto categoria)
        {
            throw new NotImplementedException();
        }

        public Task EliminarrProducto(CategoriaProducto categoria)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CategoriaProducto>> obtenerProductosAsync()
        {
            try
            {
                List<CategoriaProducto> listaProducto = await _context.CategoriaProductos.ToListAsync();
                return listaProducto;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener productos: " + ex.Message);
                return new List<CategoriaProducto>(); // Devolver una lista vacía
            }
        }

        public Task<IEnumerable<CategoriaProducto>> obtenerProductosFiltro(Func<CategoriaProducto, bool> filtro = null)
        {
            throw new NotImplementedException();
        }

        public Task RegistrarProducto(CategoriaProducto categoria)
        {
            throw new NotImplementedException();
        }
    }
}
