using System;
using System.Collections.Generic;

namespace ProyectoPAU.Models;

public partial class Tienda
{
    public int IdTienda { get; set; }

    public string Nombre { get; set; }

    public string Direccion { get; set; }

    public string Telefono { get; set; }

    public string Correo { get; set; }

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
