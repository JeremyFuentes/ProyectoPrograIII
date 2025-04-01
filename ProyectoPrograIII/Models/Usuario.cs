using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace ProyectoPrograIII.Models;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public string? Nombre { get; set; }

    public string? Direccion { get; set; }

    public string? Contacto { get; set; }

    public string? Correo { get; set; }

    public string? GoogleId { get; set; }

    public string? Contraseña { get; set; }

    public string? MetodoLogin { get; set; } = null!;

    public virtual ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();

    public virtual ICollection<Facturacion> Facturacions { get; set; } = new List<Facturacion>();

    public virtual ICollection<Favorito> Favoritos { get; set; } = new List<Favorito>();
}
