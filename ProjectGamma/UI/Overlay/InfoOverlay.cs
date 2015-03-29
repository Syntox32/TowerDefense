using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Window;
using SFML.Graphics;

namespace ProjectGamma.UI
{
    public class InfoOverlay : Scene
    {
        private Level _level;
        private UILabel _unitLabel;
        private UILabel _roundLabel;

        public InfoOverlay(Level level)
            : base(true)
        {
            _level = level;
        }

        public override void Update(float delta)
        {
            
        }

        public override void Render(RenderTarget target, RenderStates states)
        {
            _unitLabel.Draw(target, states);
            _roundLabel.Draw(target, states);
        }
    }
}
