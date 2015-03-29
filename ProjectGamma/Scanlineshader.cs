using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGamma
{
    public class ScanlineShader : Shader, IDisposable
    {
        private double _time;
        private Texture _texture;
        private Vector2u _resolution;

        public double Time
        {
            get { return _time; }
            set
            {
                _time = value;
                //base.SetParameter("in_time", (float)value);
            }
        }

        public Texture Texture
        {
            get { return _texture; }
            set
            {
                _texture = value;
                base.SetParameter("in_tex", value);
            }
        }


        public Vector2u Resolution
        {
            get { return _resolution; }
            set
            {
                _resolution = value;
                base.SetParameter("in_resolution", (float)value.X, (float)value.Y);
            }
        }


        public ScanlineShader(Vector2u resolution)
            : base(null, @"D:\Programming\Cs\ProjectGamma\Resources\Shaders\scanlines.frag")
        {
            Resolution = resolution;
        }

        public void Enable(ref RenderStates states)
        {
            states.Shader = this;
        }

        public void Disable(ref RenderStates states)
        {
            states = RenderStates.Default;
        }
    }
}
