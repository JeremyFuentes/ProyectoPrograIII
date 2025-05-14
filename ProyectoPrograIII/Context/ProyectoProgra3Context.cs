using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ProyectoPrograIII.Models;

namespace ProyectoPrograIII.Context;

public partial class ProyectoProgra3Context : DbContext
{
    public ProyectoProgra3Context()
    {
    }

    public ProyectoProgra3Context(DbContextOptions<ProyectoProgra3Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Categorium> Categoria { get; set; }

    public virtual DbSet<Comentario> Comentarios { get; set; }

    public virtual DbSet<DetalleFactura> DetalleFacturas { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Facturacion> Facturacions { get; set; }

    public virtual DbSet<Favorito> Favoritos { get; set; }

    public virtual DbSet<Marca> Marcas { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Promocione> Promociones { get; set; }

    public virtual DbSet<Proveedore> Proveedores { get; set; }

    public virtual DbSet<Servicio> Servicios { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public DbSet<ImagenesProducto> ImagenesProducto { get; set; }

    public DbSet<Administrador> Administradores { get; set; }

    public DbSet<Carrito> Carrito { get; set; }

    public virtual DbSet<EstadosProducto> estadosProductos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=ASUS_TUF_JEREMY\\SQLEXPRESS; Initial Catalog=ProyectoProgra3;User ID=Jeremy;Password = admin123; Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categorium>(entity =>
        {
            entity.HasKey(e => e.CategoriaId).HasName("PK__Categori__F353C1C50D4A6735");

            entity.Property(e => e.CategoriaId).HasColumnName("CategoriaID");
            entity.Property(e => e.Descripcion).HasMaxLength(100);
            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<Comentario>(entity =>
        {
            entity.HasKey(e => e.ComentarioId).HasName("PK__Comentar__F1844958CF9EFF6A");

            entity.Property(e => e.ComentarioId).HasColumnName("ComentarioID");
            entity.Property(e => e.Comentario1)
                .HasMaxLength(255)
                .HasColumnName("Comentario");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ProductoId).HasColumnName("ProductoID");
            entity.Property(e => e.ServicioId).HasColumnName("ServicioID");
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Producto).WithMany(p => p.Comentarios)
                .HasForeignKey(d => d.ProductoId)
                .HasConstraintName("FK__Comentari__Produ__6A30C649");

            entity.HasOne(d => d.Servicio).WithMany(p => p.Comentarios)
                .HasForeignKey(d => d.ServicioId)
                .HasConstraintName("FK__Comentari__Servi__6B24EA82");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Comentarios)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__Comentari__Usuar__6C190EBB");
        });

        modelBuilder.Entity<DetalleFactura>(entity =>
        {
            entity.HasKey(e => e.DetalleId).HasName("PK__DetalleF__6E19D6FAA3536D78");

            entity.ToTable("DetalleFactura");

            entity.Property(e => e.DetalleId).HasColumnName("DetalleID");
            entity.Property(e => e.FacturaId).HasColumnName("FacturaID");
            entity.Property(e => e.ProductoId).HasColumnName("ProductoID");

            entity.HasOne(d => d.Factura).WithMany(p => p.DetalleFacturas)
                .HasForeignKey(d => d.FacturaId)
                .HasConstraintName("FK__DetalleFa__Factu__5DCAEF64");

            entity.HasOne(d => d.Producto).WithMany(p => p.DetalleFacturas)
                .HasForeignKey(d => d.ProductoId)
                .HasConstraintName("FK__DetalleFa__Produ__5EBF139D");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.EmpleadoId).HasName("PK__Empleado__958BE6F0F48362EB");

            entity.ToTable("Empleado");

            entity.Property(e => e.EmpleadoId).HasColumnName("EmpleadoID");
            entity.Property(e => e.Cargo).HasMaxLength(50);
            entity.Property(e => e.Contacto).HasMaxLength(20);
            entity.Property(e => e.Contraseña).HasMaxLength(100);
            entity.Property(e => e.Correo).HasMaxLength(50);
            entity.Property(e => e.Direccion).HasMaxLength(150);
            entity.Property(e => e.Dui)
                .HasMaxLength(10)
                .HasColumnName("DUI");
            entity.Property(e => e.Horario).HasMaxLength(50);
            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<Facturacion>(entity =>
        {
            entity.HasKey(e => e.FacturaId).HasName("PK__Facturac__5C024805E4E51060");

            entity.ToTable("Facturacion");

            entity.Property(e => e.FacturaId).HasColumnName("FacturaID");
            entity.Property(e => e.EmpleadoId).HasColumnName("EmpleadoID");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Local).HasMaxLength(50);
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Empleado).WithMany(p => p.Facturacions)
                .HasForeignKey(d => d.EmpleadoId)
                .HasConstraintName("FK__Facturaci__Emple__59FA5E80");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Facturacions)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__Facturaci__Usuar__5AEE82B9");
        });

        modelBuilder.Entity<Favorito>(entity =>
        {
            entity.HasKey(e => e.FavoritoId).HasName("PK__Favorito__CFF711855250E871");

            entity.Property(e => e.FavoritoId).HasColumnName("FavoritoID");
            entity.Property(e => e.ProductoId).HasColumnName("ProductoID");
            entity.Property(e => e.ServicioId).HasColumnName("ServicioID");
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Producto).WithMany(p => p.Favoritos)
                .HasForeignKey(d => d.ProductoId)
                .HasConstraintName("FK__Favoritos__Produ__6FE99F9F");

            entity.HasOne(d => d.Servicio).WithMany(p => p.Favoritos)
                .HasForeignKey(d => d.ServicioId)
                .HasConstraintName("FK__Favoritos__Servi__70DDC3D8");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Favoritos)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__Favoritos__Usuar__6EF57B66");
        });

        modelBuilder.Entity<Marca>(entity =>
        {
            entity.HasKey(e => e.MarcaId).HasName("PK__Marca__D5B1CDEBB5D886F1");

            entity.ToTable("Marca");

            entity.Property(e => e.MarcaId).HasColumnName("MarcaID");
            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.ProductoId).HasName("PK__Producto__A430AE835EF929A1");

            entity.Property(e => e.ProductoId).HasColumnName("ProductoID");
            entity.Property(e => e.CategoriaId).HasColumnName("CategoriaID");
            entity.Property(e => e.Descripcion).HasMaxLength(150);
            entity.Property(e => e.MarcaId).HasColumnName("MarcaID");
            entity.Property(e => e.Nombre).HasMaxLength(50);
            entity.Property(e => e.ProveedorId).HasColumnName("ProveedorID");
            entity.Property(e => e.Sku)
                .HasMaxLength(20)
                .HasColumnName("SKU");

            entity.HasOne(d => d.Categoria).WithMany(p => p.Productos)
                .HasForeignKey(d => d.CategoriaId)
                .HasConstraintName("FK__Productos__Categ__5070F446");

            entity.HasOne(d => d.Marca).WithMany(p => p.Productos)
                .HasForeignKey(d => d.MarcaId)
                .HasConstraintName("FK__Productos__Marca__5165187F");

            entity.HasOne(d => d.Proveedor).WithMany(p => p.Productos)
                .HasForeignKey(d => d.ProveedorId)
                .HasConstraintName("FK__Productos__Prove__52593CB8");
        });

        modelBuilder.Entity<Promocione>(entity =>
        {
            entity.HasKey(e => e.PromocionId).HasName("PK__Promocio__2DA61DBDDFAA97D6");

            entity.Property(e => e.PromocionId).HasColumnName("PromocionID");
            entity.Property(e => e.Descripcion).HasMaxLength(200);
            entity.Property(e => e.FechaFin).HasColumnType("datetime");
            entity.Property(e => e.FechaInicio).HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(50);
            entity.Property(e => e.ProductoId).HasColumnName("ProductoID");
            entity.Property(e => e.ServicioId).HasColumnName("ServicioID");

            entity.HasOne(d => d.Producto).WithMany(p => p.Promociones)
                .HasForeignKey(d => d.ProductoId)
                .HasConstraintName("FK__Promocion__Produ__6477ECF3");

            entity.HasOne(d => d.Servicio).WithMany(p => p.Promociones)
                .HasForeignKey(d => d.ServicioId)
                .HasConstraintName("FK__Promocion__Servi__656C112C");
        });

        modelBuilder.Entity<Proveedore>(entity =>
        {
            entity.HasKey(e => e.ProveedorId).HasName("PK__Proveedo__61266BB96EAF19B7");

            entity.Property(e => e.ProveedorId).HasColumnName("ProveedorID");
            entity.Property(e => e.Contacto).HasMaxLength(20);
            entity.Property(e => e.Correo).HasMaxLength(50);
            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.HasKey(e => e.ServicioId).HasName("PK__Servicio__D5AEEC22FEC4DC91");

            entity.Property(e => e.ServicioId).HasColumnName("ServicioID");
            entity.Property(e => e.AreaServicio).HasMaxLength(100);
            entity.Property(e => e.CategoriaId).HasColumnName("CategoriaID");
            entity.Property(e => e.Descripcion).HasMaxLength(200);
            entity.Property(e => e.Materiales).HasMaxLength(200);
            entity.Property(e => e.Nombre).HasMaxLength(50);

            entity.HasOne(d => d.Categoria).WithMany(p => p.Servicios)
                .HasForeignKey(d => d.CategoriaId)
                .HasConstraintName("FK__Servicios__Categ__619B8048");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuarios__2B3DE79821F7B4CC");

            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");
            entity.Property(e => e.Contacto).HasMaxLength(20);
            entity.Property(e => e.Contraseña)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Correo).HasMaxLength(50);
            entity.Property(e => e.Direccion).HasMaxLength(150);
            entity.Property(e => e.GoogleId)
                .HasMaxLength(50)
                .HasColumnName("GoogleID");
            entity.Property(e => e.MetodoLogin)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("Propio");
            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<EstadosProducto>(entity =>
        {
            entity.HasKey(e => e.EstadoProductoId);
            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
