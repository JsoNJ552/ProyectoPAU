using ProyectoPAU.Models;

namespace ProyectoPAU.Services.VentasService
{
    public interface IVentasServicecs
    {
        public Task RegistrarVenta(Venta venta, List<DetalleVenta> detallesVenta);
        public Task<List<Venta>> ObtenerTodasLasVentas();
        public Task<List<DetalleVenta>> ObtenerDetalleVentasPorIdVenta(int idventa);
        public List<DetalleVenta> ObtenerDetalleVentasPorUsuarioId(List<Venta> venta);
        public List<DetalleVenta> ObtenerTodasDetalleVentasPorVenta(List<Venta> venta);

        public List<Venta> ObtenerVentasPorID(int usuarioID);

        public List<DetalleVenta> ObtenerVentasPorBusqueda(string usuario);

        public Task EditarVenta(Venta Venta, DetalleVenta VentaDetalle);

        public Task EliminarVenta(int IdVenta);


    }
}
