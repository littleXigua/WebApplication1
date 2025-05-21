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


            var txt=@"

"

            KeyGenerator.Create();

            Console.WriteLine("start");

            var privatekey = "<RSAKeyValue><Modulus>mdkAPeOvFVdGmv9VjlbhwuAZzTAG2rY+BR5DNU4y0ulMUETZYFP4oPCcraRzZas6ydeS9greYAFhbou3Ua15/W2BO3+vN/tYFzOdyWpXksBrtuStLiRfZVf1/tNHkjAmSs0dpFhHCtrJVcmPsNtYE0GNOCa8XJTdwgeWZJZONBdziJkV0g4ZEx01W96Urj3shZP9cAfvWzw5lA3cPiPyYGh3kodE/1QoTUdlZI0iWOU8K/lAC6pVXNXO4jvWIfkpFyQfAin3jlXA9XvKrlKQqAW7JNZEh9jKMa0Ml3bXd34acQdDSJimQUq/kxi79Xvv4kVLQWu89ZPijn3JahCmTQ==</Modulus><Exponent>AQAB</Exponent><P>xX8557AKHRDYkc6yc38oSisDNQbUPujWHy+Utl/2QD18QhPCLwQOiVvcWzK/PVOYBjnnA/E1sNaXu+uWvANzGA6y7EeIUDm6T9ALwzNNU0/U20+dK7VQUuIanjqAEI/1PkJ5/pSGMs3VMVCWO0B7e3p18T639clwPO8Jrdx8QbM=</P><Q>x2u4IeLRIJWHB55E2li21NfxOKLy94eA4bUEceBemf7xrn2HmGKU+2ncis7sTVdec1eqzmCtqxttk/Vw6U8RP+xrwxoLsotee8iG7kaFC4ztc4bzzPgFjLLn+4CzBnsi5FzhajvRH8uMc6bqcwwBJ50xITPWnnm3/UcmGc67d/8=</Q><DP>iIiTBHwsEboCrpJhxfzjYprnxbHp62/WD4s6WPZwbCrVi2zTcuNwFT+/UAD+OqxezfcboRsRTiackVqmQ2ZzG++VfElbkHLIPcMLTrBZxb3L3q3kb0ISvKn5UugIfuq21Yrpgbk2KUspFsWqhl3mwA+CF/yO+sl+4XEzlNF3yYc=</DP><DQ>W82xFJhwIYn0gYNQuxu//zfx6lrJcz5EDKPiifH6WvZMmEFdnL81SpAvYQ9lJS2lY3/mN9+102FrDoQdGKq3jC/wY/6nh+g09NMZmrOIKAsWfOqRbbh4BY+Oz+8tezxQkcRYWSl0WijAXbVnlNLf7TamGVzd3lK2arjIR3UuRE0=</DQ><InverseQ>cLO3FYlvPUoJHtKXHFKSCVKDCmbhKOODs8QfPYSY1oaJ3bHgdoobrcQYs0Xrb4PGEd7G8+3U4Gg2wWZRmSNI9rztcPWNSKPfrQK3iQgGVkRc2IFhUcdNTQW1+CdyCj/arqQ9POfDT7f/ukhIhsd0eGH0+yGe9DLQkemt1vNle0E=</InverseQ><D>iXW2HZYdjXfZggu5qeA+/xu0cf0DYs+KQpBzhGFGT+RbZyzc/JCKiWHy28MfyCth745JpMS5RBZ3s4nqVXQmlyUMXtkK+X39i113EcuOS3TZySQxCBU/lxEsnm6weBHFZPg4D+tALpalZZ0eYerZmvrq7szNZ/sq2H5nBaC2ZxtaXmovKxiJrXMMrDzAsjEqdheTIpQZpOcB6uLfH1u2Og9SKO2EFrVcp3XOo87k82dtQQBArcj2eVWo5K55HiNF4z+IKXdSq6tnn4g2GQ6hbvfjAVwxFh/699fjoKJ4f2HsNs7R+LiFwnUobPVC30icmD0ejNZV0nUWiS/Jp3eykQ==</D></RSAKeyValue>";
            var publickey = "<RSAKeyValue><Modulus>mdkAPeOvFVdGmv9VjlbhwuAZzTAG2rY+BR5DNU4y0ulMUETZYFP4oPCcraRzZas6ydeS9greYAFhbou3Ua15/W2BO3+vN/tYFzOdyWpXksBrtuStLiRfZVf1/tNHkjAmSs0dpFhHCtrJVcmPsNtYE0GNOCa8XJTdwgeWZJZONBdziJkV0g4ZEx01W96Urj3shZP9cAfvWzw5lA3cPiPyYGh3kodE/1QoTUdlZI0iWOU8K/lAC6pVXNXO4jvWIfkpFyQfAin3jlXA9XvKrlKQqAW7JNZEh9jKMa0Ml3bXd34acQdDSJimQUq/kxi79Xvv4kVLQWu89ZPijn3JahCmTQ==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

            JwtRS256Generator generator = new JwtRS256Generator(privatekey);

            var token = generator.GenerateToken(new Dictionary<string, object>
            {
                { "sub", "1234567890" },
                { "name", "John Doe" },
                { "admin", true }
            });


            Console.WriteLine("JWT Token: " + token);


            JwtRS256Generator Validatepublic = new JwtRS256Generator(publickey);

            if (Validatepublic.ValidateToken(token))
            {
                Console.WriteLine("true'");
            }



        }


        static void Test()
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                string privateKeyXml = rsa.ToXmlString(true);  // 包含私钥
                string publicKeyXml = rsa.ToXmlString(false); // 仅公钥

                // 2. 封装 RSA256 算法
                var rsa256 = new RS256Algorithm(rsa, rsa);

                // 3. 签名数据
                string data = "Hello, RSA256!";
                byte[] byte2 = Encoding.UTF8.GetBytes("test");
                byte[] bytes = Encoding.UTF8.GetBytes(data);
                byte[] signature = rsa256.Sign(bytes);
                Console.WriteLine("Signature (Base64):\n" + signature);

                // 4. 验证签名（使用公钥）
                var verifier = new RS256Algorithm(rsa, rsa);
                bool isValid = verifier.Verify(byte2, signature);
                Console.WriteLine("\nSignature Valid? " + isValid);
            }

        }
    }
}
