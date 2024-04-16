using CodeSummonary.Managers;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSummonary.Components.Summaries
{
    public class CodeUpSummary : Summary
    {
        public CodeUpSummary()
        {
            _texture = Main.TextureManager[TexType.UI, "CodeUp", 2];

            Click += (obj, args) =>
            {
                if (Main.MouseMenu == null) Main.MouseMenu = new MouseMenu(this);
            };

            Effect += (obj, player) =>
            {
                player.MaxCodeNum += 20;
            };
        }
    }
}
