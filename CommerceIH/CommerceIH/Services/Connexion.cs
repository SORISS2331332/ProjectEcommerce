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
using Dapper;
using System.Security.Claims;
using static CommerceIH.Pages.Connexion;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Text;


namespace CommerceIH.Services
{
    public class ConnexionService
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _dbConnection;
        public ConnexionService(IConfiguration configuration)
        {
            _configuration = configuration; //ajout de la config de l'app dans ce service
            _dbConnection = new SqlConnection(_configuration.GetConnectionString("Auth"));
        }

        public async Task<string> OnPostAsync(string email, string password)
        {

            //Appel de la procédure stockée de connexion d'utilisateur
            var connection = new SqlConnection(_configuration.GetConnectionString("Auth"));

            connection.Open();
            var parameters = new { email, password };
            var result = await _dbConnection.QuerySingleOrDefaultAsync<string>(
                "dbo.ConnecterUtilisateur", parameters, commandType: CommandType.StoredProcedure);


            //Retourne L'id de l'utilisateur
            return result.ToString();


        }

        
    }
}
