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
        static void Main(string[] args)
        {
            var fileName = "davis.dat";//Console.ReadLine();
            if (fileName == null || !File.Exists(fileName)) return;
            // First 123 bytes of lf2 .dat files are useless
            var bytes = File.ReadAllBytes(fileName).Skip(123);
            var text = Decryptor.DecryptByteSequence(bytes);
            var ch = Converter.ConvertDat(text);
            File.WriteAllText(fileName.Split('.')[0] + ".txt", text);
        }
    }
}
