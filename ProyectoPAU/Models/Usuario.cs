using System;
using System.Collections.Generic;

namespace ProyectoPAU.Models;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public string Nombre { get; set; }

    public string Apellido { get; set; }

    public string Email { get; set; }

    public string Contraseña { get; set; }

    public int? RolId { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<Carrito> Carritos { get; set; } = new List<Carrito>();

    public virtual ICollection<HistorialRefreshToken> HistorialRefreshTokens { get; set; } = new List<HistorialRefreshToken>();

    public virtual ICollection<ListaDeseo> ListaDeseos { get; set; } = new List<ListaDeseo>();

    public virtual Rol Rol { get; set; }

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
