using CodeSummonary.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSummonary.Components.Summaries
{
    public class DmgUpSummary : Summary
    {
        public DmgUpSummary()
        {
            _texture = Main.TextureManager[TexType.UI, "DamageUp", 2];

            Click += (obj, args) =>
            {
                if (Main.MouseMenu == null) Main.MouseMenu = new MouseMenu(this);
            };

            Effect += (obj, player) =>
            {
                player.Damage += 2;
            };
        }
    }
}
