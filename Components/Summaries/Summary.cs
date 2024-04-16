using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSummonary.Managers;
using CodeSummonary.Players;

namespace CodeSummonary.Components.Summaries
{
    public class Summary : Component
    {
        public Summary() { }

        public Summary(string texID, EventHandler<Player> effect = null)
        {
            _texture = Main.TextureManager[TexType.UI, texID, 2];

            Name = texID;

            Click += (obj, args) => 
            {
                if(Main.MouseMenu == null) Main.MouseMenu = new MouseMenu(this);
            };

            Effect = effect != null ? effect : (obj, player) =>
            {

            };
        }

        public EventHandler<Player> Effect;

        public string Name;

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (!_init) return;
            //if (_isHovering)
                //DrawBorder(spriteBatch, Color.White, Color.Black);
            spriteBatch.Draw(_texture, Position, Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            GiveEffect(Main.LocalPlayer);
        }

        public void GiveEffect(Player player)
        {
            Effect?.Invoke(this, player);
        }

        public virtual bool AddToPlayer()
        {
            return Main.LocalPlayer.GameView.SummaryCount() <= 6;
        }
    }
}
