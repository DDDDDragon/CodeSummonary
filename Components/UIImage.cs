using CodeSummonary.Components;
using CodeSummonary.Extensions;
using CodeSummonary.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSummonary.Components
{
    public class UIImage : Component
    {
        public UIImage(string texID, int scale = 1, Vector2 postion = default, float rotation = 0, TexType type = TexType.UI)
        {
            _texture = Main.TextureManager[type, texID, scale];
            Scale = scale;
            Position = postion == default ? new(0, 0) : postion;
            Rotation = rotation;
            texid = texID;
        }
        public string texid;

        public Vector2 Size => new(Width, Height);

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (!_init) return;
            spriteBatch.Draw(_texture, new Rectangle(Position.ToPoint() + (Size / 2).ToPoint() + DrawOffset.ToPoint(), Size.ToPoint()), new(Point.Zero, Size.ToPoint()), Color.White, Rotation, Size / 2, SpriteEffects.None, 0);
        }
    }
}
