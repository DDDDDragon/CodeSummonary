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
    public class VirusEnemy : Enemy
    {
        public VirusEnemy() 
        {
            _texture = Main.TextureManager[TexType.Entity, "VirusEnemy", 5];

            Damage = 2;

            MaxCodeNum = 20;

            CodeNum = 20;

            AttackTimer = 0;
        }

        public override void PostAttack()
        {
            CodeNum += 1;
            StringEntity.Spawn(
                "1", Main.LocalPlayer.GameView,
                Position + Rectangle.Size.ToVector2() / 2 - new Vector2(32, 22),
                ((float)((Main.Random.NextDouble() / 2 - 1) * Math.PI)).GetAngle() * 2,
                Color.Green
            );
        }
    }
}
