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

    public string Foto { get; set; }

    public string Descripcion { get; set; }

    public string Detalles { get; set; }

    public string Marca { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<Carrito> Carritos { get; set; } = new List<Carrito>();

    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();

    public virtual CategoriaProducto IdCategoriaNavigation { get; set; }

    public virtual ICollection<ListaDeseo> ListaDeseos { get; set; } = new List<ListaDeseo>();
}
