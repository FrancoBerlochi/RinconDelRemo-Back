using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Domain.Interfaces;
using Microsoft.Extensions.Options;
using System.Security.Cryptography.X509Certificates;
using Domain.Entities;

namespace Infrastructure.Services
{
    public class AuthenticationService : IAuthenticationService
    {

        private readonly AuthenticacionServiceOptions _options;
        public AuthenticationService(IOptions<AuthenticacionServiceOptions> options)
        {

            _options = options.Value;
        }

        public bool AuthenticateToken(string token, string expectedIssuer, string expectedAudience)
        {
            if (string.IsNullOrEmpty(token))
            {
                return false;
            }
            var handler = new JwtSecurityTokenHandler();

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = expectedIssuer,

                ValidateAudience = true,
                ValidAudience = expectedAudience,

                ValidateLifetime = false, // Podés poner true si querés validar expiración
                ValidateIssuerSigningKey = false,
            };
            try
            {
                handler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                return true; // Token válido
            }
            catch (Exception ex)
            {
                Console.WriteLine("Token inválido: " + ex.Message);
                return false;
            }
        }
        public string Authenticate(string token, string expectedIssuer, string expectedAudience)
        {
            var azureToken = AuthenticateToken(token, expectedIssuer, expectedAudience);
            if (!azureToken)
            {
                return ("No se a pasado un token válido");
            }

            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);

            var securityPassword = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_options.SecretForKey)); //Traemos la SecretKey del Json. agregar antes: using Microsoft.IdentityModel.Tokens;

            var credentials = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);

            //Los claims son datos en clave->valor que nos permite guardar data del usuario.
            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("NameIdentifier", jwt.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value));
            claimsForToken.Add(new Claim("Name", jwt.Claims.FirstOrDefault(c => c.Type == "name")?.Value));
            //falta role
            


            var jwtSecurityToken = new JwtSecurityToken( //agregar using System.IdentityModel.Tokens.Jwt; Acá es donde se crea el token con toda la data que le pasamos antes.
                _options.Issuer,
                _options.Audience,
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                credentials);

            var tokenToReturn = new JwtSecurityTokenHandler() //Pasamos el token a string
                .WriteToken(jwtSecurityToken);

            return tokenToReturn.ToString();
        }
        public class AuthenticacionServiceOptions
        {
            public const string AuthenticacionService = "AuthenticacionService";
            public string Issuer { get; set; }
            public string Audience { get; set; }
            public string SecretForKey { get; set; }
        }
    }
}
