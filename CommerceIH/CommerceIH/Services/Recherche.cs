using CommerceIH.Data;
using CommerceIH.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CommerceIH.Services
{
    public class Recherche
    {
        private readonly IDbContextFactory<_2024Prog3CommerceIhContext> _factory;
        public Recherche(IDbContextFactory<_2024Prog3CommerceIhContext> factory)
        {
            _factory = factory;
        }

        public async Task<List<Article>> RechercherAsync(string input)
        {
            var dbContext = _factory.CreateDbContextAsync().Result;
            var recherche = $"%{input}%";
            var param1 = new SqlParameter("@param1", recherche);
            var articles = dbContext
                .Articles.FromSqlRaw($"SELECT * FROM Article WHERE nom LIKE @param1", param1).Include(ar => ar.VendeurNavigation);

            return await articles.ToListAsync();
        }
    }
}
