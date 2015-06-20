﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Lf2datConverter
{
    public struct Block
    {
        public string Name;
        public List<string> Lines;

        public Block(string name)
        {
            Name = name;
            Lines = new List<string>();
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
            var result = new List<Block>();
            var currentBlock = new Block();
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
                    var firstLine = line.Substring(8);
                    currentBlock.Lines.Add(firstLine);
                    continue;
                }
                if (line.Contains("<bmp_end>") || line.Contains("<frame_end>"))
                {
                    result.Add(currentBlock);
                    currentBlock = new Block();
                    continue;
                }
                currentBlock.Lines.Add(line);
            }
            return result;
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
            var result = new Character();
            if (block.Name != "bmp")
                return null;
            var splittedBlock = block.Lines
                .Select(line => line.Split(new []{" ", ":"}, StringSplitOptions.RemoveEmptyEntries));
            foreach (var parts in splittedBlock)
            {
                // dirty hack to parse file statements
                if (parts[0].Contains("file"))
                {
                    result.SpriteFiles.Add(ParseSpriteFile(parts));
                    continue;
                }
                var fieldData = parts[1];
                switch (parts[0])
                {
                    case "name":
                    {
                        result.Name = fieldData;
                        break;
                    }
                        case "head":
                    {
                        result.Head = fieldData;
                        break;
                    }
                        case "small":
                    {
                        result.Small = fieldData;
                        break;
                    }
                       /* case "name":
                    {
                        result.Name = fieldData;
                        break;
                    }
                        case "name":
                    {
                        result.Name = fieldData;
                        break;
                    }
                        case "name":
                    {
                        result.Name = fieldData;
                        break;
                    }
                        case "name":
                    {
                        result.Name = fieldData;
                        break;
                    }*/

                }
            }
            return result;
        }
        /*
        name: Davis
head: sprite\sys\davis_f.bmp
small: sprite\sys\davis_s.bmp
walking_frame_rate 3
walking_speed 5.000000
walking_speedz 2.500000
running_frame_rate 3
running_speed 10.000000
running_speedz 1.600000
heavy_walking_speed 3.700000
heavy_walking_speedz 1.850000
heavy_running_speed 6.200000
heavy_running_speedz 1.000000
jump_height -16.299999
jump_distance 10.000000
jump_distancez 3.750000
dash_height -10.000000
dash_distance 18.000000
dash_distancez 5.000000
rowing_height -2.000000
rowing_distance 5.000000*/

        private static SpriteFile ParseSpriteFile(IReadOnlyList<string> parts)
        {
            var file = new SpriteFile();
            var id = parts[0].Substring(4).Trim('(', ')').Split('-');
            file.StartID = int.Parse(id[0]);
            file.FinishID = int.Parse(id[1]);
            file.Path = parts[1];
            file.Width = int.Parse(parts[3]);
            file.Height = int.Parse(parts[5]);
            file.Rows = int.Parse(parts[7]);
            file.Columns = int.Parse(parts[9]);
            return file;
        }

        private static CharacterFrame ParseFrame(Block block)
        {
            var result = new CharacterFrame();
            return result;
        }
    }
}