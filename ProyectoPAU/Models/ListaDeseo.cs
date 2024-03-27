using System;
using System.Collections.Generic;

namespace ProyectoPAU.Models;

public partial class ListaDeseo
{
    public int Id { get; set; }

    public int? UsuarioId { get; set; }

    public int? ProductoId { get; set; }

    public virtual Producto Producto { get; set; }

    public virtual Usuario Usuario { get; set; }
}
