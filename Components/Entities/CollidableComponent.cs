using CodeSummonary.Components.Summaries;
using CodeSummonary.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSummonary.Components.Entities
{
    public class CollidableComponent : Component
    {
        public int _width;

        public int _height;

        public override int Height => _height;

        public override int Width => _width;

        public override void Update(GameTime gameTime)
        {
            foreach (var item in Parent.Children) 
            {
                if (item is Entity<Summary> entity && item.Rectangle.Intersects(Rectangle))
                    entity.Collide = true;
            }
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var rec = Rectangle.StretchDown(5 - Rectangle.Height);
            spriteBatch.DrawRectangle(rec, Color.Black);
            base.Draw(spriteBatch, gameTime);
        }
    }
}
