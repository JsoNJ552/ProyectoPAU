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


        public async Task <List<Producto>> getProductosCategoriaCelular()
        {
            try
            {
              
                List <Producto>celulares  = await _context.Productos.Where(x => x.IdCategoriaNavigation.Nombre == "Celulares").ToListAsync();
                return celulares;

            }catch (Exception ex) { }

            return null;
        }

        public async Task<List<Producto>> getProductosCategoriaComputadora()
        {
            try
            {

                List<Producto> celulares = await _context.Productos.Where(x => x.IdCategoriaNavigation.Nombre == "Computadoras").ToListAsync();
                return celulares;

            }
            catch (Exception ex) { }

            return null;
        }
        [HttpGet]
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

        public async  Task<List<Producto>> getProductosCategoriaRelojes()
        {
            try
            {

                List<Producto> celulares = await _context.Productos.Where(x => x.IdCategoriaNavigation.Nombre == "Relojes").ToListAsync();
                return celulares;

            }
            catch (Exception ex) { }

            return null;
        }

        public async Task<List<Producto>> getProductosCategoriaTelevision()
        {
            try
            {

                List<Producto> celulares = await _context.Productos.Where(x => x.IdCategoriaNavigation.Nombre == "Television").ToListAsync();
                return celulares;

            }
            catch (Exception ex) { }

            return null;
        }
    }
}
