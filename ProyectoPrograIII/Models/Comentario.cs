using System;
using System.Collections.Generic;

namespace ProyectoPrograIII.Models;

public partial class Comentario
{
    public int ComentarioId { get; set; }

    public int? ProductoId { get; set; }

    public int? ServicioId { get; set; }

    public int? UsuarioId { get; set; }

    public string? Comentario1 { get; set; }

    public int? Calificacion { get; set; }

    public DateTime? Fecha { get; set; }

    public virtual Producto? Producto { get; set; }

    public virtual Servicio? Servicio { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
