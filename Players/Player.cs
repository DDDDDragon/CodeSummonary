using Microsoft.Xna.Framework.Graphics;
using CodeSummonary.Managers;
using Microsoft.Xna.Framework;
using CodeSummonary.Components;

namespace CodeSummonary.Players
{
    public class Player : Component
    {
        public Vector2 Size => new(_texture.Width, _texture.Height);

        public bool shouldDrawOthers;

        public GameView GameView;

        public Inventory Inventory;

        public bool CanMove;

        public bool CanAttack;

        public int CharNum;

        public int DefeatEnemyCount;

        public bool firstMeetAltar;

        public int MaxCodeNum;

        public int CodeNum;

        public int Damage;

        public int CodeHeal;

        public Player() 
        {
            _texture = Main.TextureManager[TexType.Entity, "Player", 5];
            shouldDrawOthers = false;

            Main.LocalPlayer = this;

            CharNum = 20;
            DefeatEnemyCount = 0;
            firstMeetAltar = false;
            MaxCodeNum = 50;
            CodeNum = 50;
            Damage = 0;
            CodeHeal = 1;
        }

        public Player(Vector2 relativePosition, GameView gameView)
        {
            _texture = Main.TextureManager[TexType.Entity, "Player", 5];
            shouldDrawOthers = false;

            Main.LocalPlayer = this;

            CharNum = 20;
            DefeatEnemyCount = 0;
            firstMeetAltar = false;
            MaxCodeNum = 15;
            CodeNum = 15;
            Damage = 0;
            CodeHeal = 1;

            GameView = gameView;
            RelativePosition = relativePosition;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (!_init) return;
            spriteBatch.Draw(_texture, new Rectangle((Position + DrawOffset).ToPoint(), Size.ToPoint()), Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            CanMove = false;
            CanAttack = false;
            Damage = 0;
            MaxCodeNum = 15;
            CodeHeal = 1;
            base.Update(gameTime);

            if (CodeNum > MaxCodeNum)
                CodeNum = MaxCodeNum;
        }
    }
}
