using CodeSummonary.Managers;
using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace CodeSummonary.Components
{
    public class Button : Component
    {
        public Button(string texID, Color? bgColor = null, Vector2 postion = default, Color? texColor = null, int scale = 1, 
            EventHandler<(SpriteBatch spriteBatch, GameTime gameTime)> hover = null, 
            EventHandler<(SpriteBatch spriteBatch, GameTime gameTime)> drawing = null, EventHandler click = null)
        {
            _texture = Main.TextureManager[TexType.UI, texID, scale];

            BackgroundColor = (Color)(bgColor == null ? Color.Black : bgColor);

            Position = postion == default ? new(0, 0) : postion;

            TexColor = (Color)(texColor == null ? Color.White : texColor);

            CanClick = true;

            Hovering += hover != null ? hover : (obj, args) =>
            {
                DrawBorder(args.spriteBatch);
            };
            Drawing += drawing != null ? drawing : (obj, args) => 
            {
                args.spriteBatch.Draw(_texture, Position, new(0, 0, _texture.Width, _texture.Height), TexColor, 0, new(0, 0), 1f, SpriteEffects.None, 0);
            };
            Click += click != null ? click : (obj, args) => { };
        }

        public Color TexColor;

        public int Timer;

        public event EventHandler<(SpriteBatch spriteBatch, GameTime gameTime)> Drawing;

        public event EventHandler<(SpriteBatch spriteBatch, GameTime gameTime)> Hovering;

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if(!_init) return;
            if (_isHovering)
                Hovering?.Invoke(this, (spriteBatch, gameTime));
            else
                Drawing?.Invoke(this, (spriteBatch, gameTime));
            base.Draw(spriteBatch, gameTime);
        }
    }
}
