using CodeSummonary.Managers;
using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace CodeSummonary.Components
{
    public class UIText : Component
    {
        public UIText(string fontID, Vector2 size, string text, int fontSize = 20, int newLineNum = int.MaxValue, bool textMiddle = false, Color? fontColor = null) 
        {
            _font = Main.FontManager[fontID, fontSize];

            Text = text;

            NewLineNum = newLineNum;

            TextMiddle = textMiddle;

            FontColor = fontColor == null ? Color.Black : (Color)fontColor;

            _width = (int)size.X;

            _height = (int)size.Y;
        }
        internal int _width;

        internal int _height;

        public override int Width => _width;

        public override int Height => _height;

        public int NewLineNum;

        public bool TextMiddle;

        public Color FontColor;

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (!_init) return;
            if (!string.IsNullOrEmpty(Text))
            {
                var texts = Text.SplitWithCount(NewLineNum);
                int x, y = 0;
                if(TextMiddle)
                {
                    foreach (var text in texts)
                    {
                        var size = _font.MeasureString(text);
                        x = Rectangle.X + Rectangle.Width / 2 - (int)size.X / 2;
                        spriteBatch.DrawString(_font, text, new(x, y + Position.Y), FontColor);
                        y += (int)size.Y;
                    }
                }
                else
                {
                    foreach (var text in texts)
                    {
                        var size = _font.MeasureString(text);
                        x = Rectangle.X;
                        spriteBatch.DrawString(_font, text, new(x, y + Position.Y), FontColor);
                        y += (int)size.Y;
                    }
                }
            }
        }
    }
}
