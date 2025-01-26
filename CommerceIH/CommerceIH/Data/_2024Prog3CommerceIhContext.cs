using System;
using System.Collections.Generic;
using CommerceIH.Models;
using Microsoft.EntityFrameworkCore;

namespace CommerceIH.Data;

public partial class _2024Prog3CommerceIhContext : DbContext
{
    public _2024Prog3CommerceIhContext()
    {
    }

    public _2024Prog3CommerceIhContext(DbContextOptions<_2024Prog3CommerceIhContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Adresse> Adresses { get; set; }

    public virtual DbSet<Article> Articles { get; set; }

    public virtual DbSet<Commande> Commandes { get; set; }

    public virtual DbSet<Commentaire> Commentaires { get; set; }

    public virtual DbSet<DetailsCommande> DetailsCommandes { get; set; }

    public virtual DbSet<ImagesArticle> ImagesArticles { get; set; }

    public virtual DbSet<Utilisateur> Utilisateurs { get; set; }

    public virtual DbSet<VueDetailsCommande> VueDetailsCommandes { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Adresse>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("PK__Adresse__357D4CF8A79722B9");
        });

        modelBuilder.Entity<Article>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("PK__Article__357D4CF81BBE8757");

            entity.HasOne(d => d.VendeurNavigation).WithMany(p => p.Articles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Article_Utilisateur");
        });

        modelBuilder.Entity<Commande>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("PK__Commande__357D4CF844782B48");

            entity.ToTable("Commande", tb => tb.HasTrigger("addDeliveryDate"));

            entity.Property(e => e.DateEmission).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DateLivraison).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.CodeUtilisateurNavigation).WithMany(p => p.Commandes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Commande_Utilisateur");
        });

        modelBuilder.Entity<Commentaire>(entity =>
        {
            entity.HasKey(e => new { e.CodeUtilisateur, e.CodeArticle }).HasName("PK__Commenta__E7BA0D4527CA228F");

            entity.ToTable("Commentaire", tb => tb.HasTrigger("update_etat"));

            entity.HasOne(d => d.CodeArticleNavigation).WithMany(p => p.Commentaires)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Commentaire_Article");

            entity.HasOne(d => d.CodeUtilisateurNavigation).WithMany(p => p.Commentaires)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Commentaire_Utilisateur");
        });

        modelBuilder.Entity<DetailsCommande>(entity =>
        {
            entity.HasKey(e => new { e.CodeArticle, e.CodeCommande }).HasName("PK__DetailsC__1965ECC9AC5F620E");

            entity.HasOne(d => d.CodeArticleNavigation).WithMany(p => p.DetailsCommandes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetailsCommande_Article");

            entity.HasOne(d => d.CodeCommandeNavigation).WithMany(p => p.DetailsCommandes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetailsCommande_Commande");
        });

        modelBuilder.Entity<ImagesArticle>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("PK__ImagesAr__357D4CF80DAC717B");

            entity.HasOne(d => d.CodeArticleNavigation).WithMany(p => p.ImagesArticles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Code_Article");
        });

        modelBuilder.Entity<Utilisateur>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("PK__Utilisat__357D4CF8A956108A");

            entity.Property(e => e.MotDePasse).IsFixedLength();

            entity.HasOne(d => d.CodeAdresseNavigation).WithMany(p => p.Utilisateurs).HasConstraintName("FK_Utilisateur_Adresse");
        });

        modelBuilder.Entity<VueDetailsCommande>(entity =>
        {
            entity.ToView("VueDetailsCommandes");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
