using CodeSummonary.Animations;
using CodeSummonary.Components.Entities;
using CodeSummonary.Components.Summaries;
using CodeSummonary.Extensions;
using CodeSummonary.Managers;
using CodeSummonary.Players;
using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace CodeSummonary.Components
{
    public class BottomContainer : ContainableComponent
    {
        public Player LocalPlayer;

        public BottomContainer()
        {
            var background = new UIImage("playerInfos", 5);

            _height = background.Height;

            _width = background.Width;

            Children.Add(background);

            var stateContainer = new StateContainer(new(1500, 480), (sender, args) =>
            {
                var container = sender as StateContainer;

                if (container.CurrentState == "introduction_4")
                {
                    if (LocalPlayer.GameView.Children.Exists(t => t is Summon<MoveableSummary>))
                        return;
                }

                if (container.CurrentState == "introduction_5")
                {
                    if (!LocalPlayer.GameView.ContainsSummary(t => t is MoveableSummary))
                        return;
                }
                if(container.CurrentState != "game")
                {
                    var page = int.Parse(container.CurrentState.Replace("introduction_", ""));
                    if (page < container.States.Count - 2)
                        container.SwitchToState($"introduction_{++page}");
                    else if (container.CurrentState == "introduction_9" && LocalPlayer.CurrentAnimation.MaxTime == 0)
                    {
                        LocalPlayer.GameView.GameStart = true;
                        container.SwitchToState("game");
                    }
                }
                
                if (container.CurrentState == "introduction_1") 
                    LocalPlayer.shouldDrawOthers = true;

                if (container.CurrentState == "introduction_4")
                {
                    var summon = new Summon<MoveableSummary>();
                    summon.RelativePosition = new(0, 500);
                    summon.Middle = true;

                    LocalPlayer.GameView.RegisterChild(summon);
                }

                if (container.CurrentState == "introduction_8")
                {
                    SummaryEntity.Spawn<AttackableSummary>(
                        Main.LocalPlayer.GameView, new(734, 500),
                        ((float)((Main.Random.NextDouble() / 2 - 1) * Math.PI)).GetAngle() * 2
                    );

                    Main.LocalPlayer.CurrentAnimation = new DirectMoveAnimation(Main.LocalPlayer, 1.2f, new Vector2(-1, 1), 13);
                }

            },
            draw: (sender, args) =>
            {
                var container = sender as StateContainer;

                if (container.CurrentState == "introduction_4")
                {
                    if (LocalPlayer.GameView.Children.Exists(t => t is Summon<MoveableSummary>))
                        return;
                }

                if (container.CurrentState == "introduction_5")
                {
                    if (!LocalPlayer.GameView.ContainsSummary(t => t is MoveableSummary))
                        return;
                }

                if (container.CurrentState == "introduction_9" && LocalPlayer.CurrentAnimation.MaxTime != 0)
                {
                    return;
                }

                if (container.CurrentState.Contains("introduction") && args.gameTime.TotalGameTime.TotalMilliseconds % 1000 <= 500)
                    args.spriteBatch.Draw(Main.TextureManager[TexType.UI, "NextPage", 3], container.Position + new Vector2(1400, 250), Color.Black);
            },
            updating: (sender, gameTime) =>
            {
                var container = sender as StateContainer;

                if (container.CurrentState == "introduction" && LocalPlayer.CurrentAnimation.Tag != "intro3")
                    LocalPlayer.CurrentAnimation = new DirectMoveAnimation(LocalPlayer, 1.5f, new(-1.5f, 0), 10, "intro3");
            });

            stateContainer.RegisterState("introduction_0", new UIText("CodeSummonary", new(1500, 480),
                @"As you can see, you are a code file.", textMiddle: true));

            stateContainer.RegisterState("introduction_1", new UIText("CodeSummonary", new(1500, 480),
                @"This is a world made of codes and code files. Everyone is composed of code summary and code body. Just like the things above your head.", textMiddle: true, newLineNum: 50));

            stateContainer.RegisterState("introduction_2", new UIText("CodeSummonary", new(1500, 480),
                @"Summary defines what you can, and body defines what you are.", textMiddle: true, newLineNum: 50));

            stateContainer.RegisterState("introduction_3", new UIText("CodeSummonary", new(1500, 480),
                @"Files use summaries and bodies to live, move and even do anything.", textMiddle: true, newLineNum: 50));

            stateContainer.RegisterState("introduction_4", 
                new UIText("CodeSummonary", new(1500, 480),
                @"No one knows where the summaries come from, but you can summon them like this. Now just click this character magic circle.", textMiddle: true, newLineNum: 50));

            stateContainer.RegisterState("introduction_5", new UIText("CodeSummonary", new(1500, 480),
                @"Look, you have your first summary. Now try to drag it to yourself.", textMiddle: true, newLineNum: 50));

            stateContainer.RegisterState("introduction_6", new UIText("CodeSummonary", new(1500, 480),
                @"Great. Now you can move to somewhere and start your adventure.", textMiddle: true, newLineNum: 50));

            stateContainer.RegisterState("introduction_7", new UIText("CodeSummonary", new(1500, 480),
                @"This world should be peaceful, but an evil file wants to rule the world. So you, the chosen one by me the author, gonna break his evil ambition.", textMiddle: true, newLineNum: 50));

            stateContainer.RegisterState("introduction_8", new UIText("CodeSummonary", new(1500, 480),
                @"And I will give you a new summary, keep it until you really know why you remove it.", textMiddle: true, newLineNum: 50));

            stateContainer.RegisterState("introduction_9", new UIText("CodeSummonary", new(1500, 480),
                @"Good luck!", textMiddle: true, newLineNum: 50));

            stateContainer.RegisterState("game", new PlayerInfos());
            stateContainer.SwitchToState("introduction_0");

            stateContainer.RelativePosition = new(0, 50);

            Children.Add(stateContainer);
        }
    }
}
