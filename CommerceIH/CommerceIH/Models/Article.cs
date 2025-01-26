using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CommerceIH.Models;

[Table("Article")]
public partial class Article
{
    [Key]
    [Column("code")]
    public int Code { get; set; }

    [Column("nom")]
    [StringLength(64)]
    public string Nom { get; set; } = null!;

    [Column("articleDescription")]
    [StringLength(512)]
    public string? ArticleDescription { get; set; }

    [Column("prixUnitaire", TypeName = "decimal(7, 2)")]
    public decimal PrixUnitaire { get; set; }

    [Column("categorie")]
    [StringLength(32)]
    public string Categorie { get; set; } = null!;

    [Column("sousCategorie")]
    [StringLength(32)]
    public string? SousCategorie { get; set; }

    [Column("etat")]
    public bool? Etat { get; set; }

    [Column("vendeur")]
    public int Vendeur { get; set; }

    public string LienImage { get; set; } = null!;

    [InverseProperty("CodeArticleNavigation")]
    public virtual ICollection<Commentaire> Commentaires { get; set; } = new List<Commentaire>();

    [InverseProperty("CodeArticleNavigation")]
    public virtual ICollection<DetailsCommande> DetailsCommandes { get; set; } = new List<DetailsCommande>();

    [InverseProperty("CodeArticleNavigation")]
    public virtual ICollection<ImagesArticle> ImagesArticles { get; set; } = new List<ImagesArticle>();

    [ForeignKey("Vendeur")]
    [InverseProperty("Articles")]
    public virtual Utilisateur VendeurNavigation { get; set; } = null!;
}
