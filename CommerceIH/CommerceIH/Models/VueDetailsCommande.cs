using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CommerceIH.Models;

[Keyless]
public partial class VueDetailsCommande
{
    [Column("codeCommande")]
    public int CodeCommande { get; set; }

    [Column("dateEmission", TypeName = "datetime")]
    public DateTime? DateEmission { get; set; }

    [Column("dateLivraison", TypeName = "datetime")]
    public DateTime? DateLivraison { get; set; }

    [Column("statut")]
    [StringLength(32)]
    public string Statut { get; set; } = null!;

    [Column("nomArticle")]
    [StringLength(64)]
    public string NomArticle { get; set; } = null!;

    [Column("quantite")]
    public int Quantite { get; set; }
}
