using CodeSummonary.Animations;
using CodeSummonary.Components.Entities;
using CodeSummonary.Extensions;
using CodeSummonary.Players;
using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSummonary.Components
{
    public class PlayerInfos : ContainableComponent
    {
        public PlayerInfos() 
        {
            LocalPlayer = Main.LocalPlayer;

            _width = 1500;
            _height = 500;

            Actions = new ColumnContainer();

            Actions.RelativePosition = new(50, 0);

            var attack = new Button("Attack", scale: 2, hover: (obj, args) =>
            {
                var button = obj as Button;

                if (!LocalPlayer.CanAttack)
                {
                    var drawPos = Mouse.GetState().Position.ToVector2() + new Vector2(20, 20);

                    var text = "You cannot attack.";

                    args.spriteBatch.DrawString(Main.FontManager["CodeSummonary", 10], text, drawPos + new Vector2(0, 2), Color.White);
                    args.spriteBatch.DrawString(Main.FontManager["CodeSummonary", 10], text, drawPos + new Vector2(0, -2), Color.White);
                    args.spriteBatch.DrawString(Main.FontManager["CodeSummonary", 10], text, drawPos + new Vector2(2, 0), Color.White);
                    args.spriteBatch.DrawString(Main.FontManager["CodeSummonary", 10], text, drawPos + new Vector2(-2, 0), Color.White);
                    args.spriteBatch.DrawString(Main.FontManager["CodeSummonary", 10], text, drawPos, Color.Black);

                    return;
                }

                if(LocalPlayer.GameView.CurrentState == "Fight_Player")
                    button.DrawBorder(args.spriteBatch, Color.Black, false);
                else
                {
                    args.spriteBatch.Draw(button._texture, button.Position, new(0, 0, button._texture.Width, button._texture.Height), Color.White, 0, new(0, 0), 1f, SpriteEffects.None, 0);

                    var drawPos = Mouse.GetState().Position.ToVector2() + new Vector2(20, 20);

                    var text = LocalPlayer.GameView.CurrentState == "Fight_Enemy" ? "It's not your turn." : "There's no enemy.";

                    args.spriteBatch.DrawString(Main.FontManager["CodeSummonary", 10], text, drawPos + new Vector2(0, 2), Color.White);
                    args.spriteBatch.DrawString(Main.FontManager["CodeSummonary", 10], text, drawPos + new Vector2(0, -2), Color.White);
                    args.spriteBatch.DrawString(Main.FontManager["CodeSummonary", 10], text, drawPos + new Vector2(2, 0), Color.White);
                    args.spriteBatch.DrawString(Main.FontManager["CodeSummonary", 10], text, drawPos + new Vector2(-2, 0), Color.White);
                    args.spriteBatch.DrawString(Main.FontManager["CodeSummonary", 10], text, drawPos, Color.Black);
                }
            },
            drawing: (obj, args) =>
            {
                var button = obj as Button;

                args.spriteBatch.Draw(button._texture, button.Position, new(0, 0, button._texture.Width, button._texture.Height), Color.White, 0, new(0, 0), 1f, SpriteEffects.None, 0);
            },
            click: (obj, args) =>
            {
                if(LocalPlayer.GameView.CurrentState == "Fight_Player" && LocalPlayer.CurrentAnimation.MaxTime == 0 && LocalPlayer.CanAttack)
                {
                    var enemy = LocalPlayer.GameView.CurrentEnemy;
                    LocalPlayer.CurrentAnimation = new ToAndReturnAnimation(LocalPlayer, 0.08f, new(1, 0), 12, 1);
                    enemy.CodeNum -= LocalPlayer.Damage;
                    StringEntity.Spawn(
                        $"{LocalPlayer.Damage}", Main.LocalPlayer.GameView,
                        enemy.Position + enemy.Rectangle.Size.ToVector2() / 2 - new Vector2(32, 22),
                        ((float)((Main.Random.NextDouble() / 2 - 1) * Math.PI)).GetAngle() * 2,
                        Color.Red
                    );
                    LocalPlayer.GameView.CurrentState = "Fight_Enemy";
                }
            });

            var go = new Button("Go", scale: 2, hover: (obj, args) =>
            {
                var button = obj as Button;

                if (!LocalPlayer.CanMove)
                {
                    var drawPos = Mouse.GetState().Position.ToVector2() + new Vector2(20, 20);

                    var text = "You cannot move.";

                    args.spriteBatch.DrawString(Main.FontManager["CodeSummonary", 10], text, drawPos + new Vector2(0, 2), Color.White);
                    args.spriteBatch.DrawString(Main.FontManager["CodeSummonary", 10], text, drawPos + new Vector2(0, -2), Color.White);
                    args.spriteBatch.DrawString(Main.FontManager["CodeSummonary", 10], text, drawPos + new Vector2(2, 0), Color.White);
                    args.spriteBatch.DrawString(Main.FontManager["CodeSummonary", 10], text, drawPos + new Vector2(-2, 0), Color.White);
                    args.spriteBatch.DrawString(Main.FontManager["CodeSummonary", 10], text, drawPos, Color.Black);

                    args.spriteBatch.Draw(button._texture, button.Position, new(0, 0, button._texture.Width, button._texture.Height), Color.White, 0, new(0, 0), 1f, SpriteEffects.None, 0);

                    return;
                }

                if (LocalPlayer.GameView.CurrentState == "Continue")
                    button.DrawBorder(args.spriteBatch, Color.Black, false);
                else if (LocalPlayer.GameView.CurrentState == "Fight_Player" || LocalPlayer.GameView.CurrentState == "Fight_Enemy")
                {
                    args.spriteBatch.Draw(button._texture, button.Position, new(0, 0, button._texture.Width, button._texture.Height), Color.White, 0, new(0, 0), 1f, SpriteEffects.None, 0);

                    var drawPos = Mouse.GetState().Position.ToVector2() + new Vector2(20, 20);

                    var text = "You have not done this fight.";

                    args.spriteBatch.DrawString(Main.FontManager["CodeSummonary", 10], text, drawPos + new Vector2(0, 2), Color.White);
                    args.spriteBatch.DrawString(Main.FontManager["CodeSummonary", 10], text, drawPos + new Vector2(0, -2), Color.White);
                    args.spriteBatch.DrawString(Main.FontManager["CodeSummonary", 10], text, drawPos + new Vector2(2, 0), Color.White);
                    args.spriteBatch.DrawString(Main.FontManager["CodeSummonary", 10], text, drawPos + new Vector2(-2, 0), Color.White);
                    args.spriteBatch.DrawString(Main.FontManager["CodeSummonary", 10], text, drawPos, Color.Black);
                }
                else
                    args.spriteBatch.Draw(button._texture, button.Position, new(0, 0, button._texture.Width, button._texture.Height), Color.White, 0, new(0, 0), 1f, SpriteEffects.None, 0);
            },
            drawing: (obj, args) =>
            {
                var button = obj as Button;

                args.spriteBatch.Draw(button._texture, button.Position, new(0, 0, button._texture.Width, button._texture.Height), Color.White, 0, new(0, 0), 1f, SpriteEffects.None, 0);
            },
            click: (obj, args) => 
            {
                if (LocalPlayer.CanMove && LocalPlayer.GameView.CurrentState == "Continue")
                {
                    if (LocalPlayer.GameView.ContainsChild(t => t is Button))
                        LocalPlayer.GameView.SelectChild(t => t is Button).CanClick = false;
                    LocalPlayer.GameView.CurrentState = "Select";
                }
            });

            Actions.RegisterChild(attack);

            Actions.RegisterChild(new Space(1, 20));

            Actions.RegisterChild(go);

            RegisterChild(Actions);

            Infos = new ColumnContainer();

            Infos.RegisterChild(new UIText("CodeSummonary", new(100, 20), "", fontSize: 15));

            Infos.RegisterChild(new Space(1, 10));

            Infos.RegisterChild(new UIText("CodeSummonary", new(100, 20), "", fontSize: 15));

            Infos.RegisterChild(new Space(1, 10));

            Infos.RegisterChild(new UIText("CodeSummonary", new(100, 20), "", fontSize: 15));

            Infos.RegisterChild(new Space(1, 10));

            Infos.RegisterChild(new UIText("CodeSummonary", new(100, 20), "", fontSize: 15));

            Infos.RegisterChild(new Space(1, 10));

            Infos.RegisterChild(new UIText("CodeSummonary", new(100, 20), "", fontSize: 15));

            Infos.RegisterChild(new Space(1, 10));

            Infos.RegisterChild(new UIText("CodeSummonary", new(100, 20), "", fontSize: 15));

            RegisterChild(Infos);
        }

        public Player LocalPlayer;

        public ColumnContainer Actions;

        public ColumnContainer Infos;

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.DrawRectangle(new Rectangle((Actions.Position - new Vector2(16, 16)).ToPoint(), new Point(Actions.Width + 32, Actions.Height + 32)), Color.Black);
            spriteBatch.DrawRectangle(new Rectangle((Actions.Position - new Vector2(12, 12)).ToPoint(), new Point(Actions.Width + 24, Actions.Height + 24)), Color.White);

            base.Draw(spriteBatch, gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            Infos.RelativePosition = new(1500 - Infos.Width - 250, -20);

            Infos.Children[0].Text = $"Code : {LocalPlayer.CodeNum} / {LocalPlayer.MaxCodeNum}";

            Infos.Children[2].Text = $"Char : {LocalPlayer.CharNum} $";

            var enemyMaxCode = LocalPlayer.GameView.CurrentEnemy != null ? LocalPlayer.GameView.CurrentEnemy.MaxCodeNum : 0;

            var enemyCode = LocalPlayer.GameView.CurrentEnemy != null ? LocalPlayer.GameView.CurrentEnemy.CodeNum : 0;

            var enemyDmg = LocalPlayer.GameView.CurrentEnemy != null ? LocalPlayer.GameView.CurrentEnemy.Damage : 0;

            Infos.Children[4].Text = $"Damage : {LocalPlayer.Damage}";

            Infos.Children[6].Text = $"Code Heal : {LocalPlayer.CodeHeal}";

            Infos.Children[8].Text = $"Enemy Code : {enemyCode} / {enemyMaxCode}";

            Infos.Children[10].Text = $"Enemy Damage : {enemyDmg}";
            base.Update(gameTime);
        }
    }
}
