﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

using SFML.Graphics;
using SFML.Window;

using ProjectGamma.Utilities;

namespace ProjectGamma.UI
{
    public class LevelOverlay : Scene
    {
        private Level _level;
        private Game _instance;
        private RectangleShape _selection;

        private int _frameCount = 0;
        private float _dt = 0f;
        private float _fps = 0f;
        private float _updateRate = 4f;
        private float _entScalar;
        private float _tileSize;

        private UILabel _label;
        private UIButton _buttonTest;
        private UILabel _debug;
        private UIPanel _testPanel;
        private UILabel _labelTitle;

        public string DebugText;

        public LevelOverlay(Level level, float scalar, float tileSize)
            : base(true)
        {
            _level = level;
            _entScalar = scalar;
            _tileSize = tileSize;
            _instance = Game.Instance;

            _label = new UILabel("Bloodthruster er best as", 16, new Vector2f(160f, 173f));
            _debug = new UILabel("N/A", 24, new Vector2f(5f, 0f), true, false);
            _buttonTest = new UIButton(_instance, "OMG YAYY", new Vector2f(200f, 210f), new Vector2f(200f,35f), 24);
            _testPanel = new UIPanel(_instance, new Vector2f(150f,150f), new Vector2f(400f,200f));
            _labelTitle = new UILabel("UPGRADES MENU", 24, new Vector2f(160f, 150f), true);

            _buttonTest.OnUIButtonClick += buttonTest_OnUIButtonClick;
            _instance.KeyPressed += gameInstance_KeyPressed;
            _instance.MouseWheelMoved += instance_MouseWheelMoved;
            _instance.MouseButtonReleased += instance_MouseButtonReleased;
        }

        //
        // Events or something
        //

        private void instance_MouseButtonReleased(object sender, MouseButtonEventArgs e)
        {
            if (UIObject.HasFocus) return; // if any gui element has the focus, skip the next code

            if (_level.ModePlaceTower && e.Button == Mouse.Button.Left)
            {
                _level.SetTile(GetCoordsToLevel(), true);
                Console.WriteLine("something became a road");
            }
            else if (_level.ModePlaceTower && e.Button == Mouse.Button.Right)
            {
                _level.RotateTile(GetCoordsToLevel());
            }
        }

        private void instance_MouseWheelMoved(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta < 0)
                _level.Camera.Zoom(1.1f);
            else
                _level.Camera.Zoom(0.9f);
        }

        private void gameInstance_KeyPressed(object sender, KeyEventArgs e)
        {
            switch((Keyboard.Key)e.Code)
            {
                // Build mode
                case Keyboard.Key.B:
                    if (!_level.ModePlaceTower)
                    {
                        Console.WriteLine("Build mode enabled!");
                        _level.ModePlaceTower = true;
                        DebugText = "] BUILD MODE ENABLED";
                    }
                    else
                    {
                        Console.WriteLine("Build mode disabled!");
                        _level.ModePlaceTower = false;
                    }
                    break;
            }
        }

        private void buttonTest_OnUIButtonClick(object sender, UIButtonClickEventArgs e)
        {
            Console.WriteLine("omg IT WORKS :DDD");
            _level.ModePauseUpdate = true;
        }
        
        //
        // The other stuff I care about
        //

        public void SetSelectionState(Texture tex, int trans = 128)
        {
            _selection = new RectangleShape(_tileSize.ToVector());//new Sprite(tex);
            // _selection.Scale = _entScalar.ToVector();
            _selection.Texture = tex;

            var color = _selection.FillColor;
            color.A = (byte)trans;
            _selection.FillColor = color;

            _selection.OutlineColor = new Color(53,173,186, (byte)trans);
            _selection.OutlineThickness = 5f;

            // set transparency 
            /*
            var color = _selection.Color;
            color.A = (byte)trans;
            _selection.Color = color; */
        }

        private void UpdateDebugInfo(float delta)
        {
            _frameCount++;
            _dt += delta;

            if (_dt > 1f / _updateRate)
            {
                _fps = _frameCount / _dt;
                _frameCount = 0;
                _dt -= 1f / _updateRate;

                _debug.SetText("[DEBUG] FPS: {0:0}\n{1}", _fps, DebugText);
            }
        }

        public Vector2f GetCoordsToLevel()
        {
            var t = Game.Instance as RenderTarget;
            return t.MapPixelToCoords(Mouse.GetPosition(_instance), _level.Camera);
        }

        public Vector2f GetGridCoordsToLevel()
        {
            var t = Game.Instance as RenderTarget;
            var mousePos = t.MapPixelToCoords(Mouse.GetPosition(_instance), _level.Camera);
            return new Vector2f(Mathf.Floor(mousePos.X / _tileSize) * _tileSize, Mathf.Floor(mousePos.Y / _tileSize) * _tileSize);
        }

        public override void Update(float delta)
        {
            //base.Update(delta);

            UpdateDebugInfo(delta);

            _testPanel.Update(delta);
            _label.Update(delta);
            _buttonTest.Update(delta);
        }
        
        public override void Render(RenderTarget target, RenderStates states)
        {

            // anything that is going to be rendered to the tilemap goes here
            target.SetView(_level.Camera);

            if (_level.ModePlaceTower)
            {
                var gridPos = GetGridCoordsToLevel();
                _selection.Position = gridPos;
                _selection.Draw(target, states);
            }

            target.SetView(target.DefaultView);

            // everything else

            // base.Render(target, states);
            _testPanel.Draw(target, states);
            _label.Draw(target, states);
            _buttonTest.Draw(target, states);
            _debug.Draw(target, states);
            _labelTitle.Draw(target, states);
        }

    }
}
