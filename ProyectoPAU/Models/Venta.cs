using System;
using System.Collections.Generic;

namespace ProyectoPAU.Models;

public partial class Venta
{
    public int IdVenta { get; set; }

    public DateOnly? Fecha { get; set; }

    public int? TiendaId { get; set; }

    public int? UsuarioId { get; set; }

    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();

    public virtual Tienda Tienda { get; set; }

    public virtual Usuario Usuario { get; set; }
}
