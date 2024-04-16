using CodeSummonary.Components;
using CodeSummonary.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CodeSummonary.Scenes
{
    public class MenuScene : Scene
    {
        public MenuScene() 
        {
            BackgroundColor = Color.White;

            var columnContainer = new ColumnContainer();
            columnContainer.childMiddle = false;
            columnContainer.Children.Add(new UIImage("Summary", 5));
            columnContainer.Children.Add(new UIImage("SummaryEnd", 5));
            columnContainer.Children.Add(new UIImage("Title", 5));

            columnContainer.Position.Y = 100;
            columnContainer.Middle = true;

            Components.Add(columnContainer);

            var player = new UIImage("Player", scale: 5, type: Managers.TexType.Entity);

            player.Middle = true;
            player.Position.Y = 400;

            Components.Add(player);

            var start = new Button("Start", texColor: new Color(116, 83, 31), scale: 4, hover: (obj, args) =>
            {
                var button = (obj as Button);

                if (button._currentMouse.LeftButton == ButtonState.Pressed)
                    args.spriteBatch.DrawRectangle(button.Rectangle.StretchDown(20), new Color(136, 186, 214));

                args.spriteBatch.Draw(button._texture, button.Position,
                    new(0, 0, button._texture.Width, button._texture.Height), 
                    new Color(0, 0, 255), 0, new(0, 0), 1f, SpriteEffects.None, 0);

                args.spriteBatch.DrawRectangle(new(button.Position.ToPoint() + new Point(0, button.Height + 5), new(button.Width, 5)), new Color(0, 0, 255));
            }, 
            click: (obj, args) => {
                Main.CurrentScene = new GameScene(); 
            });

            var StartRow = new RowContainer();
            StartRow.RegisterChild(new UIImage("Code", 4));
            StartRow.RegisterChild(new Space(20, 20));
            StartRow.RegisterChild(start);
            StartRow.RegisterChild(new UIImage("( )", 4));

            StartRow.Middle = true;
            StartRow.Position.Y = 600;

            Components.Add(StartRow);

            var exit = new Button("Exit", texColor: new Color(116, 83, 31), scale: 4, hover: (obj, args) =>
            {
                var button = (obj as Button);

                if (button._currentMouse.LeftButton == ButtonState.Pressed)
                    args.spriteBatch.DrawRectangle(button.Rectangle.StretchDown(20), new Color(136, 186, 214));

                args.spriteBatch.Draw(button._texture, button.Position,
                    new(0, 0, button._texture.Width, button._texture.Height),
                    new Color(0, 0, 255), 0, new(0, 0), 1f, SpriteEffects.None, 0);

                args.spriteBatch.DrawRectangle(new(button.Position.ToPoint() + new Point(0, button.Height + 5), new(button.Width, 5)), new Color(0, 0, 255));
            },
            click: (obj, args) => {
                Main.Instance.Exit();
            });

            var ExitRow = new RowContainer();
            ExitRow.RegisterChild(new UIImage("Code", 4));
            ExitRow.RegisterChild(new Space(20, 20));
            ExitRow.RegisterChild(exit);
            ExitRow.RegisterChild(new UIImage("( )", 4));

            ExitRow.Middle = true;
            ExitRow.Position.Y = 700;

            Components.Add(ExitRow);
        }
    }

}