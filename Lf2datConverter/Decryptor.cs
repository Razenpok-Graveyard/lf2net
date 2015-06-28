using System;
using System.Collections.Generic;
using System.Linq;

namespace LF2datConverter
{
    internal static class Decryptor
    {
        private const string EncryptionPhrase = "odBearBecauseHeIsVeryGoodSiuHungIsAGo";
        private static readonly IEnumerator<char> EncryptionKey = EncryptionPhrase.GetEnumerator();
        private static readonly Func<byte, char> DecryptByte = b => (char) (b - NextEncryptionByte);

        private static byte NextEncryptionByte
        {
            get
            {
                if (!EncryptionKey.MoveNext())
                {
                    EncryptionKey.Reset();
                    EncryptionKey.MoveNext();
                }
                return (byte) EncryptionKey.Current;
            }
        }

        public static string DecryptByteSequence(IEnumerable<byte> byteStream)
        {
            EncryptionKey.Reset();
            return new string(byteStream.Select(DecryptByte).ToArray());
        }
    }
}