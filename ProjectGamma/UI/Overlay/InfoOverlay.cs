using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Window;
using SFML.Graphics;

using ProjectGamma.Utilities;

namespace ProjectGamma.UI
{
    public class InfoOverlay : Scene
    {
        private Level _level;
        private UILabel _unitLabel;
        private UILabel _waveLabel;
        private UIPanel _infoPanel;

        public bool Invalidate { get; set; }

        public InfoOverlay(Game game, Level level)
            : base(true)
        {
            _level = level;
            var size = new Vector2f(400f, 40f);
            var posx = (Game.Size.X / 2) - (size.X / 2);
            _infoPanel = new UIPanel(game, new Vector2f(posx, 0f), size);

            _unitLabel = new UILabel("UNITS: 1000", 24, new Vector2f(posx + 20, 5));
            _waveLabel = new UILabel("WAVE: 1", 24, new Vector2f(
                (posx + size.X) - 20 - Utils.GetTextSize("WAVE: 00", UILabel.DefaultFont, 24).X, 5));
        }

        public override void Update(float delta)
        {
            if (Invalidate)
            {
                _unitLabel.SetText(String.Format("UNITS: {0}", _level.UnitBalance));
                _waveLabel.SetText(String.Format("WAVE: {0}", _level.WaveNumber));

                Invalidate = false;
            }
        }

        public override void Render(RenderTarget target, RenderStates states)
        {
            _infoPanel.Draw(target, states);
            _unitLabel.Draw(target, states);
            _waveLabel.Draw(target, states);
        }
    }
}
