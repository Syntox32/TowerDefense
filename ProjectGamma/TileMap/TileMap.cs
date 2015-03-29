using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Window;
using SFML.Graphics;

using ProjectGamma.Utilities;

namespace ProjectGamma.Tiling
{
    public sealed class TileMap : IDrawable, IEnumerable<Tile>
    {
        private Level _lvlInstance;
        private Texture _texture;
        private GridLayer<Tile> _tiles;

        private float _tileSize;
        private bool _invalidate;

        public Tile StartTile { get; set; }
        public Tile EndTile { get; set; }

        public float TileSize
        {
            get { return _tileSize; }
        }

        public int TilesX
        {
            get { return _tiles.EntitiesX; }
        }

        public int TilesY
        {
            get { return _tiles.EntitiesY; }
        }

        public Tile this[int i]
        {
            get { return _tiles[i]; }
            set
            {
                _tiles[i] = value;
                //_invalidate = true;
            }
        }

        public Tile this[int x, int y]
        {
            get { return _tiles[x + y * TilesX]; }
            set
            {
                _tiles[x + y * TilesX] = value;
                //_invalidate = true;
            }
        }

        public Vector2f Size
        {
            get { return new Vector2f(TileSize * TilesX, TileSize * TilesY); }
        }

        public int Count
        {
            get { return _tiles.Length; }
        }


        public TileMap(Level level, Texture tileTex, int tilesX, int tilesY, float tileSize = 16)
        {
            _lvlInstance = level;
            _texture = tileTex;
            _tiles = new GridLayer<Tile>(tilesX, tilesY, tileSize);

            _tileSize = tileSize;
            Console.WriteLine("TileSize: " + _tileSize);

            StartTile = null;
            EndTile = null;

            Invalidate();
        }

        public Vector2f GetGridCoords(Vector2f pos)
        {
            return new Vector2f(Mathf.Floor(pos.X / _tileSize), Mathf.Floor(pos.Y / _tileSize));
        }

        // TODO: Saving and loadinggg
        public void Save() { throw new NotImplementedException(); }
        public void Load() { throw new NotImplementedException(); }

        private void Invalidate()
        {
            _tileSize = _texture.Size.X * Level.SpriteScalar;

            if (_tiles == null) // Init array
                _tiles = new GridLayer<Tile>(TilesX, TilesY, _tileSize);

            for (int y = 0; y < TilesY; y++)
            {
                for (int x = 0; x < TilesX; x++)
                {
                    var newPos = new Vector2f(_tileSize * x, _tileSize * y);
                    int idx = _tiles.GetIndex(x, y);
                    var tile = new Tile(_texture, newPos, idx, Level.SpriteScalar);
                    _tiles[idx] = tile;
                }
            }

            _invalidate = false;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            if (_invalidate)
                Invalidate();

            _tiles.Draw(target, states);
        }

        public IEnumerator<Tile> GetEnumerator()
        {
            return _tiles.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _tiles.GetEnumerator();
        }
    }
}
