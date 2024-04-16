using CodeSummonary.Components.Entities;
using CodeSummonary.Extensions;
using CodeSummonary.Managers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSummonary.Enemies
{
    public class BugEnemy : Enemy
    {
        public BugEnemy()
        {
            _texture = Main.TextureManager[TexType.Entity, "BugEnemy", 5];

            Damage = 1;

            MaxCodeNum = 25;

            CodeNum = 20;

            AttackTimer = 0;
        }


        public override void PostAttack()
        {
            if (Main.Random.Next(0, 8) == 0)
                Main.LocalPlayer.GameView.CurrentState = "Fight_Enemy";
        }
    }
}
