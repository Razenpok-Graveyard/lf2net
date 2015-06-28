using System.IO;
using System.Linq;
using Microsoft.Xna.Framework.Content;

namespace LF2Net.Loaders
{
    public class ContentLoader
    {
        public static string ContentFolder;
        private static GameContent content;
        private static ContentManager contentManager;

        public static GameContent LoadContent(Game game)
        {
            ContentFolder = game.Content.RootDirectory;
            content = new GameContent();
            contentManager = game.Content;
            LoadCharacters();
            return content;
        }

        private static void LoadCharacters()
        {
            var charactersFolder = Path.Combine(ContentFolder, "Characters");
            var directories = Directory.GetDirectories(charactersFolder);
            content.characters = directories
                .Select(dir => CharacterLoader.LoadFromPath(dir, contentManager))
                .Where(character => character != null)
                .ToList();
        }
    }
}