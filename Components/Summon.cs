using CodeSummonary.Animations;
using CodeSummonary.Components.Entities;
using CodeSummonary.Components.Summaries;
using CodeSummonary.Extensions;
using Microsoft.Xna.Framework;
using System;

namespace CodeSummonary.Components
{
    public class Summon<T> : ContainableComponent where T : Summary
    {
        public Summon() 
        {
            RegisterChild(new UIImage("Summon", 2));

            Rotation = 0;
            _width = 108;
            _height = 100;

            Click += (obj, args) =>
            {
                if (Main.Random.Next(ClickCount - 12, 12 - ClickCount) == 0 && ClickCount >= 12)
                {
                    shouldRecover = true;
                    Clickable = false;

                    if (this is Summon<Summary>)
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
                    else SummaryEntity.Spawn<T>(
                        Main.LocalPlayer.GameView, 
                        Position + Rectangle.Size.ToVector2() / 2 - new Vector2(32, 22), 
                        ((float)((Main.Random.NextDouble() / 2 - 1) * Math.PI)).GetAngle() * 2
                    );
                }
                else
                {
                    ClickCount++;
                    CurrentAnimation = new PunchAnimation(this, 100, 4, "");
                }
            };
        }

        public float DeltaRotation = 0;

        public int ClickCount = 0;

        public bool Clickable = true;

        public override void Update(GameTime gameTime)
        {
            if (!Clickable) shouldRecover = true;
            foreach (var component in Children)
            {
                component.DrawOffset = DrawOffset;
                component.Rotation = Rotation;
            }
            DrawOffset /= 2;
            if (DrawOffset.Length() < 0.3f) DrawOffset = Vector2.Zero;
            Rotation += 0.01f;
            base.Update(gameTime);
        }
    }
}
