using CodeSummonary.Managers;
using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSummonary.Components.Entities
{
    public class StringEntity : Entity<Component>
    {
        public static void Spawn(string text, GameView gameView, Vector2 position, Vector2? startVelocity = null, Color? textColor = null)
        {
            var ret = new StringEntity();
            ret._font = Main.FontManager["CodeSummonary", 15];
            ret.Text = text;
            ret.Parent = gameView;
            ret.RelativePosition = position;
            ret.Velocity = startVelocity == null ? Vector2.Zero : startVelocity.Value;
            ret.hoverTime = 0;
            ret.dragTime = 0;
            ret.BackgroundColor = textColor == null ? Color.Black : textColor.Value;
            gameView.Children.Add(ret);
        }

        public override int Height => 0;

        public override int Width => 0;

        public int Timer = 0;

        public override void Update(GameTime gameTime)
        {
            if (Timer < 255) Timer += 5;
            else shouldRecover = true;
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.DrawString(_font, Text, Position, new Color(BackgroundColor, 255 - Timer));
        }
    }
}
