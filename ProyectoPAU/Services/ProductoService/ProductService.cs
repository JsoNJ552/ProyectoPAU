using Microsoft.EntityFrameworkCore;
using ProyectoPAU.Models;
using ProyectoPAU.Services.ProductoService.ProductoService;

namespace ProyectoPAU.Services.ProductoService
{
    public class ProductService : IProductService
    {

        private readonly TiendauContext _context;
        public ProductService(TiendauContext context)
        {
            _context = context;
            

        }

        public Task EditarrProducto(Producto producto)
        {

            return Task.CompletedTask;
            
        }

        public async Task EliminarrProducto(int IdProducto)
        {
            try
            {

                var productoEliminar = await  _context.Productos.FindAsync(IdProducto);
                productoEliminar.Activo = false;
                await _context.SaveChangesAsync();

            }catch (Exception ex)
            {

                throw new NotImplementedException();

            }
        }

        public async Task<List<Producto>> obtenerProductosAsync()
        {
            try
            {
                List<Producto> ListaProductos = await _context.Productos
                    .Where(p => p.Activo == true)  // Filtro para obtener solo productos activos
                    .ToListAsync();

                return ListaProductos;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }
        }

        public Task<IEnumerable<Producto>> obtenerProductosFiltro(Func<Producto, bool> filtro = null)
        {
            throw new NotImplementedException();
        }

        public Producto obtenerProductosPorId(int idProducto)
        {
            try
            {
                var producto = _context.Productos
                    
                    .FirstOrDefault(p => p.IdProducto == idProducto);

                return producto;
            }
            catch (Exception ex)
            {
                // Aquí deberías manejar el error apropiadamente en lugar de lanzar una excepción no implementada.
                throw new Exception("Error al obtener el producto por ID.", ex);
            }
        }






        public async Task RegistrarProducto(Producto producto, IFormFile photoFile)
        {
            try
            {
                var filePath = Path.Combine("wwwroot", "images", photoFile.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await photoFile.CopyToAsync(stream);
                }
                byte[] photoBytes = await System.IO.File.ReadAllBytesAsync(filePath);
                string photoBase64 = Convert.ToBase64String(photoBytes);

                producto.Foto = photoBase64;
                using (var context = new TiendauContext())
                {
                    context.Add(producto);
                    await context.SaveChangesAsync();

                }
            }
            catch(Exception ex)
            {
                
            }
            
        }

        
    }
}
