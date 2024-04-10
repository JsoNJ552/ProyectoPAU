using Microsoft.EntityFrameworkCore;
using ProyectoPAU.Models;
using ProyectoPAU.Services.ProductoService.ProductoService;
using System.Linq;

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

        public async Task<List<Producto>> obtenerProductosPorNombreAsync(string nombre)
        {
            try
            {
                // Consulta todos los productos cuyo nombre, marca o pertenezcan a una categoría contenga la cadena especificada
                var productos = await _context.Productos
                    .Where(p => p.Nombre.Contains(nombre) && p.Activo == true || p.Marca.Contains(nombre)  && p.Activo == true ||
                                p.IdCategoriaNavigation.Nombre.Contains(nombre) && p.Activo == true)
                    .ToListAsync();

                return productos;
            }
            catch (Exception ex)
            {
                // Manejo de excepciones si es necesario
                throw new Exception("Ocurrió un error al obtener los productos.", ex);
            }
        }

        public Producto obtenerProductosPorId(int idProducto)
        {
            try
            {
                var producto = _context.Productos
                    
                    .FirstOrDefault(p => p.IdProducto == idProducto );

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
                
                    _context.Add(producto);
                    await _context.SaveChangesAsync();

                
            }
            catch (Exception ex)
            {

            }

        }

        public async Task <bool> VerificarCantidadProducots(int IdProducto)
        {
            try
            {

                var cantidad =  await _context.Productos.Where(x => x.Cantidad > 0 && x.IdProducto == IdProducto).FirstOrDefaultAsync();

                if(cantidad != null) {

                    return true;
                
                }

            }catch (Exception ex) {

                return false;
            
            }

            return false;


        }
    }
}
