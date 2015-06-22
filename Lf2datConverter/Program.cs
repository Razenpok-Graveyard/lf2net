using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Lf2datConverter
{
    static class Program
    {
        static void Main(string[] args)
        {
            var fileName = "davis.dat";//Console.ReadLine();
            if (fileName == null || !File.Exists(fileName)) return;
            // First 123 bytes of lf2 .dat files are useless
            var bytes = File.ReadAllBytes(fileName)
                .Skip(123);
            var text = Decryptor.DecryptByteSequence(bytes);
            var baseCharacter = BaseParser.ParseDat(text);
            File.WriteAllText(fileName.Split('.')[0] + ".txt", text);
            var json = JsonConvert.SerializeObject(baseCharacter, Formatting.Indented);
            File.WriteAllText(fileName.Split('.')[0] + "_base.json", json);
            var convenientCharacter = ConvenientConverter.ConvertToConvenientCharacter(baseCharacter);
            json = JsonConvert.SerializeObject(convenientCharacter, Formatting.Indented);
            File.WriteAllText(fileName.Split('.')[0] + "_conv.json", json);
            ExtractCharacter(convenientCharacter);
            //var finalCharacter = FinalConverter.ConvertCharacter(convenientCharacter);
            //json = JsonConvert.SerializeObject(finalCharacter, Formatting.Indented);
            //File.WriteAllText(fileName.Split('.')[0] + ".json", json);
        }

        private static void ExtractCharacter(dat.Convenient.Character character)
        {
            var dirName = character.Name;
            if (!Directory.Exists(dirName))
                Directory.CreateDirectory(dirName);
            NormalizeFiles(character);
            CreateFile(dirName, character.HeadPicture);
            CreateFile(dirName, character.SmallPicture);
            foreach (var fileName in character.SpriteFiles)
            {
                CreateFile(dirName, fileName.Sprite);
            }
            var json = JsonConvert.SerializeObject(character, Formatting.Indented);
            File.WriteAllText(dirName + @"\" + dirName + ".json", json);
        }

        private static void CreateFile(string directory, string name)
        {
            var filePath = directory + @"\" + name;
            if (!File.Exists(filePath))
                File.Create(filePath);
        }

        private static void NormalizeFiles(dat.Convenient.Character character)
        {
            character.HeadPicture = ConvertPicture(character.HeadPicture);
            character.SmallPicture = ConvertPicture(character.SmallPicture);
            foreach (var file in character.SpriteFiles)
            {
                file.Sprite = ConvertPicture(file.Sprite);
            }
        }

        private static string ConvertPicture(string oldPath)
        {
            return oldPath.Split('\\').Last().Split('.').First() + ".png";
        }

        private static string ConvertSound(string oldPath)
        {
            return oldPath.Split('\\').Last().Split('.').First() + ".wav";
        }
    }
}
