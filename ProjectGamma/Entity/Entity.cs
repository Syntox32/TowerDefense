using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Window;
using SFML.Graphics;

namespace ProjectGamma.Entities
{
    public class Entity
    {
        public readonly int ID;

        private float _x;
        private float _y;

        public bool IsAlive { get; set; }

        public float X 
        {
            get { return _x; }
            set { _x = value; }
        }

        public float Y
        {
            get { return _y; }
            set { _y = value; }
        }

        public virtual Vector2f Position
        {
            get { return new Vector2f(_x, _y); }
            set
            {
                _x = value.X;
                _y = value.Y;
            }
        }

        public Entity(int id)
        {
            IsAlive = true;
            ID = id;
        }

        public virtual void Update(double delta) { }
        public virtual void Draw(RenderTarget target, RenderStates states) { Console.WriteLine("whta the fuck"); }
    }
}