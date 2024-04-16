using CodeSummonary.Animations;
using CodeSummonary.Components.Entities;
using CodeSummonary.Components.Summaries;
using CodeSummonary.Enemies;
using CodeSummonary.Extensions;
using CodeSummonary.Managers;
using CodeSummonary.Players;
using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace CodeSummonary.Components
{
    public class GameView : ContainableComponent
    {
        public Player LocalPlayer;

        public ColumnContainer Modifiers;

        public ColumnContainer Summaries;

        public CollidableComponent Colliable;

        public bool GameStart;

        public string CurrentState;

        public Enemy CurrentEnemy;

        public GameView(Player player) 
        {
            //var background = new UIImage("playerInfos", 5);

            _height = 800;

            _width = 1500;

            GameStart = false;

            CurrentState = "Summon";

            LocalPlayer = player;

            LocalPlayer.GameView = this;

            LocalPlayer.RelativePosition = new Vector2((Main.GameWidth - player.Width) / 2, 300);

            RegisterChild(LocalPlayer);

            Summaries = new ColumnContainer();

            Summaries.childMiddle = false;

            Summaries.RegisterChild(new UIImage("Summary", 2));
            Summaries.RegisterChild(new UIImage("SummaryEnd", 2));

            Summaries.RelativePosition = player.RelativePosition + new Vector2(0, -100);

            RegisterChild(Summaries);

            Colliable = new CollidableComponent();

            Colliable._width = Width;
            Colliable._height = 100;

            Colliable.AnchorBottom = true;

            RegisterChild(Colliable);
        }

        public void AddSummary(Summary summary)
        {
            Summaries.RegisterChildAt(1, summary, true);
        }

        public bool ContainsSummary(Predicate<Component> match)
        {
            return Summaries.ContainsChild(match);
        }

        public int SummaryCount()
        {
            return Summaries.Children.Count;
        }

        public override void Update(GameTime gameTime)
        {
            var Update = new List<Component>(Children);

            foreach (var component in Update)
            {
                if(component.shouldRecover) Children.Remove(component);

                if(component is not Player && component is not ColumnContainer)
                {
                    if (component.Middle)
                        component.RelativePosition.X = (Main.GameWidth - component.Width) / 2;
                    component.Position = component.RelativePosition + Position;
                }
                if(component.AnchorBottom)
                    component.Position.Y = Height - component.Height;

                component.Update(gameTime);
            }

            LocalPlayer.Position = Position + LocalPlayer.RelativePosition;
            var summariesX = LocalPlayer.Position.X + LocalPlayer.Width / 2 - Summaries.Width / 2;
            Summaries.Position = new Vector2(summariesX, LocalPlayer.Position.Y - Summaries.Height - 20);

            if (GameStart)
            {
                switch (CurrentState)
                {
                    case "Select":
                        var state = Main.Random.Next(0, 3);

                        switch (state)
                        {
                            case 0:
                                CurrentState = "Summon";
                                break;
                            case 1:
                                CurrentState = "Enemy";
                                break;
                            case 3:
                                CurrentState = "Chest";
                                break;
                        }
                        break;
                    case "Summon":
                        var altar = new Button("Altar", scale: 2, hover: (obj, args) =>
                        {
                            var button = obj as Button;

                            if (!button.CanClick && button.Timer < 255) button.Timer += 5;

                            if (button.CurrentAnimation.MaxTime == 0 && CurrentState != "Select")
                                CurrentState = "Continue";

                            args.spriteBatch.Draw(button._texture, button.Position, new(0, 0, button._texture.Width, button._texture.Height), button.TexColor, 0, new(0, 0), 1f, SpriteEffects.None, 0);

                            var drawPos = Mouse.GetState().Position.ToVector2() + new Vector2(20, 20);

                            var text = button.CanClick
                                ? $"Need {LocalPlayer.CharNum} / {LocalPlayer.DefeatEnemyCount * 5 + 20} chars."
                                : "Cannot summon summary again.";

                            args.spriteBatch.DrawString(Main.FontManager["CodeSummonary", 10], text, drawPos + new Vector2(0, 2), Color.White);
                            args.spriteBatch.DrawString(Main.FontManager["CodeSummonary", 10], text, drawPos + new Vector2(0, -2), Color.White);
                            args.spriteBatch.DrawString(Main.FontManager["CodeSummonary", 10], text, drawPos + new Vector2(2, 0), Color.White);
                            args.spriteBatch.DrawString(Main.FontManager["CodeSummonary", 10], text, drawPos + new Vector2(-2, 0), Color.White);
                            args.spriteBatch.DrawString(Main.FontManager["CodeSummonary", 10], text, drawPos, Color.Black);
                        },
                        drawing: (obj, args) =>
                        {
                            var button = obj as Button;

                            if (button.CurrentAnimation.MaxTime == 0 && CurrentState != "Select")
                                CurrentState = "Continue";

                            if (!button.CanClick && button.Timer < 255) 
                                button.Timer += 5;
                            if(button.Timer < 255)
                                args.spriteBatch.Draw(button._texture, button.Position, new(0, 0, button._texture.Width, button._texture.Height), new(button.TexColor, 255 - button.Timer), 0, new(0, 0), 1f, SpriteEffects.None, 0);
                            else
                                button.shouldRecover = true;
                        },
                        click: (obj, args) =>
                        {
                            var button = obj as Button;

                            if (button.CurrentAnimation.MaxTime != 0) return;

                            if (LocalPlayer.CharNum >= LocalPlayer.DefeatEnemyCount * 5 + 20)
                            {
                                LocalPlayer.CharNum -= LocalPlayer.DefeatEnemyCount * 5 + 20;
                                button.CanClick = false;

                                Component summon = null;
                                summon = new Summon<Summary>();
                                summon.RelativePosition = button.RelativePosition + new Vector2(button.Width - summon.Width, -200) / 2;

                                LocalPlayer.GameView.RegisterChild(summon);
                            }
                        });
                        altar.RelativePosition = new(1600, 700 - altar.Height);
                        altar.CurrentAnimation = new DirectMoveAnimation(altar, 2.5f, new(-1, 0), 13);
                        LocalPlayer.CurrentAnimation = new ToAndReturnAnimation(LocalPlayer, 0.4f, new(0, -1), 3, 5);
                        RegisterChild(altar);
                        CurrentState = "Summon_Wait";
                        break;
                    case "Enemy":
                        var summary = Main.Random.Next(0, 3);

                        switch (summary)
                        {
                            case 0:
                                CurrentEnemy = new WarningEnemy();
                                break;
                            case 1:
                                CurrentEnemy = new BugEnemy();
                                break;
                            case 2:
                                CurrentEnemy = new VirusEnemy();
                                break;
                        }
                        CurrentEnemy.RelativePosition = new(1600, 705 - CurrentEnemy.Height);
                        CurrentEnemy.CurrentAnimation = new DirectMoveAnimation(CurrentEnemy, 2.5f, new(-1, 0), 13);
                        LocalPlayer.CurrentAnimation = new ToAndReturnAnimation(LocalPlayer, 0.4f, new(0, -1), 3, 5);
                        RegisterChild(CurrentEnemy);
                        CurrentState = "Fight_Player";
                        break;
                    case "Fight_Player":
                        if (LocalPlayer.CodeNum <= 0)
                            CurrentState = "Lose";
                        break;
                    case "Fight_Enemy":
                        if (CurrentEnemy.CodeNum <= 0)
                        {
                            CurrentState = "Continue";

                            LocalPlayer.CodeNum += LocalPlayer.CodeHeal * 5;
                            StringEntity.Spawn(
                                $"{LocalPlayer.CodeHeal * 3}", Main.LocalPlayer.GameView,
                                LocalPlayer.Position + LocalPlayer.Rectangle.Size.ToVector2() / 2 - new Vector2(32, 22),
                                ((float)((Main.Random.NextDouble() / 2 - 1) * Math.PI)).GetAngle() * 2,
                                Color.Green
                            );

                            LocalPlayer.DefeatEnemyCount++;
                        }
                        else if (CurrentEnemy.AttackTimer == 20)
                        {
                            CurrentEnemy.CurrentAnimation = new ToAndReturnAnimation(CurrentEnemy, 0.08f, new(-1, 0), 12, 1);
                            LocalPlayer.CodeNum -= CurrentEnemy.Damage;
                            StringEntity.Spawn(
                                $"{CurrentEnemy.Damage}", Main.LocalPlayer.GameView,
                                LocalPlayer.Position + LocalPlayer.Rectangle.Size.ToVector2() / 2 - new Vector2(32, 22),
                                ((float)((Main.Random.NextDouble() / 2 - 1) * Math.PI)).GetAngle() * 2,
                                Color.Red
                            );
                            LocalPlayer.CodeNum += LocalPlayer.CodeHeal;
                            StringEntity.Spawn(
                                $"{LocalPlayer.CodeHeal}", Main.LocalPlayer.GameView,
                                LocalPlayer.Position + LocalPlayer.Rectangle.Size.ToVector2() / 2 - new Vector2(32, 22),
                                ((float)((Main.Random.NextDouble() / 2 - 1) * Math.PI)).GetAngle() * 2,
                                Color.Green
                            );
                            LocalPlayer.GameView.CurrentState = "Fight_Player";
                            CurrentEnemy.AttackTimer = 0;

                            CurrentEnemy.PostAttack();
                        }
                        break;
                    case "Chest":
                        var chest = new Button("Chest", scale: 2, hover: (obj, args) =>
                        {
                            var button = obj as Button;

                            if (!button.CanClick && button.Timer < 255) button.Timer += 5;

                            args.spriteBatch.Draw(button._texture, button.Position, new(0, 0, button._texture.Width, button._texture.Height), button.TexColor, 0, new(0, 0), 1f, SpriteEffects.None, 0);

                            var drawPos = Mouse.GetState().Position.ToVector2() + new Vector2(20, 20);

                            var text = "Left click to open it.";

                            args.spriteBatch.DrawString(Main.FontManager["CodeSummonary", 10], text, drawPos + new Vector2(0, 2), Color.White);
                            args.spriteBatch.DrawString(Main.FontManager["CodeSummonary", 10], text, drawPos + new Vector2(0, -2), Color.White);
                            args.spriteBatch.DrawString(Main.FontManager["CodeSummonary", 10], text, drawPos + new Vector2(2, 0), Color.White);
                            args.spriteBatch.DrawString(Main.FontManager["CodeSummonary", 10], text, drawPos + new Vector2(-2, 0), Color.White);
                            args.spriteBatch.DrawString(Main.FontManager["CodeSummonary", 10], text, drawPos, Color.Black);
                        },
                        drawing: (obj, args) =>
                        {
                            var button = obj as Button;

                            if (!button.CanClick && button.Timer < 255)
                                button.Timer += 5;
                            if (button.Timer < 255)
                                args.spriteBatch.Draw(button._texture, button.Position, new(0, 0, button._texture.Width, button._texture.Height), new(button.TexColor, 255 - button.Timer), 0, new(0, 0), 1f, SpriteEffects.None, 0);
                            else
                                button.shouldRecover = true;
                        },
                        click: (obj, args) =>
                        {
                            var button = obj as Button;

                            if (button.CurrentAnimation.MaxTime != 0) return;

                            button.CanClick = false;

                            if (Main.Random.Next(0, 1) == 0)
                            {
                                var summary = Main.Random.Next(0, 3);

                                var spawn = new Summary();

                                switch (summary)
                                {
                                    case 0:
                                        spawn = new DmgUpSummary();
                                        break;
                                    case 1:
                                        spawn = new CodeUpSummary();
                                        break;
                                    case 2:
                                        spawn = new HealUpSummary();
                                        break;
                                }

                                SummaryEntity.Spawn(
                                    Main.LocalPlayer.GameView, spawn,
                                    Position + Rectangle.Size.ToVector2() / 2 - new Vector2(32, 22),
                                    ((float)((Main.Random.NextDouble() / 2 - 1) * Math.PI)).GetAngle() * 2
                                );
                            }
                            if (Main.Random.Next(0, 1) == 0)
                            {
                                var chars = Main.Random.Next(20, Main.LocalPlayer.DefeatEnemyCount * 5 + 20);

                                StringEntity.Spawn(
                                    $"{chars} $", Main.LocalPlayer.GameView,
                                    Position + Rectangle.Size.ToVector2() / 2 - new Vector2(32, 22),
                                    ((float)((Main.Random.NextDouble() / 2 - 1) * Math.PI)).GetAngle() * 2,
                                    new Color(163, 21, 21)
                                );

                                Main.LocalPlayer.CharNum += chars;
                            }
                            CurrentState = "Continue";
                        });
                        chest.RelativePosition = new(1600, 700 - chest.Height);
                        chest.CurrentAnimation = new DirectMoveAnimation(chest, 2.5f, new(-1, 0), 13);
                        LocalPlayer.CurrentAnimation = new ToAndReturnAnimation(LocalPlayer, 0.4f, new(0, -1), 3, 5);
                        RegisterChild(chest);
                        CurrentState = "Chest_Wait";
                        break;
                    case "Lose":
                        var p = new Player(LocalPlayer.RelativePosition, LocalPlayer.GameView);
                        if (CurrentEnemy != null) CurrentEnemy = null;
                        CurrentState = "Summon";
                        break;
                }
            }

            if (!_init) _init = true;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (!_init) return;
            LocalPlayer.Draw(spriteBatch, gameTime);
            if (LocalPlayer.shouldDrawOthers)
                foreach (var component in Children)
                    if(component is not Player) 
                        component.Draw(spriteBatch, gameTime);
            if(CurrentState == "Continue")
                if (gameTime.TotalGameTime.TotalMilliseconds % 1000 <= 500)
                    spriteBatch.Draw(Main.TextureManager[TexType.UI, "NextEvent", 3], Position + new Vector2(1400, 250), Color.Black);
        }
    }
}
