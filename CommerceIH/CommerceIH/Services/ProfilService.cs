using CommerceIH.Data;
using CommerceIH.Models;
using Microsoft.AspNetCore.Components.Authorization;
using CommerceIH.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Text;
using Microsoft.Data.SqlClient;
using System.Data;
namespace CommerceIH.Services
{
    public class ProfilService
    {
        private readonly IConfiguration configuration;
        private readonly IDbContextFactory<_2024Prog3CommerceIhContext> factory;

        public ProfilService(IConfiguration configuration, IDbContextFactory<_2024Prog3CommerceIhContext> factory) 
        { 
            this.configuration = configuration; 
            this.factory = factory;
        }


        public async Task<Utilisateur> GetUtilisateurConnecte(string email)
        {
   
            var dbContext = factory.CreateDbContextAsync().Result; //Connexion à la BD

            //Include dans ce cas ce comporte comme un join en SQL et on a directement accès aux informations de l'adresse de l'utilisateur
            //Le link a été déjà créé par Blazor dans Models en utilisant les clés étrangères
            var utilisateur = (from util in dbContext.Utilisateurs
                              where util.Courriel == email
                              select util).Include(u=>u.CodeAdresseNavigation); 

            return utilisateur.FirstOrDefault();

        }
        public async Task ModifierUtilisateurConnecte(string email,string nom, string prenom,string password)
        {

            var dbContext = factory.CreateDbContextAsync().Result; //Connexion à la BD
            var utilisateur = (from util in dbContext.Utilisateurs
                              where util.Courriel == email
                              select util).FirstOrDefault();

            utilisateur.Nom = nom;
            utilisateur.Prenom = prenom;

            //Connexion à la BD pour faire appel à la procedure de modification du password
            string connectionChaine = configuration.GetConnectionString("Auth");

            using var connectionBD = new SqlConnection(connectionChaine);

            //Apple de la procédure stockée de connexion l'utilisateur
            connectionBD.Open();
            using var command = new SqlCommand("dbo.ChangerMotDePasse", connectionBD);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar, 255)).Value = email;
            command.Parameters.Add(new SqlParameter("@NouveauMotDePasse", SqlDbType.NVarChar, 255)).Value = password;

            await command.ExecuteNonQueryAsync();

            dbContext.SaveChangesAsync();
        }
    }
}
