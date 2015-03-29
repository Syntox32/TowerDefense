using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SFML.Window;
using SFML.Graphics;
using SFML.Audio;
using System.Diagnostics;

namespace ProjectGamma
{
    public class Game : RenderWindow
    {
        private float _frameTime;
        private string _title;
        private Styles _styles;
        private View _camera;
        private Stopwatch _frameTimer;
        private bool _hasScene;
        private Level _level;

        private static Game _instance;
        private static Scene _currentScene;
        private static float _time;
        private static ContextSettings _ctxSettings;
        private static RenderStates _renderStates;
        private static VideoMode _videoMode;

        public static Game Instance
        {
            get { return _instance; }
        }

        public static float Time
        {
            get { return _time; }
        }

        new public static Vector2f Size // overrides the vector2u
        {
            get { return new Vector2f(_videoMode.Width, _videoMode.Height); }
        }

        public Game(VideoMode vMode, string title, Styles styles, ContextSettings ctxSettings) 
            : base(vMode, title, styles, ctxSettings)
        {
            _videoMode = vMode;
            _title = title;
            _styles = styles;
            _ctxSettings = ctxSettings;
            _hasScene = false;

            // base.SetFramerateLimit(60);
        }

        public void SetScene(Scene scene)
        {
            _currentScene = scene;
            _hasScene = true;
        }

        public void Init()
        {
            Console.WriteLine("Initializing game...");
            _frameTimer = new Stopwatch();
            _renderStates = RenderStates.Default;
            _camera = base.DefaultView;
            _instance = this;

            // _menu = new Menu(this);
            // SetScene(_menu);

            _level = new Level(this);
            SetScene(_level);

            this.Closed += Exit;

            Run();
        }

        private void Run()
        {
            _frameTimer.Start();

            while(base.IsOpen())
            {
                base.DispatchEvents();

                _frameTime = (float)_frameTimer.Elapsed.TotalSeconds;
                _frameTimer.Restart();
                _time += _frameTime;

                base.Clear();

                if (_hasScene)
                {
                    Update(_frameTime);
                    Render((RenderTarget)this, _renderStates);
                }

                base.Display();
            }
        }

        private void Update(float delta)
        {
            _currentScene.Update(delta);
        }

        private void Render(RenderTarget target, RenderStates states)
        {
            _currentScene.Render(target, states);
        }

        private void Exit(object sender, EventArgs e)
        {
            Console.WriteLine("Closing game...");
            var t = sender as Window;
            t.Close();
        }
    }
}