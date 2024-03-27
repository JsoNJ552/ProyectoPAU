using System;
using System.Collections.Generic;

namespace ProyectoPAU.Models;

public partial class Carrito
{
    public int Id { get; set; }

    public int? UsuarioId { get; set; }

    public int? ProductoId { get; set; }

    public int? Cantidad { get; set; }

    public decimal? Precio { get; set; }

    public virtual Producto Producto { get; set; }

    public virtual Usuario Usuario { get; set; }
}
