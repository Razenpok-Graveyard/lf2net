using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lf2datConverter
{
    static class Converter
    {
        public static Character ConvertDat(string dat)
        {
            var character = new Character();
            var lines = dat.Split(new[] {"\n"}, StringSplitOptions.RemoveEmptyEntries);
            return character;
        }
    }
}
