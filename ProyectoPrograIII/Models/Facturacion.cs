using System;
using System.Collections.Generic;

namespace ProyectoPrograIII.Models;

public partial class Facturacion
{
    public int FacturaId { get; set; }

    public int? EmpleadoId { get; set; }

    public int? UsuarioId { get; set; }

    public DateTime? Fecha { get; set; }

    public string? Local { get; set; }

    public double? Total { get; set; }

    public virtual ICollection<DetalleFactura> DetalleFacturas { get; set; } = new List<DetalleFactura>();

    public virtual Empleado? Empleado { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
