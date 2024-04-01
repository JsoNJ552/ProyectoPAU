using System;
using System.Collections.Generic;

namespace ProyectoPAU.Models;

public partial class Carrito
{
    public int Id { get; set; }

    public int? UsuarioId { get; set; }

    public virtual ICollection<CarritoDetalle> CarritoDetalles { get; set; } = new List<CarritoDetalle>();

    public virtual Usuario Usuario { get; set; }
}
