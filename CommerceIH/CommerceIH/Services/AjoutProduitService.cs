using Azure;
using CommerceIH.Data;
using CommerceIH.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using System;

namespace CommerceIH.Services
{
    public class AjoutProduitService
    {
        private readonly IConfiguration configuration;
        public readonly IDbContextFactory<_2024Prog3CommerceIhContext> _factory;
        private readonly IWebHostEnvironment _environment;
        
        public AjoutProduitService(IConfiguration configuration, IDbContextFactory<_2024Prog3CommerceIhContext> factory, IWebHostEnvironment environment)
        {
            this.configuration = configuration;
            _factory = factory;
            _environment = environment;
        }

        public async Task<Article> GetArticleAsync(int idArticle, string emailVendeur)
        {
            var dbContext = _factory.CreateDbContextAsync().Result; //Connexion à la BD

            //Je trouve l'id de l'utilisateur connecté(vendeur)
            int idVendeur = (from util in dbContext.Utilisateurs
                             where util.Courriel == emailVendeur
                             select util.Code).FirstOrDefault();

            //Je trouve l'article vendu par l'utilisateur connecté
            var article = (from art in dbContext.Articles
                    where art.Vendeur == idVendeur && art.Code == idArticle
                    select art).FirstOrDefault();
                                                                         
            return article;
        }
       
        public async Task  SauvegarderImage(IBrowserFile image,string lienImage)
        {
            using (var stream = image.OpenReadStream())
            {
                var cheminDossierImg = Path.Combine(_environment.WebRootPath, lienImage);
                using (var fichierCharge = new FileStream(cheminDossierImg, FileMode.Create))
                {
                    await stream.CopyToAsync(fichierCharge);
                }
            }
        }


       public async Task AjouterArticle(Article article, string emailVendeur)
        {
            var dbContext =  _factory.CreateDbContextAsync().Result; //Connexion à la BD

            
            int idVendeur = (from util in dbContext.Utilisateurs
                                 where util.Courriel == emailVendeur
                                 select util.Code).FirstOrDefault();

            // Assignation des valeurs des propriétées non soumises par le formulaire. Par défaut un produit publié est disponible(etat=true)
            article.Vendeur = idVendeur;
            article.Etat= true;
            //Ajout de l'article et Sauvegarde de la BD
            dbContext.Add(article);
            await dbContext.SaveChangesAsync();


        }
        public async Task SupprimerArticle(int idArticle)
        {
            var dbContext = _factory.CreateDbContextAsync().Result; //Connexion à la BD

            //Je trouve l'article grâce à son id récupéré en Get
            var article = (from art in dbContext.Articles
                           where art.Code == idArticle
                           select art).FirstOrDefault();

            dbContext.Remove(article);
            dbContext.SaveChangesAsync();


        }

        public async Task ModifierArticle(int idArticle,Article articleModifie)
        {
            var dbContext = _factory.CreateDbContextAsync().Result; //Connexion à la BD

            //Je trouve l'article grâce à son id récupéré en Get
            var article  = (from art in dbContext.Articles
                             where art.Code == idArticle
                            select art).FirstOrDefault();

            //Modification des valeurs de l'article et sauvegarde de BD
            article.Nom = articleModifie.Nom;
            article.ArticleDescription = articleModifie.ArticleDescription;
            article.PrixUnitaire = articleModifie.PrixUnitaire;
            article.Categorie = articleModifie.Categorie;
            article.SousCategorie = articleModifie.SousCategorie;
            article.LienImage = articleModifie.LienImage;

            dbContext.SaveChangesAsync();


        }
    }
}
