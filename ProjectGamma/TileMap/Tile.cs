﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Window;
using SFML.Graphics;

using ProjectGamma.Pathfinding;
using ProjectGamma.Entities;
using ProjectGamma.Utilities;

namespace ProjectGamma.Tiling
{
    public class Tile : Entity, INode
    {
        #region Node Implementation

        public INode Parent { get; set; }
        public IEnumerable<int> NeighbourIndices { get; set; }

        public bool IsRoad { get; set; }

        public float H { get; set; }
        public float G { get; set; }

        public float F { get { return H + G; } }

        #endregion

        private float _scale;
        private Sprite _sprite;
        private Transform _transform;

        public float Size
        {
            // we'll just assume the tile is a square
            get { return (float)(_sprite.Texture.Size.X * _scale); }
        }

        public Vector2f Center
        { 
            get { return new Vector2f(Position.X + (Size / 2), Position.Y + (Size / 2)); } 
        }

        new public Vector2f Position
        {
            get { return base.Position; }
            set
            {
                base.Position = value;
                _sprite.Position = Position;
            }
        }

        public Tile(Texture tex, Vector2f position, int id, float scaleFactor = 1.0f)
            : base(id)
        {
            _sprite = new Sprite(tex);
            _sprite.Position = position;
            _sprite.Scale = scaleFactor.ToVector();

            _transform = Transform.Identity;
            _scale = scaleFactor;

            Position = position;

            IsRoad = false;
            Parent = null;

            H = 0;
            G = 1;
        }

        public void SetState(Texture tex, bool road = false)
        {
            IsRoad = road ? true : false;

            _sprite = new Sprite(tex);
            _sprite.Position = Position;
            _sprite.Scale = _scale.ToVector();

            _transform = Transform.Identity;
        }

        public void Rotate(float angle = 90f)
        {
            var center = Center;
            _transform.Rotate(angle, center.X, center.Y);
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform = _transform;

            if (IsAlive)
                _sprite.Draw(target, states);
        }

        public override string ToString()
        {
            return String.Format("[Tile] ID({0}) ({1})", ID, Position.ToString());
        }
    }
}
