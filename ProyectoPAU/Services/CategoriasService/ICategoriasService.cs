using ProyectoPAU.Models;

namespace ProyectoPAU.Services.CategoriasService
{
    public interface ICategoriasService
    {

        public Task RegistrarProducto(CategoriaProducto categoria);

        public Task<List<CategoriaProducto>> obtenerProductosAsync();

        public Task<IEnumerable<CategoriaProducto>> obtenerProductosFiltro(Func<CategoriaProducto, bool> filtro = null);

        public Task EditarrProducto(CategoriaProducto categoria);

        public Task EliminarrProducto(CategoriaProducto categoria);

    }
}
