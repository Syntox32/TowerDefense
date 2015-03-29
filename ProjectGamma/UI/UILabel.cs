using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProjectGamma.Utilities;

namespace ProjectGamma.UI
{
    public class UILabel : UIObject
    {
        public const string FontPath = @"D:\Programming\Cs\ProjectGamma\Resources\Fonts\";
        public static Font DefaultFont = new Font(FontPath + "04B_03__.TTF"); // SourceSansPro-Regular.otf sfpixelate-bold.ttf editundo.ttf slkscr.ttf

        public readonly Font Font;
        public readonly Text Text;
        
        private const float shadowOffset = 3.0f;
        private readonly Text _shadow;
        private bool _useShadow;

        new public Vector2f Position
        {
            get { return base.Position; }
            set
            {
                base.Position = value;
                Text.Position = base.Position;
                _shadow.Position = base.Position + new Vector2f(shadowOffset, shadowOffset);
            }
        }

        public Color Color
        {
            get { return Text.Color; }
            set { Text.Color = value; }
        }

        public Vector2f Center
        {
            get { return new Vector2f(Text.GetGlobalBounds().Width / 2, Text.GetGlobalBounds().Height / 2); }
        }

        public Vector2f Size
        {
            get { return new Vector2f(Mathf.Floor(Text.GetLocalBounds().Width), Mathf.Floor(Text.GetLocalBounds().Height)); }
        }

        public UILabel(string str, uint chrSize, Vector2f pos, bool bold = false, bool shadow = true, Font font = null, Color? fontColor = null)
            : base(false)
        {
            Font = font != null ? font : DefaultFont;
            
            // may I recommend 16, 24 and 32 as chrSize?
            Text = new Text(str, Font, chrSize);
            Text.Position = pos;
            Text.Style = bold ? Text.Styles.Bold : Text.Styles.Regular;
            Text.Color = fontColor.HasValue ? fontColor.Value : Color.White;

            // Shadow
            _shadow = new Text(str, Font, chrSize);
            _shadow.Color = new Color(40,40,40);
            _shadow.Position = pos + new Vector2f(shadowOffset, shadowOffset);
            _shadow.Style = bold ? Text.Styles.Bold : Text.Styles.Regular;

            _useShadow = shadow;
            Position = pos;
        }

        public void SetText(string str)
        {
            Text.DisplayedString = str;
            _shadow.DisplayedString = Text.DisplayedString;
        }

        public void SetText(string str, params object[] args)
        {
            Text.DisplayedString = String.Format(str, args);
            _shadow.DisplayedString = Text.DisplayedString;
        }

        public override void Update(double delta)
        {
            if (IsEnabled)
            {
                //base.Update(delta);
            }
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            if (IsEnabled)
            {
                //base.Draw(target, states);
                
                if (_useShadow)
                    _shadow.Draw(target, states);
                
                Text.Draw(target, states);
            }
        }
    }
}