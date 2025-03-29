using System;
using System.Collections.Generic;

namespace ProyectoPrograIII.Models;

public partial class Producto
{
    public int ProductoId { get; set; }

    public string? Nombre { get; set; }

    public double? Precio { get; set; }

    public int? Stock { get; set; }

    public int? CategoriaId { get; set; }

    public string? Sku { get; set; }

    public string? Descripcion { get; set; }

    public int? MarcaId { get; set; }

    public int? ProveedorId { get; set; }

    public bool? Estado { get; set; }

    public virtual Categorium? Categoria { get; set; }

    public virtual ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();

    public virtual ICollection<DetalleFactura> DetalleFacturas { get; set; } = new List<DetalleFactura>();

    public virtual ICollection<Favorito> Favoritos { get; set; } = new List<Favorito>();

    public virtual Marca? Marca { get; set; }

    public virtual ICollection<Promocione> Promociones { get; set; } = new List<Promocione>();

    public virtual Proveedore? Proveedor { get; set; }
}
