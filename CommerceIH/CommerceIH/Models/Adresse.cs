using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CommerceIH.Models;

[Table("Adresse")]
public partial class Adresse
{
    [Key]
    [Column("code")]
    public int Code { get; set; }

    [Column("codeCivique")]
    [StringLength(15)]
    public string CodeCivique { get; set; } = null!;

    [Column("rue")]
    [StringLength(64)]
    public string Rue { get; set; } = null!;

    [Column("codePostal")]
    [StringLength(10)]
    public string CodePostal { get; set; } = null!;

    [Column("noAppartement")]
    [StringLength(5)]
    public string? NoAppartement { get; set; }

    [Column("ville")]
    [StringLength(64)]
    public string Ville { get; set; } = null!;

    [Column("pays")]
    [StringLength(64)]
    public string Pays { get; set; } = null!;

    [InverseProperty("CodeAdresseNavigation")]
    public virtual ICollection<Utilisateur> Utilisateurs { get; set; } = new List<Utilisateur>();
}
