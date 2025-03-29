using System;
using System.Collections.Generic;

namespace ProyectoPrograIII.Models;

public partial class Servicio
{
    public int ServicioId { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public double? Precio { get; set; }

    public int? CategoriaId { get; set; }

    public bool? Estado { get; set; }

    public string? Materiales { get; set; }

    public string? AreaServicio { get; set; }

    public virtual Categorium? Categoria { get; set; }

    public virtual ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();

    public virtual ICollection<Favorito> Favoritos { get; set; } = new List<Favorito>();

    public virtual ICollection<Promocione> Promociones { get; set; } = new List<Promocione>();
}
