using System;
using System.Collections.Generic;

namespace ProyectoPrograIII.Models;

public partial class Promocione
{
    public int PromocionId { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public DateTime? FechaInicio { get; set; }

    public DateTime? FechaFin { get; set; }

    public int? ProductoId { get; set; }

    public int? ServicioId { get; set; }

    public bool? Estado { get; set; }

    public virtual Producto? Producto { get; set; }

    public virtual Servicio? Servicio { get; set; }
}
