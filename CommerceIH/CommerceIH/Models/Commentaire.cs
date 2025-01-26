using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CommerceIH.Models;

[PrimaryKey("CodeUtilisateur", "CodeArticle")]
[Table("Commentaire")]
public partial class Commentaire
{
    [Column("note")]
    public int Note { get; set; }

    [Column("commentaire")]
    [StringLength(512)]
    public string Commentaire1 { get; set; } = null!;

    [Key]
    [Column("codeUtilisateur")]
    public int CodeUtilisateur { get; set; }

    [Key]
    [Column("codeArticle")]
    public int CodeArticle { get; set; }

    [ForeignKey("CodeArticle")]
    [InverseProperty("Commentaires")]
    public virtual Article CodeArticleNavigation { get; set; } = null!;

    [ForeignKey("CodeUtilisateur")]
    [InverseProperty("Commentaires")]
    public virtual Utilisateur CodeUtilisateurNavigation { get; set; } = null!;
}
