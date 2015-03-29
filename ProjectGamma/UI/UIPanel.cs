using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Window;
using SFML.Graphics;

namespace ProjectGamma.UI
{
    public class UIPanel : UIObject
    {
        public static float OutlineThickness = 5f;
        
        public readonly Vector2f Size;

        private Game _game;
        private RectangleShape _sprite;

        private Color _colorIn;
        private Color _colorOut;

        new public Vector2f Position
        {
            get { return base.Position; }
            set
            {
                base.Position = value;

                InvalidatePosition();
            }
        }

        public UIPanel(Game game, Vector2f pos, Vector2f size)
            : base(true)
        {
            _game = game;
            Size = size;
            Position = pos;

            _colorIn = new Color(70,70,70);
            _colorOut = new Color(50,50,50/*65, 65, 80*/);

            _sprite = new RectangleShape(size);
            _sprite.Position = pos;
            _sprite.FillColor = _colorIn;
            _sprite.OutlineThickness = OutlineThickness;
            _sprite.OutlineColor = _colorOut;
        }

        private void InvalidatePosition()
        {
            if (_sprite != null)
            {
                _sprite.Position = Position;
            }
        }

        public override void Update(double delta)
        {
            base.Update(delta);

            var mousePos = Mouse.GetPosition(_game);

            // TODO: this does not work lol
            if (mousePos.X >= Position.X &&
                mousePos.X <= Position.X + Size.X &&
                mousePos.Y >= Position.Y &&
                mousePos.Y <= Position.Y + Size.Y)
            {
                HasFocus = true;
            }
            else
                HasFocus = false;

            // TODO: Relative position for child elements
            // TODO: Write dragging logic
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            _sprite.Draw(target, states);

            base.Draw(target, states);
        }
    }
}
