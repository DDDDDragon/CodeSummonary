using CodeSummonary.Components.Summaries;
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
    public class SummaryEntity : Entity<Summary>
    {
        public static void Spawn<T>(GameView gameView, Vector2 position, Vector2? startVelocity = null) where T : Summary
        {
            var ret = new SummaryEntity();
            ret.Value = (Summary)Activator.CreateInstance(typeof(T));
            ret.Parent = gameView;
            ret.RelativePosition = position;
            ret._texture = Main.TextureManager[TexType.Entity, "SummaryEntity", 2];
            ret.Velocity = startVelocity == null ? Vector2.Zero : startVelocity.Value;
            ret.hoverTime = 0;
            ret.dragTime = 0;
            gameView.Children.Add(ret);
        }

        public static void Spawn(GameView gameView, Summary summary, Vector2 position, Vector2? startVelocity = null)
        {
            var ret = new SummaryEntity();
            ret.Value = (Summary)Activator.CreateInstance(summary.GetType());
            ret.Parent = gameView;
            ret.RelativePosition = position;
            ret._texture = Main.TextureManager[TexType.Entity, "SummaryEntity", 2];
            ret.Velocity = startVelocity == null ? Vector2.Zero : startVelocity.Value;
            ret.hoverTime = 0;
            ret.dragTime = 0;
            gameView.Children.Add(ret);
        }

        public override int Height => _texture.Height;

        public override int Width => _texture.Width;

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (_isHovering && dragTime < 2)
            {
                if (hoverTime < 30) 
                    hoverTime++;
                else
                    spriteBatch.Draw(Value._texture, _currentMouse.Position.ToVector2() + new Vector2(4, 4), Color.White);
            }
            else
            {
                hoverTime = 0;
            }
            base.Draw(spriteBatch, gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            if (Rectangle.Intersects(Main.LocalPlayer.Rectangle) && dragTime == 2 && _currentMouse.LeftButton == ButtonState.Released && Value.AddToPlayer())
            {

                Main.LocalPlayer.GameView.AddSummary(Value);
                shouldRecover = true;
            }
            base.Update(gameTime);
        }
    }
}
