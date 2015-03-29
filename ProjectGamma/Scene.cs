using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;

namespace ProjectGamma
{
    public class Scene
    {
        public bool UserInterface { get; set; }

        public Scene()
            : this(false)
        { }

        public Scene(bool userInterface)
        {
            UserInterface = userInterface;
        }

        public virtual void Render(RenderTarget target, RenderStates states) { }
        public virtual void Update(float delta) { }
    }
}
