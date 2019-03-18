using System;
using System.Security.Cryptography;
using Wiry.Base32;

namespace oAuthClientSecretGenerator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var cryptoRandomDataGenerator = new RNGCryptoServiceProvider();
            var length = 16;
            var buffer = new byte[length];
            cryptoRandomDataGenerator.GetBytes(buffer);
            var uniq = Base32Encoding.ZBase32.GetString(buffer);
            Console.Out.WriteLine(uniq);
            Console.In.ReadLine();
        }
    }
}
