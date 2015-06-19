using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

namespace Lf2datConverter
{
    static class Program
    {
        private const string EncryptionPhrase = "odBearBecauseHeIsVeryGoodSiuHungIsAGo";
        static readonly IEnumerator<char> EncryptionKey = EncryptionPhrase.GetEnumerator();

        static void Main(string[] args)
        {
            var fileName = "davis.dat";//Console.ReadLine();
            if (fileName == null || !File.Exists(fileName)) return;
            // First 123 bytes of lf2 .dat files are useless
            var bytes = File.ReadAllBytes(fileName).Skip(123);
            var text = DecryptByteSequence(bytes);
            File.WriteAllText(fileName.Split('.')[0] + ".txt", text);
        }

        private static string DecryptByteSequence(IEnumerable<byte> byteStream)
        {
            EncryptionKey.Reset();
            return new string(byteStream.Select(DecryptByte).ToArray());
        }

        static readonly Func<byte, char> DecryptByte = b => (char)(b - NextEncryptionByte);

        static byte NextEncryptionByte
        {
            get
            {
                if (!EncryptionKey.MoveNext())
                {
                    EncryptionKey.Reset();
                    EncryptionKey.MoveNext();
                }
                return (byte)EncryptionKey.Current;
            }
        }
    }
}
