using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    //[Authorize(Roles ="Admin1")]
    public class WeatherForecastController : ControllerBase
    {
        private IRsaKeyService ras;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IRsaKeyService ras)
        {
            _logger = logger;
            this.ras = ras;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get([FromHeader] string X_Custom_Token)
        {
            var role=User.IsInRole("Admin");

            //var pkey= ras.GetPrivateKey();

            // var publicKey = ras.GetPublicKey();

            var privateKey = """
    -----BEGIN RSA PRIVATE KEY-----
    MIIEogIBAAKCAQEAujYZTlrZjQEUfdSGE4x9JxRQywb2Ves5jtkZmJkOV8QBhce7
    VKlJDseLHsxSs+x8i6rWV5UrFzmeb4nej74XUfrMhh3kJbL0ILcQz+VRoc6P/pfy
    vba2DmkzZNBQiVxnPgQAijS/c2So1F8gozRkkM/OepNjcZAvVTPDp3AbBBFinTRX
    rR0w8PmbV3q0sZytaSS7DWwolgzvtMboP3cs43hCulK8jWgxXe/MotT1rANhZyqo
    dPuFeH95m8xCKxv+snauj8aStySss9nxpiLja0lhSxcSIJtr/MjpD4Wai7Fif6QN
    iGyL/cGYfEisaOFAJQrQ78njnF/L/RJKbmoKHQIDAQABAoIBAHFU4bwFoSingqA+
    y8d0FuvE+pNGzoBmZKajTWfJp6gU/FQ+xq31Rii/m9WHx8IH6ZlbxY6SlCJTiDEc
    eR+FM05K1VW/NY8YRmru4EWXrUJMpOjv8+YxIMbOkmAGRS4E1jUxXF7BUdBacEKp
    DwUlqxtTzrhdogWJGrXBhpAOs/KZ05zEHIlKxn68EK1E4ez1hNNk5cNOVTzzvJxn
    NaA1YXhNcY2qkUf6dDNxPitOiGQ8Ay/VpwLOTt6ViVQFiPP7B5MZ56Hy0AJUj5hk
    FAE19GoCPltFdGjddr8wsIYa9evO158c/FtnWe2RC7PzTDOf14Ib6kqERB3iiVKf
    AIm552UCgYEA67/35e4aT18oB2ssrTMhBAjxQcw0i8KzPfvm/KTJS5Ik81KfHYkw
    IM9Sib/LsSoRAWed/TnPwpBkjl3S08g6yozTg4+DJ0seO7Zt6lxDqIIzPxV1H/gO
    PGziGuxg4Lr2lZKtejZGZnBF3Pu0ptfgwHwoHZpUA+hYHOMNrr33UfsCgYEAyjTM
    6h2X33yinsE1up5kIla/umJ4DbZXMALGMj7zhNjnEBozsf9wbxjJkxWUtbmVPSEn
    5hbAnoDzSZqyrYmP2Aoh8nBg+1da6S69sle96NrcEqeyZCe7FZcUptpsRMGsnRkX
    IbK+ZuIjfoYL5GeIvEkjHD3oo4hEWm7BJw+K8McCgYAndEEwoeCVV/+C/lTDx8LG
    whheQqaonTUSMo49yfZGR3cXocQvVT5Qv2G42/hi9f9SlNzD/GDg1vl9OgaGmwP7
    0gW0BtiUQuYUaDuM9VP9Z4zcCeNILmNziWVtzBGKir+p2Jpf0aWGj1Oh4B4jrg1X
    KXAtRB67aK8WZolrrS8u8QKBgH8a6aRQreMCCYcZlJiWcANV3Hdg5m5YLD41wDhd
    cd4UKkro7Y36L8bmIe+fJTeGTNV8PgRYUQRRspyV7bNAQXdgBy7KjWvw4WiqxPuS
    RAJXcPcZCv4CK4vKyGSJ8HzX8ZVe7Dd2kFFD3WPvBxKwMjeJEdTy5YXGZpEiBbB0
    CLaJAoGAJhhfQLYfBjAf/QLA3l83K9DY8ZQZSQaSC6ukIemlFE16HkXiK3ma9ART
    vaSHVtkVbceBjvcrj/lfOwJMavA9WJqHowtzAc9rlHs+5yYWY+seggEUqTJ3tpnn
    JRNuyjCqio2G5+cOr+mdn321C9jsxz37f0fhaXo7kX9klOleWvg=
    -----END RSA PRIVATE KEY-----
    """;

            var rsa = RSA.Create();
            rsa.ImportFromPem(privateKey.ToCharArray());
            var signingKey = new RsaSecurityKey(rsa);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
        new Claim(ClaimTypes.Name, "username"), new Claim(ClaimTypes.Role, "Admin")
        // Add other claims as needed
    }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = "your-issuer",
                Audience = "your-audience",
                SigningCredentials = new SigningCredentials(
                   signingKey,
                    SecurityAlgorithms.RsaSha256) // Using RSA-SHA256 algorithm
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = tokenHandler.WriteToken(token);

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
