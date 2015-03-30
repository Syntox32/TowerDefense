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
    public class EnemyEntity : Entity
    {
        public readonly Vector2f InitPos;
        public readonly float SpriteScalar;

        public float Speed;

        public Vector2f Velocity;
        public Vector2f Direction;
        public Vector2f TargetPosition;

        private Sprite _sprite;

        new public Vector2f Position
        {
            get { return base.Position; }
            set
            {
                base.Position = value;
                _sprite.Position = Position;
            }
        }

        public EnemyEntity(int id, Vector2f initPos, Texture tex, float scalar = 1.0f)
            : base(id)
        {
            Speed = 100f;

            SpriteScalar = scalar;
            InitPos = initPos; // position to be reset to

            _sprite = new Sprite(tex);
            _sprite.Position = initPos;
            _sprite.Scale = scalar.ToVector();
        }

        public void Reset()
        {
            Velocity = new Vector2f();
            Direction = new Vector2f();

            IsAlive = true;
            Position = InitPos;
        }

        public override void Update(double delta)
        {
            if (!IsAlive) return;

            Velocity = Direction * Speed;
            Position += Velocity * (float)delta;
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            if (!IsAlive) return; // is kill

            _sprite.Draw(target, states);
        }
    }
}
