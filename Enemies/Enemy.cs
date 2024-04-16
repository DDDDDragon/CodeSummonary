using CodeSummonary.Components;
using CodeSummonary.Components.Entities;
using CodeSummonary.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace CodeSummonary.Enemies
{
    public class Enemy : Component
    {
        public Enemy() { }

        public int CodeNum;

        public int MaxCodeNum;

        public int Damage;

        public int AttackTimer;

        public int Timer;

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if(Timer <= 255) spriteBatch.Draw(_texture, Position, new(0, 0, _texture.Width, _texture.Height), new Color(Color.White, 255 - Timer), 0, new(0, 0), 1f, SpriteEffects.None, 0);
            base.Draw(spriteBatch, gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            if (AttackTimer < 20 && Main.LocalPlayer.GameView.CurrentState == "Fight_Enemy") AttackTimer++;
            if (CodeNum <= 0)
            {
                if(Timer < 255) Timer += 5;
                if (Timer == 255)
                {
                    shouldRecover = true;
                    var chars = Main.Random.Next(20, Main.LocalPlayer.DefeatEnemyCount * 5 + 20);

                    StringEntity.Spawn(
                        $"{chars} $", Main.LocalPlayer.GameView,
                        Position + Rectangle.Size.ToVector2() / 2 - new Vector2(32, 22),
                        ((float)((Main.Random.NextDouble() / 2 - 1) * Math.PI)).GetAngle() * 2,
                        new Color(163, 21, 21)
                    );

                    Main.LocalPlayer.CharNum += chars;

                    Timer = 256;
                }
            }
            base.Update(gameTime);
        }

        public virtual void PostAttack()
        {

        }
    }
}
