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
        private int _id;

        private float _x;
        private float _y;

        public int ID 
        { 
            get { return _id; }
            set
            {
                if (InUse) // never change the ID while in use
                    throw new Exception("Can't change ID of entity while in use");
            
                _id = value;
                InUse = true; // set to in use when the id is set
            }
        }

        public bool IsAlive { get; set; }
        public bool InUse { get; set; }

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

        public Entity()
            : this(-1)
        { }

        public Entity(int id)
        {
            IsAlive = true;

            if (id < 0) // if initalized with a negative id, it should not be in use by default
                _id = id;
            else
                ID = id;
        }

        public virtual void Update(double delta) { }
        public virtual void Draw(RenderTarget target, RenderStates states) { Console.WriteLine("whta the fuck"); }
    }
}