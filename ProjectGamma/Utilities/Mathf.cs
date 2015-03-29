using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGamma.Utilities
{
    public sealed class Mathf
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Sin(float value)
        {
            return (float)Math.Sin((double)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Cos(float value)
        {
            return (float)Math.Cos((double)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Atan2(float y, float x)
        {
            return (float)Math.Atan2((double)y, (double)x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Ceiling(float value)
        {
            return (float)Math.Ceiling((double)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Floor(float value)
        {
            return (float)Math.Floor((double)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Sqrt(float value)
        {
            return (float)Math.Sqrt((double)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Pow(float value, float pow)
        {
            return (float)Math.Pow((double)value, (double)pow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Abs(float value)
        {
            return Math.Abs(value);
        }
    }
}
