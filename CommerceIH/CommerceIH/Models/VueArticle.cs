using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CommerceIH.Models;

[Keyless]
public partial class VueArticle
{
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

    [Column("lienImg")]
    [StringLength(32)]
    [Unicode(false)]
    public string LienImg { get; set; } = null!;
}
