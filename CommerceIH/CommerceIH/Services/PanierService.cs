using CommerceIH.Data;
using CommerceIH.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CommerceIH.Services
{
    public class PanierService
    {
        private readonly IDbContextFactory<_2024Prog3CommerceIhContext> _factory;
        public PanierService(IDbContextFactory<_2024Prog3CommerceIhContext> factory)
        {
            _factory = factory;
        }

        private async Task CreerPanier(int codeUser)
        {
            var dbContext = _factory.CreateDbContextAsync().Result;
            var newPanier = new Commande();
            newPanier.CodeUtilisateur = codeUser;
            newPanier.Statut = "panier";
            dbContext.Commandes.Add(newPanier);

            dbContext.SaveChanges();
        }

        public int RecupCodeUser(string email)
        {
            var dbContext = _factory.CreateDbContextAsync().Result;
            int codeUser = (from util in dbContext.Utilisateurs
                            where util.Courriel == email
                            select util.Code).FirstOrDefault();

            return codeUser;
        }

        public async Task<List<Article>> AfficherPanier(int codeUser)
        {
            var dbContext = _factory.CreateDbContextAsync().Result;
            bool panierExists = dbContext.Commandes.Any(p => p.CodeUtilisateur == codeUser && p.Statut == "panier");

            //Si l'utilisateur n'a pas de panier, on le crée
            if (!panierExists)
            {
                CreerPanier(codeUser);
            }

            //On récupère le code du panier
            var codePanier = (from cmnd in dbContext.Commandes
                              where cmnd.CodeUtilisateur == codeUser && cmnd.Statut == "panier"
                              select cmnd.Code).FirstOrDefault();

            //On récupère les codes des articles dans le panier
            var codesArticles = (from cmnd in dbContext.DetailsCommandes
                                 where cmnd.CodeCommande == codePanier
                                 select cmnd.CodeArticle).ToList();

            var articles = new List<Article>();

            foreach (int codeA in codesArticles)
            {
                articles.Add((from article in dbContext.Articles
                              where article.Code == codeA
                              select article).Include(ar => ar.VendeurNavigation).FirstOrDefault());
            }

            return articles;
        }

        public async Task AjoutPanier(int codeUser, int codeArticle)
        {
            var dbContext = _factory.CreateDbContextAsync().Result;
            bool panierExists = dbContext.Commandes.Any(p => p.CodeUtilisateur == codeUser && p.Statut == "panier");

            //Si l'utilisateur n'a pas de panier, on le crée
            if (!panierExists)
            {
                CreerPanier(codeUser);
            }

            var codePanier = (from cmnd in dbContext.Commandes
                              where cmnd.CodeUtilisateur == codeUser && cmnd.Statut == "panier"
                              select cmnd.Code).FirstOrDefault();

            var newArticle = new DetailsCommande();
            newArticle.CodeCommande = codePanier;
            newArticle.CodeArticle = codeArticle;
            newArticle.Quantite += 1;
            dbContext.DetailsCommandes.Add(newArticle);

            dbContext.SaveChangesAsync();
        }

        public async Task RetirerPanier(int codeUser, int codeArticle)
        {
            var dbContext = _factory.CreateDbContextAsync().Result;
            var codePanier = (from cmnd in dbContext.Commandes
                              where cmnd.CodeUtilisateur == codeUser && cmnd.Statut == "panier"
                              select cmnd.Code).FirstOrDefault();

            bool articleExists = dbContext.DetailsCommandes.Any(a => a.CodeArticle == codeArticle);

            if (articleExists)
            {
                var article = (from details in dbContext.DetailsCommandes
                               where details.CodeArticle == codeArticle && details.CodeCommande == codePanier
                               select details).FirstOrDefault();
                dbContext.DetailsCommandes.Remove(article);
                dbContext.SaveChangesAsync();
            }
        }

        public async Task Commander(int codeUser)
        {
            var dbContext = _factory.CreateDbContextAsync().Result;

            var commande = (from cmnd in dbContext.Commandes
                            where cmnd.CodeUtilisateur == codeUser && cmnd.Statut == "panier"
                            select cmnd).FirstOrDefault();

            commande.Statut = "en cours";
            commande.DateEmission = DateTime.Now;
            commande.DateLivraison = DateTime.Now.AddDays(3);
            dbContext.SaveChangesAsync();
        }
    }
}
