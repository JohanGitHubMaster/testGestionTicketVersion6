using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GestionTicket.Repositories.Token
{
   public class Token
   {
      public string GenerateTokenAsync(string userRole)
      {
         try
         {
            if (userRole == null)
               userRole = "User";
            // Clé correctement encodée en octets avec la longueur appropriée (512 bits)
            byte[] keyBytes = Encoding.UTF8.GetBytes("MaCleSecreteHS512MaCleSecreteHS512MaCleSecreteHS512MaCleSecreteHS512");

            // Création de la clé symétrique
            var key = new SymmetricSecurityKey(keyBytes);

            // Création des informations d'identification pour signer le jeton
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            // Création du jeton JWT
            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: new List<Claim>()
                {
                  new Claim(ClaimTypes.Role, userRole),
                },
         expires: DateTime.Now.AddMinutes(90),
                signingCredentials: cred
            );

            // Sérialisation du jeton en une chaîne de caractères
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            // Retourne le jeton généré
            return jwt;
         }
         catch (Exception ex)
         {
            // Gérer ou enregistrer l'exception selon les besoins de votre application
            throw ex; // Re-lancer l'exception capturée pour le moment
         }
      }
   }
}
