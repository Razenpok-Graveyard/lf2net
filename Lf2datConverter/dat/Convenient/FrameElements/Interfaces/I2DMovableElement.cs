using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Lf2datConverter.dat.Convenient
{
    interface I2DMovableElement
    {
        Point VelocityDelta { get; set; }
    }
}
