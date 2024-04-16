using CodeSummonary.Managers;

namespace CodeSummonary.Enemies
{
    public class WarningEnemy : Enemy
    {
        public WarningEnemy() 
        {
            _texture = Main.TextureManager[TexType.Entity, "WarningEnemy", 5];

            Damage = 4;

            MaxCodeNum = 10;

            CodeNum = 10;

            AttackTimer = 0;
        }
    }
}
