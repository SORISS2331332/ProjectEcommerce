using CommerceIH.Data;
using CommerceIH.Models;
using Microsoft.EntityFrameworkCore;

namespace CommerceIH.Services
{
    public class CommandesService
    {
        private readonly IDbContextFactory<_2024Prog3CommerceIhContext> _factory;
        public CommandesService(IDbContextFactory<_2024Prog3CommerceIhContext> factory)
        {
            _factory = factory;
        }

        public async Task<List<Commande>> AfficherCommandes(int codeUser)
        {
            var dbContext = _factory.CreateDbContextAsync().Result; //Connexion à la BD

            //Récupération des commandes
            var commandes = await (from cmnd in dbContext.Commandes
                                  where cmnd.CodeUtilisateur == codeUser && cmnd.Statut != "panier"
                                  select cmnd).ToListAsync();

            return commandes;
        }
    }
}
