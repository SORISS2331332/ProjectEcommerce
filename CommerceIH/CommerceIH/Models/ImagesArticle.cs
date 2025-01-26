using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CommerceIH.Models;

public partial class ImagesArticle
{
    [Key]
    [Column("code")]
    public int Code { get; set; }

    [Column("codeArticle")]
    public int CodeArticle { get; set; }

    [Column("lienImg")]
    [Unicode(false)]
    public string LienImg { get; set; } = null!;

    [ForeignKey("CodeArticle")]
    [InverseProperty("ImagesArticles")]
    public virtual Article CodeArticleNavigation { get; set; } = null!;
}
