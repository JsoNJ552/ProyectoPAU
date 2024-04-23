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

        public  async Task EditarrProducto(Producto producto, IFormFile photoFile)
        {

            try
            {
                if(photoFile != null)
                {

                    var filePath = Path.Combine("wwwroot", "images", photoFile.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await photoFile.CopyToAsync(stream);
                    }
                    byte[] photoBytes = await System.IO.File.ReadAllBytesAsync(filePath);
                    string photoBase64 = Convert.ToBase64String(photoBytes);

                    producto.Foto = photoBase64;

                }
               


               
                    _context.Productos.Update(producto);
                    await _context.SaveChangesAsync();  
            

            }catch (Exception ex) { 
            
            }


            
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


		public async Task<int> obtenerCantidadProductosPorNombre(string nombre)
		{
            try
            {
				int cantidad = await _context.Productos.CountAsync(x => x.IdCategoriaNavigation.Nombre == nombre);

                return cantidad;



            }catch(Exception ex)
            {

                return 0;

            }
		}


		public async Task <Producto> obtenerProductosPorId(int idProducto)
        {
            try
            {
                var producto =  await _context.Productos.Include(x=>x.IdCategoriaNavigation)
                    
                    .FirstOrDefaultAsync(p => p.IdProducto == idProducto );

                return producto;
            }
            catch (Exception ex)
            {
                // Aquí deberías manejar el error apropiadamente en lugar de lanzar una excepción no implementada.
                throw new Exception("Error al obtener el producto por ID.", ex);
            }

            return null; 
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


        public async Task <bool> VerificarCantidadProducots(int IdProducto, int cantidadSolicitada)
        {
            try
            {

                var cantidad =  await _context.Productos.Where(x => x.Cantidad >= cantidadSolicitada && x.IdProducto == IdProducto).FirstOrDefaultAsync();

                if(cantidad != null) {

                    return true;
                
                }

            }catch (Exception ex) {

                return false;
            
            }

            return false;


        }

        public async Task EditarCantidadProductos(Producto producto)
        {
            try
            {
              

                var productoEditar = await _context.Productos.Where(x=> x.IdProducto == producto.IdProducto).FirstAsync();
                productoEditar.Cantidad = productoEditar.Cantidad - producto.Cantidad;
                _context.Update(productoEditar);
                await _context.SaveChangesAsync();  
            


            }catch  (Exception ex)
            {

            }
        }

        public async Task<List<Producto>> ObtenerTodosProductosPorNombre(string name)
        {


            try
            {

                var productos = await   _context.Productos.Where(x => x.Nombre == name || x.Marca == name).ToListAsync();
                return productos;




            }
            catch (Exception ex)
            { 

                Console.WriteLine(ex.Message);
            
            }
            throw new NotImplementedException();
        }
    }
}
