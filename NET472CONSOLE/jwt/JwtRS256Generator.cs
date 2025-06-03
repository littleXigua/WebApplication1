using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

public class JwtRS256Generator
{
    private readonly RSACryptoServiceProvider _rsa;

    public JwtRS256Generator(string xmlkey)
    {
        _rsa = new RSACryptoServiceProvider(2048);
        
        _rsa.FromXmlString(xmlkey);
    }

    // 生成 JWT
    public string GenerateToken(Dictionary<string, object> payload)
    {
        var algorithm = new RS256Algorithm(_rsa, _rsa);
        var serializer = new JsonNetSerializer();
        var urlEncoder = new JwtBase64UrlEncoder();
        var encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

        return encoder.Encode(payload, string.Empty);
    }

    // 验证 JWT
    public bool ValidateToken(string token)
    {
        try
        {
            var serializer = new JsonNetSerializer();
            var urlEncoder = new JwtBase64UrlEncoder();
            var validator = new JwtValidator(serializer, new UtcDateTimeProvider());
            var RS256Algorithm = new RS256Algorithm(_rsa,_rsa);
            var decoder = new JwtDecoder(serializer,validator,urlEncoder, RS256Algorithm);          
            var jwtpart = new JwtParts(token);
            var payload = decoder.Decode(jwtpart, true);
            Console.WriteLine("JWT Payload: " + payload);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("JWT Validation Failed: " + ex.Message);
            return false;
        }
    }  

}