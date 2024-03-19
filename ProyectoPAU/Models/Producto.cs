using System;
using System.Collections.Generic;

namespace ProyectoPAU.Models;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string Nombre { get; set; }

    public int? Precio { get; set; }

    public string Tipo { get; set; }

    public int? IdCategoria { get; set; }

    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();

    public virtual CategoriaProducto IdCategoriaNavigation { get; set; }
}
