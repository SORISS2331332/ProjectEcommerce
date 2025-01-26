using CommerceIH.Authentication;
using CommerceIH.Data;
using CommerceIH.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Security.Claims;
using static CommerceIH.Pages.Inscription;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Text;
using CommerceIH.Data;

namespace CommerceIH.Services
{
    public class InscriptionSrvc
    {
        private IDbContextFactory<_2024Prog3CommerceIhContext> _factory;

        public InscriptionSrvc(IDbContextFactory<_2024Prog3CommerceIhContext> factory)
        {
            _factory = factory;
        }

        public async Task<int> InscriptionUser(string nom, string prenom, string courriel, string mdp)
        {
            var dbContext = await _factory.CreateDbContextAsync();
            var param1 = new SqlParameter("nom", nom);
            var param2 = new SqlParameter("prenom", prenom);
            var param3 = new SqlParameter("courriel", courriel);
            var param4 = new SqlParameter("mdp", mdp);
            var resultatParam = new SqlParameter("@reponse", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            var resultat = await dbContext.Database
                .ExecuteSqlRawAsync("EXEC dbo.creerUtilisateur @nom, @prenom, @courriel, @mdp, @reponse output", param1, param2, param3, param4, resultatParam);

            return (int)resultatParam.Value;
        }
    }
}
