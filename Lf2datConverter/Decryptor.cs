using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LF2datConverter
{
    static class Decryptor
    {
        private const string EncryptionPhrase = "odBearBecauseHeIsVeryGoodSiuHungIsAGo";
        static readonly IEnumerator<char> EncryptionKey = EncryptionPhrase.GetEnumerator();

        public static string DecryptByteSequence(IEnumerable<byte> byteStream)
        {
            EncryptionKey.Reset();
            return new string(byteStream
                .Select(DecryptByte)
                .ToArray());
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
