using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PIA_PWEB.Models.dbModels;

public partial class PiaDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
{
    public PiaDbContext()
    {
    }

    public PiaDbContext(DbContextOptions<PiaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Calificacione> Calificaciones { get; set; }

    public virtual DbSet<Categorium> Categoria { get; set; }

    public virtual DbSet<Like> Likes { get; set; }

    public virtual DbSet<Lista> Listas { get; set; }

    public virtual DbSet<Pelicula> Peliculas { get; set; }

    public virtual DbSet<Reseña> Reseñas { get; set; }

    public virtual DbSet<Streaming> Streamings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Calificacione>(entity =>
        {
            entity.HasKey(e => new { e.IdUsuario, e.IdPelicula }).HasName("PK__Califica__DDA295DEE7E04FED");

            entity.HasOne(d => d.IdPeliculaNavigation).WithMany(p => p.Calificaciones)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Calificac__idPel__66603565");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Calificaciones)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Calificac__idUsu__656C112C");
        });

        modelBuilder.Entity<Categorium>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__Categori__8A3D240C1C9D8F85");
        });

        modelBuilder.Entity<Like>(entity =>
        {
            entity.HasKey(e => new { e.IdUsuario, e.IdPelicula }).HasName("PK__Likes__DDA295DEB65CAEBF");

            entity.HasOne(d => d.IdPeliculaNavigation).WithMany(p => p.Likes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Likes__idPelicul__6B24EA82");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Likes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Likes__idUsuario__6A30C649");
        });

        modelBuilder.Entity<Lista>(entity =>
        {
            entity.HasKey(e => e.IdLista).HasName("PK__listas__6C8A0FE58F6A0F3F");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Lista)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__listas__idUsuari__5AEE82B9");

            entity.HasMany(d => d.IdPeliculas).WithMany(p => p.IdLista)
                .UsingEntity<Dictionary<string, object>>(
                    "PeliculaListum",
                    r => r.HasOne<Pelicula>().WithMany()
                        .HasForeignKey("IdPelicula")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__PeliculaL__idPel__5EBF139D"),
                    l => l.HasOne<Lista>().WithMany()
                        .HasForeignKey("IdLista")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__PeliculaL__idLis__5DCAEF64"),
                    j =>
                    {
                        j.HasKey("IdLista", "IdPelicula").HasName("PK__Pelicula__D57FB99D0E1763E5");
                        j.ToTable("PeliculaLista");
                        j.IndexerProperty<int>("IdLista").HasColumnName("idLista");
                        j.IndexerProperty<int>("IdPelicula").HasColumnName("idPelicula");
                    });
        });

        modelBuilder.Entity<Pelicula>(entity =>
        {
            entity.HasKey(e => e.IdPelicula).HasName("PK__Pelicula__9F5B678A4FAA0764");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Peliculas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pelicula_Categoria");

            entity.HasOne(d => d.IdStreamingNavigation).WithMany(p => p.Peliculas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pelicula_Streaming");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Peliculas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pelicula_Usuario");
        });

        modelBuilder.Entity<Reseña>(entity =>
        {
            entity.HasKey(e => new { e.IdUsuario, e.IdPelicula }).HasName("PK__Reseñas__DDA295DE13E729DA");

            entity.HasOne(d => d.IdPeliculaNavigation).WithMany(p => p.Reseñas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reseñas__idPelic__628FA481");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Reseñas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reseñas__idUsuar__619B8048");
        });

        modelBuilder.Entity<Streaming>(entity =>
        {
            entity.HasKey(e => e.IdStreaming).HasName("PK__Streamin__0E4E246D4490C82F");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
