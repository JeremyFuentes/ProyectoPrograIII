using System;
using System.Collections.Generic;

namespace ProyectoPrograIII.Models;

public partial class DetalleFactura
{
    public int DetalleId { get; set; }

    public int? FacturaId { get; set; }

    public int? ProductoId { get; set; }

    public int? Cantidad { get; set; }

    public double? PrecioUnitario { get; set; }

    public double? Subtotal { get; set; }

    public virtual Facturacion? Factura { get; set; }

    public virtual Producto? Producto { get; set; }
}
