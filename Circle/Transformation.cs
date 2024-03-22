using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circle
{
    public static partial class Transformation
    {
        public static float ToFloat(this string str) => float.Parse(str);
        public static int ToInteger(this string str) => int.Parse(str);

    }
}
