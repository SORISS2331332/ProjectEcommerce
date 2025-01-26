using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CommerceIH.Models;

[Table("Commande")]
public partial class Commande
{
    [Key]
    [Column("code")]
    public int Code { get; set; }

    [Column("dateEmission", TypeName = "datetime")]
    public DateTime? DateEmission { get; set; }

    [Column("dateLivraison", TypeName = "datetime")]
    public DateTime? DateLivraison { get; set; }

    [Column("statut")]
    [StringLength(32)]
    public string Statut { get; set; } = null!;

    [Column("codeUtilisateur")]
    public int CodeUtilisateur { get; set; }

    [ForeignKey("CodeUtilisateur")]
    [InverseProperty("Commandes")]
    public virtual Utilisateur CodeUtilisateurNavigation { get; set; } = null!;

    [InverseProperty("CodeCommandeNavigation")]
    public virtual ICollection<DetailsCommande> DetailsCommandes { get; set; } = new List<DetailsCommande>();
}
