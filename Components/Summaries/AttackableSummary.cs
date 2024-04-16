using CodeSummonary.Managers;
using CodeSummonary.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSummonary.Components.Summaries
{
    public class AttackableSummary : Summary
    {
        public AttackableSummary()
        {
            _texture = Main.TextureManager[TexType.UI, "Attackable", 2];

            Click += (obj, args) =>
            {
                if (Main.MouseMenu == null) Main.MouseMenu = new MouseMenu(this);
            };

            Effect += (obj, player) =>
            {
                player.CanAttack = true;
                player.Damage += 2;
            };
        }

        public override bool AddToPlayer()
        {
            return !Main.LocalPlayer.GameView.ContainsSummary(t => t is AttackableSummary);
        }
    }
}
