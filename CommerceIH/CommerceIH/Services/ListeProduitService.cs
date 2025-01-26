using CommerceIH.Data;
using CommerceIH.Models;
using Microsoft.EntityFrameworkCore;

namespace CommerceIH.Services
{
    public class ListeProduitService
    {
        private readonly IConfiguration configuration;
        public readonly IDbContextFactory<_2024Prog3CommerceIhContext> _factory;
        public ListeProduitService(IConfiguration configuration, IDbContextFactory<_2024Prog3CommerceIhContext> factory)
        {
            this.configuration = configuration; 
            _factory = factory;
        }

        public async Task<List<Article>> GetUtilisateurArticleAsync(string email)
        {

            var dbContext = _factory.CreateDbContextAsync().Result; //Connexion à la BD

            //Je trouve l'id de l'utilisateur connecté
           int idUtilisateur = (from util in dbContext.Utilisateurs
                                 where util.Courriel == email
                                 select util.Code).FirstOrDefault();

            //Recuperation des articles mis en ligne par l'utilisateur
            var articles = await (from art in dbContext.Articles
                               where art.Vendeur == idUtilisateur
                           select art).Include(ar => ar.VendeurNavigation).ToListAsync();

            return articles;

        }



    }
}
