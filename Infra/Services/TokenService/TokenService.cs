using Domain.Entities.Credential;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Infra.Services.TokenService;

public class TokenService(IConfiguration configuration)
{
    public string CreateToken(AccountCredential data)
    {
        string secretKey = configuration["Jwt:SecretKey"]!;
        var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            SigningCredentials = credentials,
            Subject = new ClaimsIdentity([
                new Claim(JwtRegisteredClaimNames.Sub, data.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, data.Email),
                new Claim("last_sign_in", DateTime.Now.ToString()),
                ]),
            Expires = DateTime.Now.AddHours(configuration.GetValue<int>("Jwt:ExpirationTimeInHours")),
            Issuer = configuration["Jwt:Issuer"],
            Audience = configuration["Jwt:Audience"]
        };

        var handler = new JsonWebTokenHandler();

        var token = handler.CreateToken(tokenDescriptor);

        return token;
    }

    public string GenerateRefreshToken(AccountCredential data)
    {
        string refreshToken = "";
        int counter = 0;

        byte[] convertionBytes;

        string securityId = data.Id.ToString();
        string idSection = securityId.Split('-')[0];

        while (counter < idSection.Count())
        {
            var randomNumber = new byte[16];
            var randomGenerator = RandomNumberGenerator.Create();
            randomGenerator.GetBytes(randomNumber);
            string convertedNumber = Convert.ToBase64String(randomNumber);
            convertionBytes = Encoding.UTF8.GetBytes(convertedNumber);
            refreshToken += Encoding.UTF8.GetString(convertionBytes);
            counter++;
        }

        return refreshToken;
    }
}
