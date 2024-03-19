using System;
using System.Collections.Generic;

namespace ProyectoPAU.Models;

public partial class ErroresDePrograma
{
    public int IdErroresDePrograma { get; set; }

    public string Descripcion { get; set; }

    public DateOnly? Fecha { get; set; }
}
