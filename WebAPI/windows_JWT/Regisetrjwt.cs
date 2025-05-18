using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;

namespace WebAPI
{
    public static class Class1
    {

        public static IServiceCollection Regisetrjwt(this IServiceCollection service)
        {
            service.AddAuthentication(options =>
            {
                options.DefaultScheme = "negotiate"; // 或者 "jwt_or_ntlm" 自定义方案
                options.DefaultChallengeScheme = "negotiate"; // Windows 认证的挑战方案
            })
.AddJwtBearer("jwt", options =>
{
    var pulickey = """
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
    rsa.ImportFromPem(pulickey.ToCharArray());
    var signingKey = new RsaSecurityKey(rsa);

    options.TokenValidationParameters = new TokenValidationParameters
    {
        //Issuer = "your-issuer",
        //Audience = "your-audience",

        ValidateIssuer = true,
        ValidIssuer = "your-issuer",
        ValidateAudience = true,
        ValidAudience = "your-audience",
        ValidateIssuerSigningKey = true,
        IssuerSigningKey= signingKey        
    };
})
.AddNegotiate("negotiate", options =>
{
    // Windows 身份验证配置
    options.PersistNtlmCredentials = true;
});

            return service;
        }
    }
}