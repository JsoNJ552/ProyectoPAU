using ProyectoPAU.Models;

namespace ProyectoPAU.Services.VentasService
{
    public interface IVentasServicecs
    {
        public Task RegistrarVenta(Venta venta, List<DetalleVenta> detallesVenta);

        public Task<List<DetalleVenta>> ObtenerVentas(int idventa);
        public List<DetalleVenta> ObtenerDetalleVentasPorUsuarioId(List<Venta> venta);

        public List<Venta> ObtenerVentasPorID(int usuarioID);

        public Task EditarVenta(Venta Venta, DetalleVenta VentaDetalle);

        public Task EliminarVenta(int IdVenta);


    }
}
