using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Learning.Model;
using Microsoft.IdentityModel.Tokens;

namespace Learning.Service;

public class TokenService
{
    public static object GenerateToken(Employee employee)
    {
        var key = Encoding.ASCII.GetBytes(Key.secret);
        var tokenConfig = new SecurityTokenDescriptor
        {
            // Dizendo oq quero salvar no token que é o ID do User
            Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
            {
                new Claim("employeeId", employee.id.ToString()),
            }),
            Expires = DateTime.UtcNow.AddHours(3), //tempo de inspiração
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256) 
            //key e algoritmo de criptografia
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenConfig);
        var tokenString = tokenHandler.WriteToken(token);

        return new { token = tokenString};
    }
}