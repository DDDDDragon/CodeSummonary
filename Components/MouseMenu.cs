using CodeSummonary.Components.Entities;
using CodeSummonary.Components.Summaries;
using CodeSummonary.Extensions;
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
    public class MouseMenu : ContainableComponent
    {
        public ColumnContainer Container;

        public Summary Target;

        public MouseMenu(Summary target)
        {
            Position = Mouse.GetState().Position.ToVector2();

            Target = target;

            _width = 160;
            _height = 200;

            Container = new ColumnContainer();
            Container.RelativePosition.Y = 10;

            Container.childMiddle = true;
            Container.RegisterChild(new Button("Remove", scale: 2, click: (obj, args) =>
            {
                SummaryEntity.Spawn(
                    Main.LocalPlayer.GameView, target,
                    Position + Rectangle.Size.ToVector2() / 2 - new Vector2(32, 22),
                    ((float)((Main.Random.NextDouble() / 2 - 1) * Math.PI)).GetAngle() * 2
                );
                target.shouldRecover = true;
                Main.MouseMenu = null;
            },
            hover: (obj, args) =>
            {
                var button = obj as Button;
                button.DrawBorder(args.spriteBatch, new Color(43, 145, 175), false);
            }));

            RegisterChild(new UIImage("MouseMenu", 2));
            RegisterChild(Container);
        }

        public override void Update(GameTime gameTime)
        {
            if (!_isHovering && _currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                Main.MouseMenu = null;
            Container.RelativePosition.X = (Width - Container.Width) / 2;
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            base.Draw(spriteBatch, gameTime);
        }
    }
}
