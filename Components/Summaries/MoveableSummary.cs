using CodeSummonary.Managers;
using CodeSummonary.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace CodeSummonary.Components.Summaries
{
    public class MoveableSummary : Summary
    {
        public MoveableSummary()
        {
            _texture = Main.TextureManager[TexType.UI, "Moveable", 2];

            Click += (obj, args) =>
            {
                if (Main.MouseMenu == null) Main.MouseMenu = new MouseMenu(this);
            };

            Effect += (obj, player) =>
            {
                player.CanMove = true;
            };
        }

        public override bool AddToPlayer()
        {
            return !Main.LocalPlayer.GameView.ContainsSummary(t => t is MoveableSummary);
        }
    }
}
