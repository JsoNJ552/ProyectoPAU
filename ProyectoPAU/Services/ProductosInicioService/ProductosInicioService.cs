using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoPAU.Models;

namespace ProyectoPAU.Services.ProductosInicioService
{
    public class ProductosInicioService : IProductosInicioService
    {
        private readonly TiendauContext _context;

        public ProductosInicioService(TiendauContext context)
        {
            _context = context;
                
        }


  

       
     
        public async  Task<List<Producto>> getProductosCategoria(string categoria)
        {
            try
            {
                var producto = await _context.Productos.Where(x=> x.IdCategoriaNavigation.Nombre == categoria && x.Activo == true).ToListAsync();
                return producto;
            }catch (Exception ex) {

                return null;
            }
        }


        public async Task<List<Producto>> obtenerTodosProductosPorNombreAsync(string nombre)
        {
            try
            {
                // Consulta todos los productos cuyo nombre, marca o pertenezcan a una categoría contenga la cadena especificada
                var productos = await _context.Productos
                    .Where(p => p.Nombre.Contains(nombre) || p.Marca.Contains(nombre)  ||
                                p.IdCategoriaNavigation.Nombre.Contains(nombre) )
                    .ToListAsync();

                return productos;
            }
            catch (Exception ex)
            {
                // Manejo de excepciones si es necesario
                throw new Exception("Ocurrió un error al obtener los productos.", ex);
            }
        }


        public async Task<List<Producto>> getProductosAcAndInac( )
        {
            try
            {
                var producto = await _context.Productos.ToListAsync();
                return producto;
            }
            catch (Exception ex)
            {

                return null;
            }
        }


    }
}
