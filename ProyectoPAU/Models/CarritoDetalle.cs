using System;
using System.Collections.Generic;

namespace ProyectoPAU.Models;

public partial class CarritoDetalle
{
    public int Id { get; set; }

    public int? IdCarrito { get; set; }

    public int? IdProducto { get; set; }

    public int? Cantidad { get; set; }

    public int? PrecioUnitario { get; set; }

    public int? PrecioTotal { get; set; }

    public virtual Carrito IdCarritoNavigation { get; set; }

    public virtual Producto IdProductoNavigation { get; set; }
}
