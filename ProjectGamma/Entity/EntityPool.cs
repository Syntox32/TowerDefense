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
    public class EntityPool : IEnumerable
    {
        public readonly Entity[] Entities;

        public int MaxEntities { get; private set; }
        public int Count  { get { return Entities.Length; } }
        public int Alive { get { return Entities.Count(x => x.IsAlive); } }

        public Entity this[int index]
        {
            get
            {
                if (index >= Entities.Length || index < 0) throw new ArgumentOutOfRangeException();

                return Entities[index];
            }
            set 
            {
                // we want all entities initalized
                if (value == null) throw new ArgumentNullException();

                Entities[index] = value;
            }
        }

        public EntityPool(int initalEntities)
        {
            MaxEntities = initalEntities;
            Entities = new Entity[initalEntities];

            for (int i = 0; i < initalEntities; i++) 
                Entities[i] = new Entity(i) { IsAlive = false }; // initalize all entities to be dead
        }

        public void PushEntity(Entity ent)
        {
            for(int i = 0; i < Entities.Length; i++)
            {
                // just for debugging
                if (Entities[i] == null)
                    Console.WriteLine("[WARN] Entity({0}) should not be null", i);

                // find the first dead entity to replace
                if (!Entities[i].IsAlive && Entities[i] != null)
                {
                    ent.ID = i;
                    ent.IsAlive = true;
                    ent.InUse = true;

                    Entities[i] = ent;

                    return;
                }
            }

            throw new ArgumentException("Reached entity limit");
        }

        public void RemoveEntity(Entity ent)
        {
            // as long as you don't change the entity id while it is in the pool
            // you should be fine
            Entity e = Entities[ent.ID];

            if (e != null) Entities[ent.ID].IsAlive = false;
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

                if (ent != null && ent.IsAlive)
                    ent.Draw(target, states);
            }
        }

        public IEnumerator<Entity> GetEnumerator()
        {
            for(int i = 0; i < Entities.Length; i++) yield return Entities[i];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (int i = 0; i < Entities.Length; i++) yield return Entities[i];
        }
    }
}
