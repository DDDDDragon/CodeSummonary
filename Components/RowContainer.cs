using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSummonary.Components
{
    public class RowContainer : ContainableComponent
    {
        public RowContainer() { }

        public override void Update(GameTime gameTime)
        {
            _width = 0;
            foreach (var component in Children)
                _height = Math.Max(component.Height, Height);
            foreach (var component in Children)
            {
                component.Position.X = Position.X + Width;
                if (childMiddle)
                    component.Position.Y = Position.Y + (Height - component.Height) / 2;
                else
                    component.Position.Y = Position.Y;
                _width += component.Width;
                component.Update(gameTime);
            }
            if (!_init) _init = true;
        }
    }
}
