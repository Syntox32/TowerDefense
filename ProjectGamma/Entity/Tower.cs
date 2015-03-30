using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using SFML.Window;

using ProjectGamma.Utilities;

namespace ProjectGamma.Entities
{
    public enum TowerEntityType : int
    {
        Basic = 0,
        Intermediate = 1,
        Advanced = 2,
        EndGame = 3
    }

    public class TowerEntity : Entity
    {
        public const float SellPercentage = 0.8f;

        public int CurrentTargetID;
        public bool Occupied;
       
        private float _rotation;
        private Vector2f _center;

        protected TowerEntityType Type;
        protected Transform Transform;
        protected Sprite Sprite;
        protected Level Level;

        private Vector2f Center
        {
            get { return _center; }
        }

        new public Vector2f Position
        {
            get { return base.Position; }
            set
            {
                base.Position = value;
                Sprite.Position = Position;

                InvalidateCenter();
            }
        }


        public float Rotation
        {
            get { return _rotation; }
            set
            {
                _rotation = value;
                Transform.Rotate(value, Center);
            }
        }

        public TowerEntity(int id, Level level, TowerEntityType type, Texture tex)
            : base(id)
        {
            Level = level;
            Type = type;

            Occupied = false;
            CurrentTargetID = -1;

            Sprite = new Sprite(tex);
            Sprite.Scale = Level.SpriteScalar.ToVector();

            Transform = Transform.Identity;
            
            InvalidateCenter();
        }

        public bool Seek()
        {
            return false;
        }

        public bool Attack()
        {
            return false;
        }

        protected void InvalidateCenter()
        {
            _center = new Vector2f(
                    Position.X + (Sprite.Texture.Size.X / 2),
                    Position.Y + (Sprite.Texture.Size.Y / 2)
            );
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform = Transform;
            Sprite.Draw(target, states);            
        }

        public override string ToString()
        {
            return String.Format("[Tile] Type({0}) ID({1}) ({2})", 
                (int)Type, ID, Position.ToString());
        }
    }

    public class BasicTower : TowerEntity
    {
        public BasicTower(int id, Vector2f pos, Level level, Texture tex)
            : base(id, level, TowerEntityType.Basic, tex)
        {
            Position = pos;
        }
    }
}
