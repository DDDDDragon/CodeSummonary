using CodeSummonary.Managers;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSummonary.Components.Summaries
{
    public class HealUpSummary : Summary
    {
        public HealUpSummary()
        {
            _texture = Main.TextureManager[TexType.UI, "HealUp", 2];

            Click += (obj, args) =>
            {
                if (Main.MouseMenu == null) Main.MouseMenu = new MouseMenu(this);
            };

            Effect += (obj, player) =>
            {
                player.CodeHeal += 1;
            };
        }
    }
}
