using CommerceIH.Data;
using CommerceIH.Models;
using Microsoft.EntityFrameworkCore;

namespace CommerceIH.Services
{
    public class AffichageProduits
    {
        private readonly IDbContextFactory<_2024Prog3CommerceIhContext> _factory;
        public AffichageProduits(IDbContextFactory<_2024Prog3CommerceIhContext> factory)
        {
            _factory = factory;
        }

        public async Task<List<Article>> GetArticlesAsync()
        {
            var dbContext = _factory.CreateDbContextAsync().Result; //Connexion à la BD

            //Recuperation des articles mis en ligne par l'utilisateur
            var articles = await (from art in dbContext.Articles
                                  select art).Include(ar => ar.VendeurNavigation).ToListAsync();

            return articles;
        }
    }
}
