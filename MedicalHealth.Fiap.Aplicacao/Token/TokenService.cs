using MedicalHealth.Fiap.Dominio;
using MedicalHealth.Fiap.Infraestrutura.DTO;
using MedicalHealth.Fiap.Infraestrutura.Enum;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MedicalHealth.Fiap.Aplicacao.Token
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
            => _configuration = configuration;

        public async Task<string> ObterToken(AutenticarUsuarioDTO usuarioDTO)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var chaveCriptografia = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("SecretJWT"));

            string role = usuarioDTO.TipoUsuario.ToString();

            ClaimsIdentity claimsIdentity = null;

            // Autenticação condicional com base no tipo de usuário
            if (usuarioDTO.TipoUsuario == Roles.Medico)
            {
                claimsIdentity = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, usuarioDTO.Email),
                    new Claim(ClaimTypes.Role, role)
                });
            }
            else if (usuarioDTO.TipoUsuario == Roles.Paciente)
            {
                claimsIdentity = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, usuarioDTO.Email),
                    new Claim(ClaimTypes.Role, role)
                });
            }
            else if (usuarioDTO.TipoUsuario == Roles.Administrador)
            {
                claimsIdentity = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, usuarioDTO.Email),
                    new Claim(ClaimTypes.Role, role)
                });
            }
            else
            {
                throw new UnauthorizedAccessException("Tipo de usuário inválido.");
            }

            // Criação do token JWT
            var tokenPropriedades = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddHours(8),
                Issuer = "API",
                Audience = "Hackathon",
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(chaveCriptografia),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenPropriedades);
            return tokenHandler.WriteToken(token); 
        }
    }
}
