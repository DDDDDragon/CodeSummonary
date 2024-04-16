using CodeSummonary.Components.Summaries;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSummonary.Components
{
    public abstract class ContainableComponent : Component
    {
        public List<Component> Children = new List<Component>();

        internal int _width;

        internal int _height;

        public override int Height => _height;

        public override int Width => _width;

        public void RegisterChild(Component component)
        {
            component.Parent = this;
            Children.Add(component);
        }

        public bool childMiddle = true;

        public override void Update(GameTime gameTime)
        {
            foreach (var child in Children)
            {
                child.Position = child.RelativePosition + Position;
                child.Update(gameTime);
            }
            for(int i = 0; i < Children.Count; i++)
            {
                if (Children[i].shouldRecover)
                {
                    Children.RemoveAt(i);
                    i--;
                }
            }
            base.Update(gameTime);
            if (!_init) _init = true;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (!_init) return;
            foreach (var component in Children)
                component.Draw(spriteBatch, gameTime);
        }

        public void RegisterChildAt(int index, Component component, bool behind = false)
        {
            if (behind)
                Children.Insert(Children.Count - index, component);
            else 
                Children.Insert(index, component);
        }

        public bool ContainsChild(Predicate<Component> match)
        {
            return Children.Exists(match);
        }

        public Component SelectChild(Func<Component, bool> match)
        {
            return Children.First(match);
        }
    }
}
