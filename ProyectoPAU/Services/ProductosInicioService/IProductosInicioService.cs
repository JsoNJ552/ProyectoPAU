using Microsoft.AspNetCore.Mvc;
using ProyectoPAU.Models;

namespace ProyectoPAU.Services.ProductosInicioService
{
    public interface IProductosInicioService
    {

        public Task<List<Producto>> getProductosCategoria(string categoria);

        public  Task<List<Producto>> getProductosAcAndInac();

        public Task<List<Producto>> obtenerTodosProductosPorNombreAsync(string nombre);

    }
}
