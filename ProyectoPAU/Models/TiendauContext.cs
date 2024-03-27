using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProyectoPAU.Models;

public partial class TiendauContext : DbContext
{
    public TiendauContext()
    {
    }

    public TiendauContext(DbContextOptions<TiendauContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Carrito> Carritos { get; set; }

    public virtual DbSet<CategoriaProducto> CategoriaProductos { get; set; }

    public virtual DbSet<DetalleVenta> DetalleVenta { get; set; }

    public virtual DbSet<ErroresDePrograma> ErroresDeProgramas { get; set; }

    public virtual DbSet<HistorialRefreshToken> HistorialRefreshTokens { get; set; }

    public virtual DbSet<ListaDeseo> ListaDeseos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Tienda> Tienda { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Venta> Venta { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=JIM3N3Z; DataBase=TIENDAU; Trusted_Connection=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Carrito>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Carrito__3214EC278F89A68D");

            entity.ToTable("Carrito");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProductoId).HasColumnName("ProductoID");
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Producto).WithMany(p => p.Carritos)
                .HasForeignKey(d => d.ProductoId)
                .HasConstraintName("FK__Carrito__Product__236943A5");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Carritos)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__Carrito__Usuario__22751F6C");
        });

        modelBuilder.Entity<CategoriaProducto>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__Categori__02AA0785E79862D2");

            entity.ToTable("CategoriaProducto");

            entity.Property(e => e.IdCategoria)
                .ValueGeneratedNever()
                .HasColumnName("ID_Categoria");
            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<DetalleVenta>(entity =>
        {
            entity.HasKey(e => e.IdDetalleVenta).HasName("PK__DetalleV__0157010AF5AB29E3");

            entity.Property(e => e.IdDetalleVenta).HasColumnName("ID_DetalleVenta");
            entity.Property(e => e.ProductoId).HasColumnName("Producto_ID");
            entity.Property(e => e.VentaId).HasColumnName("Venta_ID");

            entity.HasOne(d => d.Producto).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.ProductoId)
                .HasConstraintName("FK__DetalleVe__Produ__1F98B2C1");

            entity.HasOne(d => d.Venta).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.VentaId)
                .HasConstraintName("FK__DetalleVe__Venta__1EA48E88");
        });

        modelBuilder.Entity<ErroresDePrograma>(entity =>
        {
            entity.HasKey(e => e.IdErroresDePrograma).HasName("PK__Errores___F47557452062BB7E");

            entity.ToTable("Errores_de_Programa");

            entity.Property(e => e.IdErroresDePrograma)
                .ValueGeneratedNever()
                .HasColumnName("ID_Errores_de_Programa");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
        });

        modelBuilder.Entity<HistorialRefreshToken>(entity =>
        {
            entity.HasKey(e => e.IdHistorialToken).HasName("PK__Historia__03DC48A53C05F444");

            entity.ToTable("HistorialRefreshToken");

            entity.Property(e => e.EsActivo).HasComputedColumnSql("(case when [FechaExpiracion]<getdate() then CONVERT([bit],(0)) else CONVERT([bit],(1)) end)", false);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaExpiracion).HasColumnType("datetime");
            entity.Property(e => e.RefreshToken)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Token)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.HistorialRefreshTokens)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Historial__IdUsu__5FB337D6");
        });

        modelBuilder.Entity<ListaDeseo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ListaDes__3214EC27EBDCFCC6");

            entity.ToTable("ListaDeseo");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ProductoId).HasColumnName("ProductoID");
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Producto).WithMany(p => p.ListaDeseos)
                .HasForeignKey(d => d.ProductoId)
                .HasConstraintName("FK_Product_Lista");

            entity.HasOne(d => d.Usuario).WithMany(p => p.ListaDeseos)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK_Usuario_Lista");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__Producto__9B4120E297622B4B");

            entity.ToTable("Producto");

            entity.Property(e => e.IdProducto).HasColumnName("ID_Producto");
            entity.Property(e => e.Descripcion).IsUnicode(false);
            entity.Property(e => e.Detalles).IsUnicode(false);
            entity.Property(e => e.Foto).IsUnicode(false);
            entity.Property(e => e.IdCategoria).HasColumnName("ID_Categoria");
            entity.Property(e => e.Marca)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Precio).HasColumnName("precio");
            entity.Property(e => e.Tipo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("tipo");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("FK__Producto__ID_Cat__1AD3FDA4");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.RolId).HasName("PK__Rol__F92302D1746E5A69");

            entity.ToTable("Rol");

            entity.Property(e => e.RolId)
                .ValueGeneratedNever()
                .HasColumnName("RolID");
            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<Tienda>(entity =>
        {
            entity.HasKey(e => e.IdTienda).HasName("PK__Tienda__91024DF42D6E7CC8");

            entity.Property(e => e.IdTienda)
                .ValueGeneratedNever()
                .HasColumnName("ID_Tienda");
            entity.Property(e => e.Correo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuario__2B3DE798BB8FD573");

            entity.ToTable("Usuario");

            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");
            entity.Property(e => e.Apellido).HasMaxLength(50);
            entity.Property(e => e.Contraseña).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Nombre).HasMaxLength(50);
            entity.Property(e => e.RolId).HasColumnName("RolID");

            entity.HasOne(d => d.Rol).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.RolId)
                .HasConstraintName("FK__Usuario__RolID__534D60F1");
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.IdVenta).HasName("PK__Venta__3CD842E5A5527BDC");

            entity.Property(e => e.IdVenta).HasColumnName("ID_Venta");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.TiendaId).HasColumnName("tienda_id");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_Id");

            entity.HasOne(d => d.Tienda).WithMany(p => p.Venta)
                .HasForeignKey(d => d.TiendaId)
                .HasConstraintName("FK__Venta__tienda_id__08B54D69");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Venta)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__Venta__usuario_I__09A971A2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
