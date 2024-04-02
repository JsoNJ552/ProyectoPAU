using System;
using System.Collections.Generic;

namespace ProyectoPAU.Models;

public partial class DetalleVenta
{
    public int IdDetalleVenta { get; set; }

    public int? VentaId { get; set; }

    public int? ProductoId { get; set; }

    public int? Cantidad { get; set; }

    public int? PrecioUnitario { get; set; }

    public int? PrecioTotal { get; set; }

    public string NombreJuridico { get; set; }

    public string ApellidoJuridico { get; set; }

    public string Direccion { get; set; }

    public string ZipCode { get; set; }

    public string TipoPago { get; set; }

    public virtual Producto Producto { get; set; }

    public virtual Venta Venta { get; set; }
}
