using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CommerceIH.Models;

[PrimaryKey("CodeArticle", "CodeCommande")]
[Table("DetailsCommande")]
public partial class DetailsCommande
{
    [Key]
    [Column("codeArticle")]
    public int CodeArticle { get; set; }

    [Key]
    [Column("codeCommande")]
    public int CodeCommande { get; set; }

    [Column("quantite")]
    public int Quantite { get; set; }

    [ForeignKey("CodeArticle")]
    [InverseProperty("DetailsCommandes")]
    public virtual Article CodeArticleNavigation { get; set; } = null!;

    [ForeignKey("CodeCommande")]
    [InverseProperty("DetailsCommandes")]
    public virtual Commande CodeCommandeNavigation { get; set; } = null!;
}
