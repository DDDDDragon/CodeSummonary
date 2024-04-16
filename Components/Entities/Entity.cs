using CodeSummonary.Extensions;
using CodeSummonary.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSummonary.Components.Entities
{
    public class Entity<T> : Component where T : Component
    {
        public static Entity<T> Spawn(GameView gameView)
        {
            return new();
        }

        public T Value;

        public Vector2 Velocity;

        public override int Height => Value.Height;

        public override int Width => Value.Width;

        public bool Collide = false;

        public int hoverTime;

        public int dragTime;

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(_texture, Position, Color.White);
            base.Draw(spriteBatch, gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            Collide = false;

            if(this is SummaryEntity summary)
            {
                if (_isHovering && _currentMouse.LeftButton == ButtonState.Pressed && Main.MouseEntity == null)
                {
                    if (dragTime < 2)
                        dragTime++;
                }
                if (dragTime == 2)
                {
                    if (_currentMouse.LeftButton == ButtonState.Released)
                    {
                        dragTime = 0;
                        Main.MouseEntity = null;
                    }
                    else
                    {
                        RelativePosition = _currentMouse.Position.ToVector2() - new Vector2(32, 22);
                        Collide = true;
                        Velocity = Vector2.Zero;
                        Main.MouseEntity = summary;
                    }
                }
            }

            if (!Collide)
            {
                RelativePosition += Velocity * 2f;

                if (Velocity.Y < 5) Velocity.Y += 0.1f;
            }

            if ((Parent as GameView).Colliable.Rectangle.Intersects(Rectangle))
            {
                Velocity = Vector2.Zero;
                Collide = true;
            }

            if (Position.Y + Height > (Parent as GameView).Colliable.Position.Y)
                Position.Y = (Parent as GameView).Colliable.Position.Y - Height;

            if (Position.X < 0) Position.X = 0;

            if (Position.X + Width > 1500) Position.X = 1500 - Width;
            base.Update(gameTime);
        }
    }
}
