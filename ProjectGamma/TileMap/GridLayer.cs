using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Window;
using ProjectGamma.Entities;
using SFML.Graphics;

namespace ProjectGamma.Tiling
{
    public class GridLayer<T> : IEnumerable<T>
        where T : Entity
    {
        public int[][] Directions = new int[8][] { 
            // perp
            new int[] {  0, -1 }, // top
            new int[] {  0,  1 }, // bot
            new int[] { -1,  0 }, // left
            new int[] {  1,  0 }, // right
            // diag
            new int[] {  1,  1 }, // bot right
            new int[] {  1, -1 }, // top right
            new int[] { -1,  1 }, // bot left
            new int[] { -1, -1 }  // top left
        };

        private int _entX;
        private int _entY;
        private float _size;
        private bool _checkNullEnt;

        private T[] _entities;

        public T this[int i]
        {
            get 
            {
                if (i >= _entities.Length)
                    throw new ArgumentOutOfRangeException("Indices can't be out of bounds");

                return _entities[i];
            }
            set { _entities[i] = value; }
        }

        public T this[int x, int y]
        {
            get 
            {
                if (x > _entX || y > _entY)
                    throw new ArgumentOutOfRangeException("Indices can't be out of bounds");

                return _entities[x + y * _entX]; 
            }
            set { _entities[x + y * _entX] = value; }
        }

        public int Length
        {
            get { return _entities.Length; }
        }

        public int EntitiesX 
        { 
            get { return _entX; }
        }

        public int EntitiesY 
        { 
            get { return _entY; }
        }

        public GridLayer(int entX, int entY, float entSize, bool checkForNullEntities = true)
        {
            _entX = entX;
            _entY = entY;
            _size = entSize;
            _checkNullEnt = checkForNullEntities;

            _entities = new T[entX * entY];
        }

        public int GetIndex(int x, int y)
        {
            return x + y * _entX;
        }

        public Vector2i GetPosition(int index)
        {
            int y = (int)Math.Floor((double)(index / _entX));
            int x = index % _entX;

            return new Vector2i(x, y);
        }

        public int GetTileIndexAtPosition(Vector2i position)
        {
            for (int i = 0; i < Length; i++)
            {
                var tile = _entities[i];
                var tPos = tile.Position;

                if (position.X >= tPos.X &&
                    position.X <= tPos.X + _size &&
                    position.Y >= tPos.Y &&
                    position.Y <= tPos.Y + _size)
                {
                    return i;
                }
            }

            return -1; // No tile was found
        }

        public int[] GetNearbyEntities(T ent, bool diagonal = false)
        {
            var temp = new List<int>();
            var len = diagonal ? 8 : 4; // look at the top of the file

            for(int i = 0; i < len; i++)
            {
                var dir = Directions[i];

                // TODO: Is this next step redundant?
                var posx = (int)Math.Floor((double)(ent.ID / _entX));
                var posy = ent.ID % _entX;

                var newx = posx + dir[0]; // x
                var newy = posy + dir[1]; // y

                int idx = GetIndex(newx, newy);

                // make sure the index does not go outside the bounderies of the map
                // and make sure it does not wrap around
                if (idx > (newy * _entX) - 1 && idx < (newy + 1) * _entX
                    && idx >= 0 && idx < _entX * _entY)
                {
                    temp.Add(idx);
                }
            }

            // return the ID of the nearby entities
            return temp.ToArray();
        }

        public virtual void Draw(RenderTarget target, RenderStates states)
        {
            for (int i = 0; i < _entities.Length; i++)
            {
                if (_checkNullEnt)
                {
                    if (_entities[i] != null)
                        _entities[i].Draw(target, states);
                }
                else
                    _entities[i].Draw(target, states);
            }
        }

        public virtual void Update(float delta)
        {
            for (int i = 0; i < _entities.Length; i++)
            {
                if (_checkNullEnt)
                {
                    if (_entities[i] != null)
                        _entities[i].Update(delta);
                }
                else
                    _entities[i].Update(delta);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _entities.ToList<T>().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _entities.ToList<T>().GetEnumerator();
        }
    }
}
