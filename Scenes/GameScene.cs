using CodeSummonary.Components;
using CodeSummonary.Extensions;
using CodeSummonary.Managers;
using CodeSummonary.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CodeSummonary.Scenes
{
    public class GameScene : Scene
    {
        public GameScene() 
        {
            BackgroundColor = Color.White;

            var player = new Player();

            var gameView = new GameView(player);

            Components.Add(gameView);

            var playerInfos = new BottomContainer();
            playerInfos.AnchorBottom = true;
            playerInfos.LocalPlayer = player;

            Components.Add(playerInfos);
        }
    }
}
