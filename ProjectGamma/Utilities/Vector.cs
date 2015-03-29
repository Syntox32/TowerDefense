using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGamma.Utilities
{
    public static class Vector
    {
        public static Vector2f Normalize(this Vector2f vec)
        {
            var len = (float)Math.Sqrt((double)(vec.X * vec.X + vec.Y * vec.Y));
            return new Vector2f(vec.X / len, vec.Y / len);
        }

        public static Vector2f Rotate(this Vector2f vec, Vector2f other, float angle)
        {
            var rad = angle.ToRadian();

            var dx = vec.X - other.X;
            var dy = vec.Y - other.Y;

            var xPrime = other.X + ((dx * Math.Cos(rad)) - (dy * Math.Sin(rad)));
            var yPrime = other.Y + ((dx * Math.Sin(rad)) + (dy * Math.Cos(rad)));

            return new Vector2f((float)xPrime, (float)yPrime);
        }

        public static Vector2f Project(this Vector2f axis, Vector2f vec)
        {
            // www.gamedev.net/page/resources/_/technical/game-programming/2d-rotated-rectangle-collision-r2604

            float proj = (float)((vec.X * axis.X + vec.Y * axis.Y)
                / (Math.Pow(axis.X, 2) + Math.Pow(axis.Y, 2)));

            float x = proj * axis.X;
            float y = proj * axis.Y;

            return new Vector2f(x, y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2f Mult(Vector2f v1, Vector2f v2)
        {
            return new Vector2f(v1.X * v2.X, v1.Y * v2.Y);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2f Bisect(this Vector2f vec, Vector2f other)
        {
            return new Vector2f((vec.X + other.X) / 2.0f, (vec.Y + other.Y) / 2.0f);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Magnitude(this Vector2f vec)
        {
            return (float)Math.Sqrt((double)(vec.X * vec.X + vec.Y * vec.Y));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Dot(this Vector2f vec, Vector2f other)
        {
            return (vec.X * other.X) + (vec.Y * other.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Distance(this Vector2f vec, Vector2f other)
        {
            return Mathf.Sqrt(Mathf.Pow(Mathf.Abs(vec.X - other.X), 2) + Mathf.Pow(Mathf.Abs(vec.Y - other.Y), 2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2f ToVector(this float val)
        {
            return new Vector2f(val, val);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2f ToFloatVector(this Vector2i vec)
        {
            return new Vector2f((float)vec.X, (float)vec.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2f ToFloatVector(this Vector2u vec)
        {
            return new Vector2f((float)vec.X, (float)vec.Y);
        }
    }
}
