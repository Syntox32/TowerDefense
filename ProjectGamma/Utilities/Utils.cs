using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using SFML.Window;

using System.Runtime.CompilerServices;

namespace ProjectGamma.Utilities
{
    public static class Utils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Normalize(this float val, float min, float max)
        {
            return (val - min) / (max - min);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Clamp(this float val, float min, float max)
        {
            return val > max ? max : val < min ? min : val;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float ToAnglef(this double radian)
        {
            return (float)(radian * (180 / Math.PI));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ToAngle(this double radian)
        {
            return (double)(radian * (180 / Math.PI));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ToRadian(this float angle)
        {
            return (double)(angle * (Math.PI / 180));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float ToRadianf(this float angle)
        {
            return (float)(angle * (Math.PI / 180));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Texture GetTexture(Image image, int top, int left, int w, int h)
        {
            return new Texture(image, new IntRect() { Top = top, Left = left, Width = w, Height = h });
        }

        public static float WrapAngle360(this float angle)
        {
            var ang = angle % 360.0f;
            if (ang < 0)
                ang += 360.0f;

            return ang;
        }

        public static Color ColorFromHex(string hexRgb)
        {
            var hex = hexRgb.StartsWith("#") ? hexRgb.Replace("#", "FF") : "FF" + hexRgb;
            uint argb = UInt32.Parse(hex, NumberStyles.HexNumber);

            byte a = (byte)((argb >> 24) & 0xFF);
            byte r = (byte)((argb >> 16) & 0xFF);
            byte g = (byte)((argb >> 8) & 0xFF);
            byte b = (byte)((argb) & 0xFF);

            return new Color(r, g, b, a);
        }

        public static Vector2f GetTextSize(string text, Font font, uint sz)
        {
            var t = new Text(text, font, sz);

            return new Vector2f(t.GetLocalBounds().Width, t.GetLocalBounds().Height);
        }

        public static void DrawVertices(Vertex[] vertices, PrimitiveType type, RenderTarget rt)
        {
            rt.Draw(vertices, type, RenderStates.Default);
        }

        public static void DrawPoint(Vector2f vec, RenderTarget rt, Color? color = null)
        {
            var circle = new CircleShape(3);
            circle.Position = vec - new Vector2f(3f, 3f);
            circle.FillColor = color.HasValue ? color.Value : Color.Magenta;

            rt.Draw(circle, RenderStates.Default);
        }

        public static void DrawLine(Vector2f pos, Vector2f normal, float length, RenderTarget rt, Color? color = null)
        {
            var col = color.HasValue ? color.Value : Color.Magenta;

            var startVert = new Vertex(pos, col);
            var endVert = new Vertex(pos + (normal * length), col);

            rt.Draw(new Vertex[] { startVert, endVert }, PrimitiveType.Lines, RenderStates.Default);
        }

        public static void DrawLine(Vector2f startPos, Vector2f endPos, RenderTarget rt, Color? color = null)
        {
            var col = color.HasValue ? color.Value : Color.Magenta;

            var startVert = new Vertex(startPos, col);
            var endVert = new Vertex(endPos, col);

            rt.Draw(new Vertex[] { startVert, endVert }, PrimitiveType.Lines, RenderStates.Default);
        }
    }
}
