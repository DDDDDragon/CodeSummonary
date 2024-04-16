using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSummonary.Components
{
    public class ColumnContainer : ContainableComponent
    {
        public ColumnContainer() { }

        public override void Update(GameTime gameTime)
        {
            _height = 0;
            var Update = new List<Component>(Children);

            foreach (var component in Update)
                _width = Math.Max(component.Width, Width);
            foreach (var component in Update)
            {
                if(component.shouldRecover) Children.Remove(component);
                component.Update(gameTime);
                if (childMiddle)
                    component.Position.X = Position.X + (Width - component.Width) / 2;
                else
                    component.Position.X = Position.X;
                component.Position.Y = Position.Y + Height;
                _height += component.Height;
            }
            if (!_init) _init = true;
        }
    }
}
