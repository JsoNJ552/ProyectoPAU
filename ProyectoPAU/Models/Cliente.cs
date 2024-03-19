using System;
using System.Collections.Generic;

namespace ProyectoPAU.Models;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public string Nombre { get; set; }

    public string Apellido { get; set; }

    public string Email { get; set; }

    public string Telefono { get; set; }

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
