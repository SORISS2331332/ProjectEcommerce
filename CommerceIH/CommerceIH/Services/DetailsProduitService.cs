using CommerceIH.Data;
using CommerceIH.Models;
using Microsoft.EntityFrameworkCore;

namespace CommerceIH.Services
{
    public class DetailsProduitService
    {
        private readonly IDbContextFactory<_2024Prog3CommerceIhContext> _factory;
        public DetailsProduitService(IDbContextFactory<_2024Prog3CommerceIhContext> factory)
        {
            _factory = factory;
        }

        public async Task<Article> GetArticlesInfo(int code)
        {
            var dbContext = _factory.CreateDbContextAsync().Result; //Connexion à la BD
            var article = (from Article in dbContext.Articles
                          where Article.Code == code
                          select Article).Include(ar => ar.VendeurNavigation).Include(ar => ar.DetailsCommandes).FirstOrDefault();

            return article;
        }
    }
}
