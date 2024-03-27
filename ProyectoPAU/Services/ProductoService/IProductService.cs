using ProyectoPAU.Models;


namespace ProyectoPAU.Services.ProductoService.ProductoService
{
    public interface IProductService
    {



        public Task RegistrarProducto(Producto producto, IFormFile photoFile);

        public Task<List<Producto>> obtenerProductosAsync();
        public Producto obtenerProductosPorId(int idProducto);

        public Task<IEnumerable<Producto>> obtenerProductosFiltro(Func<Producto, bool> filtro = null);

        public Task EditarrProducto(Producto producto);

        public Task EliminarrProducto(int IdProducto);





    }
}
