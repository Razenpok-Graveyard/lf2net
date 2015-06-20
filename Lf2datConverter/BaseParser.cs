using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Lf2datConverter.dat.Base;

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

    public class SourceDat
    {
        public BmpBlock Bmp;
        public List<FrameBlock> Frames= new List<FrameBlock>();
    }

    static class BaseParser
    {
        public static Character ParseDat(string dat)
        {
            var lines = dat.Split(new[] {"\n"}, StringSplitOptions.RemoveEmptyEntries);
            var sourceDat = GetSourceDat(lines);
            var character = ParseCharacter(sourceDat);
            return character;
        }

        private static SourceDat GetSourceDat(IEnumerable<string> lines)
        {
            var dat = new SourceDat();
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
                if (parts[0].Contains("catchingact"))
                {
                    var catchArgs = line.Split(new[] {"catchingact: ", "caughtact: "},
                        StringSplitOptions.RemoveEmptyEntries);
                    if (currentFrameElement != null)
                    {
                        currentFrameElement.Fields["catchingact"] = catchArgs[1];
                        currentFrameElement.Fields["caughtact"] = catchArgs[2];
                    }
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

        private static Character ParseCharacter(SourceDat sourceDat)
        {
            var result = ParseBmp(sourceDat.Bmp);
            if (result == null) return null;
            result.Frames = sourceDat.Frames
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
                Sprite = parts[1],
                W = int.Parse(parts[3]),
                H = int.Parse(parts[5]),
                Row = int.Parse(parts[7]),
                Col = int.Parse(parts[9])
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
                FrameNumber = ParseInt(fields, "Number"),
                Name = ParseString(fields, "Name"),
                Pic = ParseInt(fields, "pic"),
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

        private static FrameElement ParseFrameElement(FrameElementBlock block)
        {
            FrameElement frameElement = null;
            var fields = block.Fields;
            switch (block.Name)
            {
                case "wpoint":
                {
                    frameElement = new WeaponPoint
                    {
                        Kind = ParseInt(fields, "kind"),
                        Attacking = ParseInt(fields, "attacking"),
                        Cover = ParseInt(fields, "cover"),
                        DVX = ParseInt(fields, "dvx"),
                        DVY = ParseInt(fields, "dvy"),
                        DVZ = ParseInt(fields, "dvz"),
                        WeaponAct = ParseInt(fields, "weaponact"),
                        X = ParseInt(fields, "x"),
                        Y = ParseInt(fields, "y")
                    };
                    break;
                }
                case "opoint":
                {
                    frameElement = new ObjectPoint
                    {
                        Action = ParseInt(fields, "action"),
                        DVX = ParseInt(fields, "dvx"),
                        DVY = ParseInt(fields, "dvy"),
                        Facing = ParseInt(fields, "facing"),
                        Kind = ParseInt(fields, "kind"),
                        OID = ParseInt(fields, "oid"),
                        X = ParseInt(fields, "x"),
                        Y = ParseInt(fields, "y")
                    };
                    break;
                }
                case "cpoint":
                {
                    var kind = ParseInt(fields, "kind");
                    if (kind == 1)
                        frameElement = new CatchingPoint
                        {
                            AAction = ParseInt(fields, "aaction"),
                            Cover = ParseInt(fields, "cover"),
                            Decrease = ParseInt(fields, "decrease"),
                            DirControl = ParseInt(fields, "dircontrol"),
                            Hurtable = ParseInt(fields, "hurtable"),
                            Injury = ParseInt(fields, "injury"),
                            JAction = ParseInt(fields, "jaction"),
                            TAction = ParseInt(fields, "taction"),
                            ThrowInjury = ParseInt(fields, "throwinjury"),
                            ThrowVX = ParseInt(fields, "throwvx"),
                            ThrowVY = ParseInt(fields, "throwvy"),
                            ThrowVZ = ParseInt(fields, "throwvz"),
                            VAction = ParseInt(fields, "vaction"),
                            X = ParseInt(fields, "x"),
                            Y = ParseInt(fields, "y")
                        };
                    if (kind == 2)
                        frameElement = new CaughtPoint
                        {
                            BackHurtAct = ParseInt(fields, "backhurtact"),
                            FrontHurtAct = ParseInt(fields, "fronthurtact"),
                            X = ParseInt(fields, "x"),
                            Y = ParseInt(fields, "y")
                        };
                    break;
                }
                case "bpoint":
                {
                    frameElement = new BloodPoint
                    {
                        X = ParseInt(fields, "x"),
                        Y = ParseInt(fields, "y")
                    };
                    break;
                }
                case "bdy":
                {
                    frameElement = new Body
                    {
                        H = ParseInt(fields, "h"),
                        Kind = ParseInt(fields, "kind"),
                        W = ParseInt(fields, "w"),
                        X = ParseInt(fields, "x"),
                        Y = ParseInt(fields, "y")
                    };
                    break;
                }
                case "itr":
                {
                    var kind = ParseInt(fields, "kind");
                    switch (kind)
                    {
                        case 0:
                        {
                            frameElement = new NormalHit
                            {
                                ARest = ParseInt(fields, "arest"),
                                BDefend = ParseInt(fields, "bdefend"),
                                DVX = ParseInt(fields, "dvx"),
                                DVY = ParseInt(fields, "dvy"),
                                Effect = ParseInt(fields, "effect"),
                                Fall = ParseInt(fields, "fall"),
                                H = ParseInt(fields, "h"),
                                Injury = ParseInt(fields, "injury"),
                                VRest = ParseInt(fields, "vrest"),
                                ZWidth = ParseInt(fields, "zwidth"),
                                X = ParseInt(fields, "x"),
                                Y = ParseInt(fields, "y"),
                                W = ParseInt(fields, "w")
                            };
                            break;
                        }
                        case 1:
                        {
                            var catchingAct = ParseString(fields, "catchingact").Split(' ');  
                            var caughtAct = ParseString(fields, "caughtact").Split(' ');
                            frameElement = new CatchDoP
                            {
                                CatchingActBack = int.Parse(catchingAct[1]),
                                CatchingActFront = int.Parse(catchingAct[0]),
                                CaughtActBack = int.Parse(caughtAct[1]),
                                CaughtActFront = int.Parse(caughtAct[0]),
                                H = ParseInt(fields, "h"),
                                W = ParseInt(fields, "w"),
                                X = ParseInt(fields, "x"),
                                Y = ParseInt(fields, "y")
                            };
                            break;
                        }
                        case 2:
                        {
                            frameElement = new PickWeapon
                            {
                                H = ParseInt(fields, "h"),
                                VRest = ParseInt(fields, "vrest"),
                                W = ParseInt(fields, "w"),
                                X = ParseInt(fields, "x"),
                                Y = ParseInt(fields, "y")
                            };
                            break;
                        }
                        case 3:
                        {
                            var catchingAct = ParseString(fields, "catchingact").Split(' ');
                            var caughtAct = ParseString(fields, "caughtact").Split(' ');
                            frameElement = new CatchBody
                            {
                                CatchingActBack = int.Parse(catchingAct[1]),
                                CatchingActFront = int.Parse(catchingAct[0]),
                                CaughtActBack = int.Parse(caughtAct[1]),
                                CaughtActFront = int.Parse(caughtAct[0]),
                                H = ParseInt(fields, "h"),
                                W = ParseInt(fields, "w"),
                                X = ParseInt(fields, "x"),
                                Y = ParseInt(fields, "y")
                            };
                            break;
                        }
                        case 4:
                        {
                            frameElement = new Falling
                            {
                                BDefend = ParseInt(fields, "bdefend"),
                                DVX = ParseInt(fields, "dvx"),
                                Fall = ParseInt(fields, "fall"),
                                H = ParseInt(fields, "h"),
                                Injury = ParseInt(fields, "injury"),
                                VRest = ParseInt(fields, "vrest"),
                                X = ParseInt(fields, "x"),
                                Y = ParseInt(fields, "y"),
                                W = ParseInt(fields, "w")
                            };
                            break;
                        }
                        case 5:
                        {
                            frameElement = new WeaponStrength
                            {
                                BDefend = ParseInt(fields, "bdefend"),
                                DVX = ParseInt(fields, "dvx"),
                                Fall = ParseInt(fields, "fall"),
                                H = ParseInt(fields, "h"),
                                Injury = ParseInt(fields, "injury"),
                                X = ParseInt(fields, "x"),
                                Y = ParseInt(fields, "y"),
                                W = ParseInt(fields, "w")
                            };
                            break;
                        }
                        case 6:
                        {
                            frameElement = new SuperPunch
                            {
                                H = ParseInt(fields, "h"),
                                VRest = ParseInt(fields, "vrest"),
                                X = ParseInt(fields, "x"),
                                Y = ParseInt(fields, "y"),
                                W = ParseInt(fields, "w")
                            };
                            break;
                        }
                        case 7:
                        {
                            frameElement = new PickWeapon2
                            {
                                H = ParseInt(fields, "h"),
                                VRest = ParseInt(fields, "vrest"),
                                X = ParseInt(fields, "x"),
                                Y = ParseInt(fields, "y"),
                                W = ParseInt(fields, "w")
                            };
                            break;
                        }
                        case 8:
                        {
                            frameElement = new HealBall
                            {
                                DVX = ParseInt(fields, "dvx"),
                                H = ParseInt(fields, "h"),
                                Injury = ParseInt(fields, "injury"),
                                X = ParseInt(fields, "x"),
                                Y = ParseInt(fields, "y"),
                                W = ParseInt(fields, "w")
                            };
                            break;
                        }
                        case 9:
                        {
                            frameElement = new ReflectiveShield
                            {
                                DVX = ParseInt(fields, "dvx"),
                                Fall = ParseInt(fields, "fall"),
                                H = ParseInt(fields, "h"),
                                Injury = ParseInt(fields, "injury"),
                                VRest = ParseInt(fields, "vrest"),
                                X = ParseInt(fields, "x"),
                                Y = ParseInt(fields, "y"),
                                W = ParseInt(fields, "w")
                            };
                            break;
                        }
                        case 10:
                        {
                            frameElement = new SonataOfDeath
                            {
                                H = ParseInt(fields, "h"),
                                Injury = ParseInt(fields, "injury"),
                                VRest = ParseInt(fields, "vrest"),
                                ZWidth = ParseInt(fields, "zwidth"),
                                X = ParseInt(fields, "x"),
                                Y = ParseInt(fields, "y"),
                                W = ParseInt(fields, "w")
                            };
                            break;
                        }
                        case 11:
                        {
                            frameElement = new SonataOfDeath2
                            {
                                H = ParseInt(fields, "h"),
                                Injury = ParseInt(fields, "injury"),
                                VRest = ParseInt(fields, "vrest"),
                                ZWidth = ParseInt(fields, "zwidth"),
                                X = ParseInt(fields, "x"),
                                Y = ParseInt(fields, "y"),
                                W = ParseInt(fields, "w")
                            };
                            break;
                        }
                        case 14:
                        {
                            frameElement = new SolidObject
                            {
                                H = ParseInt(fields, "h"),
                                VRest = ParseInt(fields, "vrest"),
                                X = ParseInt(fields, "x"),
                                Y = ParseInt(fields, "y"),
                                W = ParseInt(fields, "w")
                            };
                            break;
                        }
                        case 15:
                        {
                            frameElement = new WindWhirlWind
                            {
                                BDefend = ParseInt(fields, "bdefend"),
                                DVX = ParseInt(fields, "dvx"),
                                DVY = ParseInt(fields, "dvy"),
                                Fall = ParseInt(fields, "fall"),
                                H = ParseInt(fields, "h"),
                                Injury = ParseInt(fields, "injury"),
                                VRest = ParseInt(fields, "vrest"),
                                ZWidth = ParseInt(fields, "zwidth"),
                                X = ParseInt(fields, "x"),
                                Y = ParseInt(fields, "y"),
                                W = ParseInt(fields, "w")
                            };
                            break;
                        }
                        case 16:
                        {
                            frameElement = new FrostWhirlWind
                            {
                                BDefend = ParseInt(fields, "bdefend"),
                                DVX = ParseInt(fields, "dvx"),
                                DVY = ParseInt(fields, "dvy"),
                                Fall = ParseInt(fields, "fall"),
                                H = ParseInt(fields, "h"),
                                Injury = ParseInt(fields, "injury"),
                                VRest = ParseInt(fields, "vrest"),
                                ZWidth = ParseInt(fields, "zwidth"),
                                X = ParseInt(fields, "x"),
                                Y = ParseInt(fields, "y"),
                                W = ParseInt(fields, "w")
                            };
                            break;
                        }
                    }
                    break;
                }
            }
            return frameElement;
        }
    }
}


