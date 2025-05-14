using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using JWT.Algorithms;
using NET472CONSOLE.KeyGen;

namespace NET472CONSOLE
{
    internal class Program
    {
        static void Main(string[] args)
        {
           // KeyGeneratro.KeyTest();

            Console.WriteLine("start");

            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                string privateKeyXml = rsa.ToXmlString(true);  // 包含私钥
                string publicKeyXml = rsa.ToXmlString(false); // 仅公钥

                // 2. 封装 RSA256 算法
                var rsa256 = new RSA256Algorithm(privateKeyXml);

                // 3. 签名数据
                string data = "Hello, RSA256!";
                string signature = rsa256.Sign(data);
                Console.WriteLine("Signature (Base64):\n" + signature);

                // 4. 验证签名（使用公钥）
                var verifier = new RSA256Algorithm(publicKeyXml);
                bool isValid = verifier.Verify(data, signature);
                Console.WriteLine("\nSignature Valid? " + isValid);
            }

        }
    }
}
