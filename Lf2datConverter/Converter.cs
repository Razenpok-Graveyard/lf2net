using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Lf2datConverter
{
    public abstract class Block
    {
        public string Name;
        public Dictionary<string, string> Fields = new Dictionary<string, string>();
    }

    public class BmpBlock : Block
    {
        public List<SpriteFile> SpriteFiles = new List<SpriteFile>();
    }

    public class FrameBlock : Block
    {
        public List<FrameElementBlock> FrameElements = new List<FrameElementBlock>();
    }

    public class FrameElementBlock : Block { }

    public class Dat
    {
        public BmpBlock Bmp;
        public List<FrameBlock> Frames= new List<FrameBlock>();
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

        private static Dat GetBlocks(IEnumerable<string> lines)
        {
            var dat = new Dat();
            var bmp = new BmpBlock {Name = "bmp"};
            var currentFrame = new FrameBlock();
            var currentFields = new Dictionary<string, string>();
            FrameElementBlock currentFrameElement = null;
            foreach (var line in lines)
            {
                if (line.Contains("<bmp_begin>"))
                {
                    continue;
                }
                if (line.Contains("<frame>"))
                {
                    var firstLine = line.Substring(8).Split(' ');
                    currentFrame = new FrameBlock {Name = "frame"};
                    currentFields.Add("Number", firstLine[0]);
                    currentFields.Add("Name", firstLine[1]);
                    continue;
                }
                if (line.Contains("<bmp_end>"))
                {
                    bmp.Fields = currentFields;
                    dat.Bmp = bmp;
                    currentFields = new Dictionary<string, string>();
                }
                if (line.Contains("<frame_end>"))
                {
                    currentFrame.Fields = currentFields;
                    dat.Frames.Add(currentFrame);
                    currentFrame = new FrameBlock();
                    currentFields = new Dictionary<string, string>();
                    continue;
                }
                var parts = line.Split(new[] {" ", ":"}, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 0) continue;
                if (parts.Length == 1)
                {
                    if (parts[0].Contains("end"))
                    {
                        if (currentFrameElement != null)
                        {
                            currentFrame.FrameElements.Add(currentFrameElement);
                        }
                        currentFrameElement = null;
                    }
                    else
                    {
                        currentFrameElement = new FrameElementBlock {Name = parts[0]};
                    }
                    continue;
                }
                if (parts[0].Contains("file"))
                {
                    bmp.SpriteFiles.Add(ParseSpriteFile(parts));
                    continue;
                }
                var targetFields = currentFrameElement != null ? currentFrameElement.Fields : currentFields;
                for (var i = 0; i < parts.Length; i += 2)
                {
                    targetFields.Add(parts[i], parts[i+1]);
                }
            }
            return dat;
        }

        private static Character ParseCharacter(Dat dat)
        {
            var result = ParseBmp(dat.Bmp);
            if (result == null) return null;
            result.Frames = dat.Frames
                .Select(ParseFrame)
                .ToList();
            return result;
        }

        private static Character ParseBmp(BmpBlock bmpBlock)
        {
            if (bmpBlock.Name != "bmp")
                return null;
            var fields = bmpBlock.Fields;
            var character = new Character
            {
                Name = ParseString(fields, "name"),
                Head = ParseString(fields, "head"),
                Small = ParseString(fields, "small"),
                SpriteFiles = bmpBlock.SpriteFiles,
                WalkingFrameRate = ParseInt(fields, "walking_frame_rate"),
                WalkingSpeed = ParseFloat(fields, "walking_speed"),
                WalkingSpeedZ = ParseFloat(fields, "walking_speedz"),
                RunningFrameRate = ParseInt(fields, "running_frame_rate"),
                RunningSpeed = ParseFloat(fields, "running_speed"),
                RunningSpeedZ = ParseFloat(fields, "running_speedz"),
                HeavyWalkingSpeed = ParseFloat(fields, "heavy_walking_speed"),
                HeavyWalkingSpeedZ = ParseFloat(fields, "heavy_walking_speedz"),
                HeavyRunningSpeed = ParseFloat(fields, "heavy_running_speed"),
                HeavyRunningSpeedZ = ParseFloat(fields, "heavy_running_speedz"),
                JumpHeight = ParseFloat(fields, "jump_height"),
                JumpDistance = ParseFloat(fields, "jump_distance"),
                JumpDistanceZ = ParseFloat(fields, "jump_distancez"),
                DashHeight = ParseFloat(fields, "dash_height"),
                DashDistance = ParseFloat(fields, "dash_distance"),
                DashDistanceZ = ParseFloat(fields, "dash_distancez"),
                RowingHeight = ParseFloat(fields, "rowing_height"),
                RowingDistance = ParseFloat(fields, "rowing_distance")
            };
            return character;
        }

        private static float ParseFloat(Dictionary<string, string> dict, string key)
        {
            string s;
            return dict.TryGetValue(key, out s) ? float.Parse(s, CultureInfo.InvariantCulture) : 0;
        }

        private static int ParseInt(Dictionary<string, string> dict, string key)
        {
            string s;
            return dict.TryGetValue(key, out s) ? int.Parse(s) : 0;
        }

        private static string ParseString(Dictionary<string, string> dict, string key)
        {
            string s;
            return dict.TryGetValue(key, out s) ? s : "";
        }

        private static SpriteFile ParseSpriteFile(IReadOnlyList<string> parts)
        {
            var id = parts[0].Substring(4).Trim('(', ')').Split('-');
            var file = new SpriteFile
            {
                StartID = int.Parse(id[0]),
                FinishID = int.Parse(id[1]),
                Path = parts[1],
                Width = int.Parse(parts[3]),
                Height = int.Parse(parts[5]),
                Rows = int.Parse(parts[7]),
                Columns = int.Parse(parts[9])
            };
            return file;
        }

        private static CharacterFrame ParseFrame(FrameBlock block)
        {
            if (block.Name != "frame")
                return null;
            var fields = block.Fields;
            var frame = new CharacterFrame
            {
                Number = ParseInt(fields, "Number"),
                Name = ParseString(fields, "Name"),
                Picture = ParseInt(fields, "pic"),
                State = ParseInt(fields, "state"),
                Wait = ParseInt(fields, "wait"),
                Next = ParseInt(fields, "next"),
                DVX = ParseInt(fields, "dvx"),
                DVY = ParseInt(fields, "dvy"),
                DVZ = ParseInt(fields, "dvz"),
                CenterX = ParseInt(fields, "centerx"),
                CenterY = ParseInt(fields, "centery"),
                HitA = ParseInt(fields, "hit_a"),
                HitD = ParseInt(fields, "hit_d"),
                HitJ = ParseInt(fields, "hit_j"),
                HitFA = ParseInt(fields, "hit_Fa"),
                HitFJ = ParseInt(fields, "hit_Fj"),
                HitUA = ParseInt(fields, "hit_Ua"),
                HitUJ = ParseInt(fields, "hit_Uj"),
                HitDA = ParseInt(fields, "hit_Da"),
                HitDJ = ParseInt(fields, "hit_Dj"),
                HitJA = ParseInt(fields, "hit_ja"),
                MP = ParseInt(fields, "mp"),
                Sound = ParseString(fields, "sound"),
                FrameElements = block.FrameElements.Select(ParseFrameElement).ToList()
            };
            return frame;
        }

        private static IFrameElement ParseFrameElement(FrameElementBlock block)
        {
            IFrameElement frameElement = null;
            var fields = block.Fields;
            switch (block.Name)
            {
                case "wpoint":
                {
                    var weaponPoint = new WeaponPoint();
                    weaponPoint.Kind = ParseInt(fields, "kind");
                    frameElement = weaponPoint;
                    break;
                }
            }
            return frameElement;
        }
    }
}


