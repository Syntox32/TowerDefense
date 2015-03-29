using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using SFML.Window;

namespace ProjectGamma.UI
{
    public class UIObject: IDrawable
    {
        public bool IsDraggable { get; set; }
        public bool IsClickable { get; set; }
        public bool IsEnabled { get; set; }

        public UIObject Parent { get; set; }
        public List<UIObject> Children;

        public Vector2f Position { get; set; }

        // If a gui element has focus, lock other mouse and keyboard events
        public static bool HasFocus { get; protected set; }

        public UIObject()
            : this(false)
        { }

        public UIObject(bool draggable)
        {
            IsDraggable = draggable;
            IsClickable = true;
            IsEnabled = true;

            Parent = null;
            Children = new List<UIObject>();
        }

        public void AddChild(UIObject obj)
        {
            if (!Children.Contains((UIObject)obj))
            {
                obj.Parent = this;
                Children.Add(obj);
            }
        }

        public void RemoveChild(UIObject obj)
        {
            if (Children.Contains((UIObject)obj))
            {
                obj.Parent = null;
                Children.Remove(obj);
            }
        }

        public virtual void Update(double delta)
        {
            foreach(var child in Children)
            {
                child.Update(delta);
            }
        }

        public virtual void Draw(RenderTarget target, RenderStates states)
        {
            foreach(var child in Children)
            {
                child.Draw(target, states);
            }
        }
    }
}
