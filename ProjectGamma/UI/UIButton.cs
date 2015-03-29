using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using SFML.Window;

using ProjectGamma.Utilities;

namespace ProjectGamma.UI
{
    public class UIButtonClickEventArgs : EventArgs
    {
        public UIButtonClickEventArgs() { }
    }

    public class UIButton : UIObject
    {
        public delegate void ButtonClickHandler(object sender, UIButtonClickEventArgs e);
        public event ButtonClickHandler OnUIButtonClick;

        public readonly string Text;
        public readonly Vector2f Size;

        private UILabel _label;
        private Game _game;
        private RectangleShape _sprite;
        private Vector2f _lastPos;
        private bool _clickEffect;

        private Color _colorIdle;
        private Color _colorClick;
        private Color _colorOutline;
        private Color _colorTextIdle;
        private Color _colorTextClick;

        public bool IsClicking { get; private set; }

        new public Vector2f Position
        {
            get { return base.Position; }
            set
            {
                base.Position = value;

                InvalidatePosition();
            }
        }

        public UIButton(Game game, string text, Vector2f position, Vector2f size, uint charSz = 32)
            : base(false)
        {
            Position = position;
            Size = size;
            Text = text;

            _colorIdle = new Color(61,61,60 /*77, 80, 84*/);
            _colorOutline = new Color(43,43,42);
            _colorClick = new Color(56, 56, 55);
            _colorTextIdle = new Color(240,240,240);
            _colorTextClick = new Color(225,225,225);

            _game = game;
            _label = new UILabel(Text, charSz, Position);
            _label.Color = _colorTextIdle;

            _sprite = new RectangleShape(size);
            _sprite.FillColor = _colorIdle;
            _sprite.OutlineThickness = 4f;
            _sprite.OutlineColor = _colorOutline;

            InvalidatePosition();
        }

        private void OnClick()
        {
            var handle = OnUIButtonClick;

            if (handle != null)
            {
                var e = new UIButtonClickEventArgs();
                OnUIButtonClick(this, e);
            }
        }

        private void InvalidatePosition()
        {
            if (_sprite != null)
            {
                _sprite.Position = Position;
            }

            if (_label != null)
            {
                var labelOffset = new Vector2f(Mathf.Floor(Size.X / 2 - _label.Size.X / 2), Mathf.Floor(Size.Y / 2 - _label.Size.Y));
                _label.Position = Position + labelOffset;
            }
        }

        public override void Update(double delta)
        {
            base.Update(delta);

            var mousePos = Mouse.GetPosition(_game);

            if (mousePos.X >= Position.X &&
                mousePos.X <= Position.X + Size.X &&
                mousePos.Y >= Position.Y &&
                mousePos.Y <= Position.Y + Size.Y)
            {
                HasFocus = true;
                
                // Clicking in progress
                if (Mouse.IsButtonPressed(Mouse.Button.Left))
                {
                    IsClicking = true;

                    if (!_clickEffect)
                    {
                        _clickEffect = true;

                        _lastPos = Position;
                        Position += new Vector2f(0,1);

                        _sprite.FillColor = _colorClick;
                        _label.Color = _colorTextClick;
                    }
                }

                // The click has been completeddd
                if (IsClicking && !Mouse.IsButtonPressed(Mouse.Button.Left))
                {
                    OnClick();

                    IsClicking = false;
                    HasFocus = false;

                    _clickEffect = false;
                    Position = _lastPos;

                    _sprite.FillColor = _colorIdle;
                    _label.Color = _colorTextIdle;
                }
            }
            else
                HasFocus = false;

            _label.Update(delta);
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            base.Draw(target, states);

            _sprite.Draw(target, states);
            _label.Draw(target, states);
        }
    }
}
