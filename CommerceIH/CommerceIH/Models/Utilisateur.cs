using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CommerceIH.Models;

[Table("Utilisateur")]
[Index("Courriel", Name = "UQ_Courriel", IsUnique = true)]
public partial class Utilisateur
{
    [Key]
    [Column("code")]
    public int Code { get; set; }

    [Column("nom")]
    [StringLength(64)]
    public string Nom { get; set; } = null!;

    [Column("prenom")]
    [StringLength(64)]
    public string Prenom { get; set; } = null!;

    [Column("courriel")]
    [StringLength(64)]
    public string Courriel { get; set; } = null!;

    [Column("motDePasse")]
    [MaxLength(128)]
    public byte[] MotDePasse { get; set; } = null!;

    [Column("codeAdresse")]
    public int? CodeAdresse { get; set; }

    [Column("sel")]
    public Guid? Sel { get; set; }

    [InverseProperty("VendeurNavigation")]
    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();

    [ForeignKey("CodeAdresse")]
    [InverseProperty("Utilisateurs")]
    public virtual Adresse? CodeAdresseNavigation { get; set; }

    [InverseProperty("CodeUtilisateurNavigation")]
    public virtual ICollection<Commande> Commandes { get; set; } = new List<Commande>();

    [InverseProperty("CodeUtilisateurNavigation")]
    public virtual ICollection<Commentaire> Commentaires { get; set; } = new List<Commentaire>();
}
