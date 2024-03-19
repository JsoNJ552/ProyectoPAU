using System;
using System.Collections.Generic;

namespace ProyectoPAU.Models;

public partial class Fechastoken
{
    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaExpiracion { get; set; }

    public bool? EsaActivo { get; set; }
}
