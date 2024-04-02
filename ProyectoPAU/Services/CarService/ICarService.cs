using Microsoft.AspNetCore.Mvc;
using ProyectoPAU.Models;

namespace ProyectoPAU.Services.CarService
{
    public interface ICarService
    {

        public Task CrearCarrito(Carrito carrito);

        public Task addCarrito(CarritoDetalle categoria);

        public Task <Carrito> BuscarAsyncCarrito(int UsuarioID);

        public Task<List<CarritoDetalle>> detallesCarrito(Carrito carrito);

        public Task EliminarCarritoDetalle(int idCarritoDetalle);

        public  Task EliminarCarritosDetalles(int idCarrito);


    }


}
