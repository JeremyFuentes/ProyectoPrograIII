using System;
using System.Collections.Generic;

namespace ProyectoPrograIII.Models;

public partial class Favorito
{
    public int FavoritoId { get; set; }

    public int? UsuarioId { get; set; }

    public int? ProductoId { get; set; }

    public int? ServicioId { get; set; }

    public virtual Producto? Producto { get; set; }

    public virtual Servicio? Servicio { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
