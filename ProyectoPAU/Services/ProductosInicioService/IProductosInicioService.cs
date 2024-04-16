using ProyectoPAU.Models;

namespace ProyectoPAU.Services.ProductosInicioService
{
    public interface IProductosInicioService
    {

        public Task<List<Producto>> getProductosCategoria(string categoria);

        public Task<List<Producto>> getProductosCategoriaCelular();

        public Task<List<Producto>> getProductosCategoriaComputadora();

        public Task<List<Producto>> getProductosCategoriaRelojes();

        public Task<List<Producto>> getProductosCategoriaTelevision();
    }
}
