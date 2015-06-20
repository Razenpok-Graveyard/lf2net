using System.Collections.Generic;

namespace Lf2datConverter
{
    class CharacterFrame
    {
        public int FrameNumber;
        public string Name;
        public int Pic;
        public int State;
        public int Wait;
        public int Next;
        public int DVX;
        public int DVY;
        public int DVZ;
        public int CenterX;
        public int CenterY;
        public int HitA;
        public int HitD;
        public int HitJ;
        public int HitFA;
        public int HitFJ;
        public int HitUA;
        public int HitUJ;
        public int HitDA;
        public int HitDJ;
        public int HitJA;
        public int MP;
        public string Sound;
        public List<FrameElement> FrameElements = new List<FrameElement>();
    }
}
