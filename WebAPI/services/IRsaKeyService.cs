using System.Security.Cryptography;

public interface IRsaKeyService
{
    (string publicKey, string privateKey) GenerateKeys(int keySize = 2048);
    string GetPublicKey();
    string GetPrivateKey();
}

public class RsaKeyService : IRsaKeyService
{
    private readonly string _publicKey;
    private readonly string _privateKey;

    public RsaKeyService(int keySize = 2048)
    {
        var rsa = RSA.Create(keySize);
        _privateKey = rsa.ExportRSAPrivateKeyPem();
        _publicKey = rsa.ExportRSAPublicKeyPem();
    }

    public (string publicKey, string privateKey) GenerateKeys(int keySize = 2048)
    {
        var rsa = RSA.Create(keySize);
        return (rsa.ExportRSAPublicKeyPem(), rsa.ExportRSAPrivateKeyPem());
    }

    public string GetPublicKey() => _publicKey;
    public string GetPrivateKey() => _privateKey;
}