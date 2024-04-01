using Microsoft.EntityFrameworkCore;
using ProyectoPAU.Models;

namespace ProyectoPAU.Services.CarService
{
    public class CarService : ICarService
    {
        private readonly TiendauContext _contex;

        private readonly CarritoDetalle _carritoDetalle;


        private readonly ILogger<CarService> _logger;


        public CarService(TiendauContext context, CarritoDetalle carritoDetalle, ILogger<CarService> logger)
        {
            _contex = context;
            _carritoDetalle = carritoDetalle;
            _logger = logger;


        }
        public async Task addCarrito(CarritoDetalle carritoDetalle)
        {

            try
            {
                _contex.CarritoDetalles.Add(carritoDetalle);
                await _contex.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Aquí puedes manejar la excepción de acuerdo a tus necesidades
                // Por ejemplo, puedes registrar el error en un archivo de registro, enviar un correo electrónico al administrador, etc.
                // También puedes decidir qué tipo de excepción lanzar o cómo manejarla
                throw new Exception("Error al agregar el carrito: " + ex.Message, ex);
            }








        }

        public async Task<Carrito> BuscarAsyncCarrito(int UsuarioID)
        {
            try
            {
                Carrito carrito = await _contex.Carritos.FirstOrDefaultAsync(x => x.UsuarioId == UsuarioID);
                return carrito;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al buscar el carrito del usuario con ID {UsuarioID}", UsuarioID);
                throw; // Re-lanza la excepción para que pueda ser manejada en un nivel superior si es necesario
            }
        }

        public async Task CrearCarrito(Carrito carrito)
        {
            try
            {

                if(carrito != null)
                {
                     await _contex.Carritos.AddAsync(carrito);
                    await _contex.SaveChangesAsync();

                }
                

            }catch (Exception ex) {

                Console.WriteLine(ex);
            
            }
        }

        public async Task<List<CarritoDetalle>> detallesCarrito(Carrito carrito)
        {
            try
            {
                if (carrito != null)
                {

                    var detallesDelCarrito = await _contex.CarritoDetalles
                    .Where(x => x.IdCarrito == carrito.Id).Include(x => x.IdProductoNavigation)
                    .ToListAsync();

                    return detallesDelCarrito;

                }

                return null;

            }
            catch (Exception ex)
            {
                // Registra la excepción principal
                _logger.LogError(ex, "Error al obtener los detalles del carrito con ID {CarritoId}", carrito?.Id);

                // Mira la excepción interna si está presente
                if (ex.InnerException != null)
                {
                    _logger.LogError(ex.InnerException, "Excepción interna al obtener los detalles del carrito");
                }

                throw; // Re-lanza la excepción para que pueda ser manejada en un nivel superior si es necesario
            }
        }

        public async Task EliminarCarritoDetalle(int idCarritoDetalle)
        {
            try
            {
                // Buscar el carrito detalle por su ID
                var carritoDetalle = await _contex.CarritoDetalles.FindAsync(idCarritoDetalle);

                if (carritoDetalle != null)
                {
                    // Eliminar el carrito detalle encontrado
                    _contex.CarritoDetalles.Remove(carritoDetalle);
                    await _contex.SaveChangesAsync();
                }
                else
                {
                    throw new Exception($"No se encontró ningún carrito detalle con el ID {idCarritoDetalle}");
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción según sea necesario
                throw new Exception("Error al eliminar el carrito detalle: " + ex.Message );
            }
        }


    }

}
