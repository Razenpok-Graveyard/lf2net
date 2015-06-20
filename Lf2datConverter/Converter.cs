using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Lf2datConverter
{
    public struct Block
    {
        public string Name;
        public Dictionary<string, string> Fields;

        public Block(string name)
        {
            Name = name;
            Fields = new Dictionary<string, string>();
        }
    }

    static class Converter
    {
        public static Character ConvertDat(string dat)
        {
            var lines = dat.Split(new[] {"\n"}, StringSplitOptions.RemoveEmptyEntries);
            var blocks = GetBlocks(lines);
            var character = ParseCharacter(blocks);
            return character;
        }

        private static List<Block> GetBlocks(IEnumerable<string> lines)
        {
            var blocks = new List<Block>();
            var currentBlock = new Block();
            var currentFrameElement = "";
            var currentFrameElementContents = "";
            foreach (var line in lines)
            {
                if (line.Contains("<bmp_begin>"))
                {
                    currentBlock = new Block("bmp");
                    continue;
                }
                if (line.Contains("<frame>"))
                {
                    currentBlock = new Block("frame");
                    var firstLine = line.Substring(8).Split(' ');
                    currentBlock.Fields.Add("Number", firstLine[0]);
                    currentBlock.Fields.Add("Name", firstLine[1]);
                    continue;
                }
                if (line.Contains("<bmp_end>") || line.Contains("<frame_end>"))
                {
                    blocks.Add(currentBlock);
                    currentBlock = new Block();
                    continue;
                }
                var parts = line.Split(new[] {" ", ":"}, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 0) continue;
                if (parts.Length == 1)
                {
                    if (parts[0].Contains("end"))
                    {
                        if (currentBlock.Fields.ContainsKey(currentFrameElement))
                        {
                            currentBlock.Fields[currentFrameElement] += " | " + currentFrameElementContents;
                        }
                        else
                        {
                            currentBlock.Fields.Add(currentFrameElement, currentFrameElementContents);
                        }
                        currentFrameElement = "";
                        currentFrameElementContents = "";
                    }
                    else
                    {
                        currentFrameElement = parts[0];
                    }
                    continue;
                }
                if (parts[0].Contains("file"))
                {
                    if (!currentBlock.Fields.ContainsKey("SpriteFiles"))
                    {
                        currentBlock.Fields["SpriteFiles"] = "";
                    }
                    currentBlock.Fields["SpriteFiles"] += line + " | ";
                    continue;
                }
                if (currentFrameElement != "")
                {
                    currentFrameElementContents += line;
                }
                else
                {
                    for (var i = 0; i < parts.Length; i += 2)
                    {
                        currentBlock.Fields.Add(parts[i], parts[i+1]);
                    }
                }
            }
            return blocks;
        }

        private static Character ParseCharacter(List<Block> blocks)
        {
            var result = ParseBmp(blocks.First());
            if (result == null) return null;
            result.Frames = blocks.Skip(1)
                .Select(ParseFrame)
                .ToList();
            return result;
        }

        private static Character ParseBmp(Block block)
        {
            var character = new Character();
            if (block.Name != "bmp")
                return null;
            var fields = block.Fields;
            character.Name = fields["name"];
            character.Head = fields["head"];
            character.Small = fields["small"];
            var files = fields["SpriteFiles"].Split('|')
                .Select(file => file.Split(new[] {" ", ":"}, StringSplitOptions.RemoveEmptyEntries));
            foreach (var file in files.Where(file => file.Length > 0))
            {
                character.SpriteFiles.Add(ParseSpriteFile(file));
            }
            character.WalkingFrameRate = ParseInt(fields["walking_frame_rate"]);
            character.WalkingSpeed = ParseFloat(fields["walking_speed"]);
            character.WalkingSpeedZ = ParseFloat(fields["walking_speedz"]);
            character.RunningFrameRate = ParseInt(fields["running_frame_rate"]);
            character.RunningSpeed = ParseFloat(fields["running_speed"]);
            character.RunningSpeedZ = ParseFloat(fields["running_speedz"]);
            character.HeavyWalkingSpeed = ParseFloat(fields["heavy_walking_speed"]);
            character.HeavyWalkingSpeedZ = ParseFloat(fields["heavy_walking_speedz"]);
            character.HeavyRunningSpeed = ParseFloat(fields["heavy_running_speed"]);
            character.HeavyRunningSpeedZ = ParseFloat(fields["heavy_running_speedz"]);
            character.JumpHeight = ParseFloat(fields["jump_height"]);
            character.JumpDistance = ParseFloat(fields["jump_distance"]);
            character.JumpDistanceZ = ParseFloat(fields["jump_distancez"]);
            character.DashHeight = ParseFloat(fields["dash_height"]);
            character.DashDistance = ParseFloat(fields["dash_distance"]);
            character.DashDistanceZ = ParseFloat(fields["dash_distancez"]);
            character.RowingHeight = ParseFloat(fields["rowing_height"]);
            character.RowingDistance = ParseFloat(fields["rowing_distance"]);
            return character;
        }

        private static float ParseFloat(string s)
        {
            return float.Parse(s, CultureInfo.InvariantCulture);
        }

        private static int ParseInt(string s)
        {
            return int.Parse(s);
        }

        private static SpriteFile ParseSpriteFile(IReadOnlyList<string> parts)
        {
            var file = new SpriteFile();
            var id = parts[0].Substring(4).Trim('(', ')').Split('-');
            file.StartID = ParseInt(id[0]);
            file.FinishID = ParseInt(id[1]);
            file.Path = parts[1];
            file.Width = ParseInt(parts[3]);
            file.Height = ParseInt(parts[5]);
            file.Rows = ParseInt(parts[7]);
            file.Columns = ParseInt(parts[9]);
            return file;
        }

        private static CharacterFrame ParseFrame(Block block)
        {
            var result = new CharacterFrame();
            return result;
        }
    }
}
