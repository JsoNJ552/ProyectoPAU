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

    public virtual DbSet<CarritoDetalle> CarritoDetalles { get; set; }

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
    {

    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Carrito>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Carrito__3214EC271C5E4F1A");

            entity.ToTable("Carrito");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Carritos)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__Carrito__Usuario__7FEAFD3E");
        });

        modelBuilder.Entity<CarritoDetalle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CarritoD__3214EC27288AC43A");

            entity.ToTable("CarritoDetalle");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IdCarrito).HasColumnName("ID_Carrito");
            entity.Property(e => e.IdProducto).HasColumnName("ID_PRODUCTO");
            entity.Property(e => e.PrecioTotal).HasColumnName("Precio_Total");
            entity.Property(e => e.PrecioUnitario).HasColumnName("Precio_Unitario");

            entity.HasOne(d => d.IdCarritoNavigation).WithMany(p => p.CarritoDetalles)
                .HasForeignKey(d => d.IdCarrito)
                .HasConstraintName("FK__CarritoDe__Preci__02C769E9");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.CarritoDetalles)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__CarritoDe__ID_PR__03BB8E22");
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
            entity.HasKey(e => e.IdDetalleVenta).HasName("PK__DetalleV__0157010AB9809744");

            entity.Property(e => e.IdDetalleVenta).HasColumnName("ID_DetalleVenta");
            entity.Property(e => e.ProductoId).HasColumnName("Producto_ID");
            entity.Property(e => e.VentaId).HasColumnName("Venta_ID");

            entity.HasOne(d => d.Producto).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.ProductoId)
                .HasConstraintName("FK__DetalleVe__Produ__7D0E9093");

            entity.HasOne(d => d.Venta).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.VentaId)
                .HasConstraintName("FK__DetalleVe__Venta__7C1A6C5A");
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
            entity.HasKey(e => e.IdHistorialToken).HasName("PK__Historia__03DC48A508C03EFE");

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
                .HasConstraintName("FK__Historial__IdUsu__503BEA1C");
        });

        modelBuilder.Entity<ListaDeseo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ListaDes__3214EC279E3B6038");

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
            entity.HasKey(e => e.IdProducto).HasName("PK__Producto__9B4120E2E9835DF0");

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
                .HasConstraintName("FK__Producto__ID_Cat__756D6ECB");
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
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuario__2B3DE798A6D52D92");

            entity.ToTable("Usuario");

            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");
            entity.Property(e => e.Apellido).HasMaxLength(50);
            entity.Property(e => e.Contraseña).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Nombre).HasMaxLength(50);
            entity.Property(e => e.RolId).HasColumnName("RolID");

            entity.HasOne(d => d.Rol).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.RolId)
                .HasConstraintName("FK__Usuario__RolID__3D2915A8");
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.IdVenta).HasName("PK__Venta__3CD842E5E812E5DE");

            entity.Property(e => e.IdVenta).HasColumnName("ID_Venta");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.TiendaId).HasColumnName("tienda_id");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_Id");

            entity.HasOne(d => d.Tienda).WithMany(p => p.Venta)
                .HasForeignKey(d => d.TiendaId)
                .HasConstraintName("FK__Venta__tienda_id__7849DB76");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Venta)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__Venta__usuario_I__793DFFAF");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
