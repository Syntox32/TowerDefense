using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using ProjectGamma.Entities;

namespace ProjectGamma
{
    public class EntityPool<T> : IEnumerable
        where T : Entity
    {
        public readonly T[] Entities;

        public int MaxEntities { get; private set; }

        public int Count 
        {
            get
            {
                int count = 0;

                for (int i = 0; i < Entities.Length; i++)
                {
                    if (Entities[i] != null) count++;
                }

                return count;
            }
        }

        public int Alive 
        { 
            get { return Entities.Count(x => x.IsAlive); }
        }

        public T this[int index]
        {
            get
            {
                if (index >= Entities.Length || index < 0)  throw new ArgumentOutOfRangeException();
                    
                return Entities[index];
            }
        }

        public EntityPool(int maxEntities)
        {
            MaxEntities = maxEntities;
            Entities = new T[maxEntities];

            for (int i = 0; i < maxEntities; i++)
            {
                Entities[i] = null; // lol
            }
        }

        public void PushEntity(T ent)
        {
            if (Count + 1 >= MaxEntities) throw new ArgumentException("Reached entity limit");
            
            for(int i = 0; i < Entities.Length; i++)
            {
                if (Entities[i] == null) Entities[i] = (T)ent;
            }
        }

        public void RemoveEntity(T ent)
        {
            for(int i = 0; i < Entities.Length; i++)
            {
                if (Entities[i].ID == ent.ID) Entities[i] = null;
            }

            throw new ArgumentException("No entity with id in current pool");
        }

        public void Update(double dt)
        {
            for (int i = 0; i < Entities.Length; i++)
            {
                var ent = Entities[i];

                if (ent != null && ent.IsAlive) ent.Update(dt);
            }
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            for (int i = 0; i < Entities.Length; i++)
            {
                var ent = Entities[i];

                if (ent != null && ent.IsAlive) ent.Draw(target, states);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Entities.Length; i++)
            {
                if (Entities[i] != null) yield return Entities[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (int i = 0; i < Entities.Length; i++)
            {
                if (Entities[i] != null) yield return Entities[i];
            }
        }
    }
}
