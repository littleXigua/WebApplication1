using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NET472CONSOLE.KeyGen
{
    internal class KeyGeneratro
    {

        public KeyGeneratro() { }

        public static void Create()
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                // 获取私钥（XML 格式，包含公钥和私钥）
                string privateKeyXml = rsa.ToXmlString(true); // true 表示包含私钥

                // 获取公钥（XML 格式，仅公钥）
                string publicKeyXml = rsa.ToXmlString(false); // false 表示仅公钥

                Console.WriteLine("Private Key (XML):\n" + privateKeyXml);
                Console.WriteLine("\nPublic Key (XML):\n" + publicKeyXml);

                // （可选）转换为 PEM 格式（手动处理）
                string privateKeyPem = ConvertToPem(privateKeyXml, true);
                string publicKeyPem = ConvertToPem(publicKeyXml, false);

                Console.WriteLine("\nPrivate Key (PEM):\n" + privateKeyPem);
                Console.WriteLine("\nPublic Key (PEM):\n" + publicKeyPem);
            }
        }





        // 将 XML 格式的密钥转换为 PEM 格式（简化版）
        static string ConvertToPem(string xmlKey, bool isPrivate)
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(xmlKey);
                byte[] keyBytes = isPrivate ? rsa.ExportCspBlob(true) : rsa.ExportCspBlob(false);
                string base64 = Convert.ToBase64String(keyBytes);
                string header = isPrivate ? "-----BEGIN RSA PRIVATE KEY-----" : "-----BEGIN PUBLIC KEY-----";
                string footer = isPrivate ? "-----END RSA PRIVATE KEY-----" : "-----END PUBLIC KEY-----";
                return $"{header}\n{base64}\n{footer}";
            }
        }


        public static void KeyTest()
        {
            // 原始数据
            string data = "Hello, RSA!";
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);

            // 生成密钥对
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                string publicKeyXml = rsa.ToXmlString(false);
                string privateKeyXml = rsa.ToXmlString(true);

                // 用私钥签名
                byte[] signature = rsa.SignData(dataBytes, new SHA256CryptoServiceProvider());
                Console.WriteLine("Signature (Base64):\n" + Convert.ToBase64String(signature));

                // 用公钥验证
                using (var rsaPublic = new RSACryptoServiceProvider())
                {
                    rsaPublic.FromXmlString(publicKeyXml);
                    bool isValid = rsaPublic.VerifyData(
                        dataBytes,
                        new SHA256CryptoServiceProvider(),
                        signature
                    );
                    Console.WriteLine("\nSignature Valid? " + isValid);
                }
            }

        }
    }
}
