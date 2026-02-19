using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VHBurguer.Domains;
using VHBurguer.Exceptions;

namespace VHBurguer.Applications.Autenticacao
{
    public class GeradorTokenJwt
    {
        private readonly IConfiguration _config;
        private IEnumerable<Claim> claims;

        public GeradorTokenJwt (IConfiguration config)
        {
            _config = config;
        }

        public string GerarToken(Usuario usuario)
        {

            // garante que o token não foi auterado
            var chave = _config["Jwt:Key"]!;

            //a api valida se o token veio do emissor correto
            var issuer = _config["Jwt:Issuer"]!;

            //define qual sistema pode usar o token 
            var audience = _config["Jwt:Audience"]!;

            //Depois disso, o usuário precisa Logar novamente
            var expiraEmMinutos = int.Parse(_config["Jwt:expiraEmMinutos"]!);

            //converte a chave para bytes (necessario para criar a assinatura)
            var KeyBytes = Encoding.UTF8.GetBytes(chave);

            if(KeyBytes.Length < 32)
            {
                throw new DomainException("Jwt: Key precisa ter pelo menos 32 caracteres (256 bits).");
            }
            // cria chave de segurança usada para assinar o token 
            var securityKey = new SymmetricSecurityKey(KeyBytes);

            //Define o algoritimo de assinatura do token 
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var clain = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.UsuarioID.ToString()),

                new Claim (ClaimTypes.Name, usuario.Nome),

                new Claim (ClaimTypes.Email, usuario.Email),

            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(expiraEmMinutos),
                signingCredentials: credentials,
                );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
