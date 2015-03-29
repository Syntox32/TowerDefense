using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using SFML.Window;

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
        public static readonly float SellPercentage = 0.8f;

        private Level _level;
        private Texture _texture;
        private TowerEntityType _type;

        public TowerEntity(Level level, TowerEntityType type, Texture tex)
            : base(-1) // shold assign an id when spawning
        {
            _level = level;
            _texture = tex;
            _type = type;
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            base.Draw(target, states);
        }

        public override void Update(double delta)
        {
            base.Update(delta);
        }
    }
}
