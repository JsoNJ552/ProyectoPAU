using ProyectoPAU.Models;


namespace ProyectoPAU.Services.ProductoService.ProductoService
{
    public interface IProductService
    {



        public Task RegistrarProducto(Producto producto, IFormFile photoFile);

        public Task<List<Producto>> obtenerProductosAsync();
        public Task<Producto> obtenerProductosPorId(int idProducto);

        public Task<List<Producto>> obtenerProductosPorNombreAsync(string nombre);

        public Task EditarrProducto(Producto producto, IFormFile photoFile);

        public Task EditarCantidadProductos(Producto producto);

        public Task EliminarrProducto(int IdProducto);

        public Task<bool> VerificarCantidadProducots(int IdProducto, int cantidadSolicitada);





    }
}
