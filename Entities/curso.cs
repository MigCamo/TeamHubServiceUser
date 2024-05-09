using System;
using System.Collections.Generic;

namespace TeamHubServiceUser.Entities;

public partial class curso
{
    public int ID { get; set; }

    public string? Clave { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public int? NoHoras { get; set; }

    public DateTime? FechaInicio { get; set; }

    public DateTime? FechaTermino { get; set; }

    public decimal? Costo { get; set; }

    public string? Instructor { get; set; }
}
