using Microsoft.EntityFrameworkCore;
using ProyectoPAU.Models;

namespace ProyectoPAU.Services.VentasService
{
    public class VentaService : IVentasServicecs
    {
        private readonly TiendauContext _context;

        private readonly DetalleVenta _detalleVenta;
        public VentaService(TiendauContext context, DetalleVenta detalleVenta)
        {
            _context = context;

            _detalleVenta = detalleVenta;

        }


        public Task EditarVenta(Venta Venta, DetalleVenta VentaDetalle)
        {
            throw new NotImplementedException();
        }

        public Task EliminarVenta(int IdVenta)
        {
            throw new NotImplementedException();
        }

        public async Task<List<DetalleVenta>> ObtenerDetalleVentasPorIdVenta(int idventa)
        {
            try
            {

                    // Obtener todos los detalles de venta relacionados con la venta actual
                    var detallesVenta = await _context.DetalleVenta
                        .Where(d => d.VentaId == idventa)
                        .Include(d => d.Producto)
                        .Include(d => d.Venta)
                        .ToListAsync();

                    // Agregar los detalles de venta encontrados a la lista
                   
            

                return detallesVenta;
            }
            catch (Exception ex)
            {
                // Manejar la excepción según sea necesario
                throw new Exception("Error al obtener las ventas: " + ex.Message);
            }
        }
    


        public  List <DetalleVenta>  ObtenerDetalleVentasPorUsuarioId(List<Venta> venta)
        {

            try
            {

                var ventasUsuario = new List<DetalleVenta>();

                foreach(var ventas in venta)
                {
                    var primeraVentaDetalle = _context.DetalleVenta.Where(x => x.VentaId == ventas.IdVenta)
                        .OrderBy(x => x.VentaId).Include(d => d.Producto).Include(d => d.Venta).FirstOrDefault();

                    if(primeraVentaDetalle != null)
                    {
                        ventasUsuario.Add(primeraVentaDetalle);
                    }
                }

                return ventasUsuario;


            }catch (Exception ex)
            {


            }
            throw new NotImplementedException();
        }

       

        public List<DetalleVenta> ObtenerTodasDetalleVentasPorVenta(List<Venta> venta)
        {

            try
            {

                var ventasUsuario = new List<DetalleVenta>();

                foreach (var ventas in venta)
                {
                    var primeraVentaDetalle = _context.DetalleVenta.Where(x => x.VentaId == ventas.IdVenta)
                        .OrderBy(x => x.VentaId).Include(d => d.Producto).Include(d => d.Venta).FirstOrDefault();

                    if (primeraVentaDetalle != null)
                    {
                        ventasUsuario.Add(primeraVentaDetalle);
                    }
                }

                return ventasUsuario;


            }
            catch (Exception ex)
            {


            }
            throw new NotImplementedException();
        }

        public  List<DetalleVenta> ObtenerVentasPorBusqueda(string usuario)
        {

            try
            {
                List <DetalleVenta> productos =  _context.DetalleVenta.Where(x => x.Producto.Nombre.Contains(usuario) || x.Venta.Usuario.Nombre.Contains(usuario) ).Include(x=>x.Venta.Usuario ).Include(x=> x.Venta) as List<DetalleVenta>;


                return productos;
            }
            catch (Exception ex)
            {
                return null;
            }
      
        }

        public List <Venta> ObtenerVentasPorID(int usuarioID)
        {

            try
            {
                var VentasUsuario = new List<Venta>();

                VentasUsuario = (List<Venta>)_context.Venta.Where(x => x.UsuarioId == usuarioID).ToList();
                

                return VentasUsuario;
                



            }catch (Exception ex)
            {


            }
            throw new NotImplementedException();
        }

        public async Task RegistrarVenta(Venta venta, List<DetalleVenta> detallesVenta)
        {
            try
            {
                // Asignar la fecha actual a la venta si no está definida
                if (venta.Fecha == null)
                {
                    
                }

                // Agregar la venta al contexto de la base de datos
                _context.Venta.Add(venta);
                await _context.SaveChangesAsync();

                // Obtener el IdVenta generado
                int idVenta = venta.IdVenta;

                // Asignar el IdVenta a cada detalle de venta y agregarlos al contexto
                foreach (var detalle in detallesVenta)
                {
                    detalle.VentaId = idVenta;
                    _context.DetalleVenta.Add(detalle);
                }

                // Guardar los cambios en el contexto de la base de datos
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Manejar la excepción adecuadamente
                Console.WriteLine($"Error al registrar venta: {ex.Message}");
                throw; // Lanzar la excepción para manejarla en un nivel superior si es necesario
            }
        }


        public async Task<List<Venta>> ObtenerTodasLasVentas()
        {
            try
            {
                var todasLasVentas = new List<Venta>();
                todasLasVentas = (List<Venta>)_context.Venta.ToList();

                return todasLasVentas;
            }
            catch (Exception ex)
            {
                // Manejar la excepción según sea necesario
                throw new Exception("Error al obtener las ventas con detalles y productos: " + ex.Message);
            }
        }

        
    }
}
