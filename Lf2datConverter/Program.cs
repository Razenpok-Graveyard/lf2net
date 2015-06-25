using System.IO;
using System.Linq;
using LF2NetCore;
using Newtonsoft.Json;

namespace LF2datConverter
{
    static class Program
    {
        static void Main(string[] args)
        {
            var fileName = "davis.dat";//Console.ReadLine();
            if (fileName == null || !File.Exists(fileName)) return;
            var objectName = fileName.Split('.')[0];
            // First 123 bytes of lf2 .dat files are useless
            var bytes = File.ReadAllBytes(fileName)
                .Skip(123);
            var text = Decryptor.DecryptByteSequence(bytes);
            var baseCharacter = BaseParser.ParseDat(text);
            File.WriteAllText(objectName + ".txt", text);
            var json = JsonConvert.SerializeObject(baseCharacter, Formatting.Indented);
            File.WriteAllText(objectName + "_base.json", json);
            var convenientCharacter = ConvenientConverter.ConvertToConvenientCharacter(baseCharacter);
            json = JsonConvert.SerializeObject(convenientCharacter, Formatting.Indented);
            File.WriteAllText(objectName + "_conv.json", json);
            var coreCharacter = CoreConverter.ConvertConvenient(convenientCharacter);
            GenerateFiles(coreCharacter);
            json = JsonConvert.SerializeObject(coreCharacter, Formatting.Indented);
            File.WriteAllText(coreCharacter.Name + "/" + objectName + ".json", json);
        }

        private static void GenerateFiles(Character character)
        {
            var dirName = character.Name;
            if (!Directory.Exists(dirName))
                Directory.CreateDirectory(dirName);
            CreateFile(dirName, character.HeadPicture);
            CreateFile(dirName, character.SmallPicture);
            foreach (var file in character.SpriteFiles)
            {
                CreateFile(dirName, file.Filename);
            }
        }

        private static void CreateFile(string directory, string name)
        {
            var filePath = directory + "/" + name;
            if (!File.Exists(filePath))
                File.Create(filePath);
        }
    }
}
