using CodeSummonary.Components;
using CodeSummonary.Components.Entities;
using CodeSummonary.Managers;
using CodeSummonary.Players;
using CodeSummonary.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace CodeSummonary
{
    public class Main : Game
    {
        internal GraphicsDeviceManager _graphics;
        internal SpriteBatch _spriteBatch;

        public static int GameHeight => Instance._graphics.PreferredBackBufferHeight;
        public static int GameWidth => Instance._graphics.PreferredBackBufferWidth;

        internal static string GamePath => Environment.CurrentDirectory;

        public static Main Instance;
        public static TextureManager TextureManager;
        public static FontManager FontManager;

        public static Scene CurrentScene;

        public static MouseMenu MouseMenu;

        public static Random Random;

        public static Player LocalPlayer;

        public static SummaryEntity MouseEntity;

        public Main()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1500;
            _graphics.PreferredBackBufferHeight = 1200;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Instance = this;
            Random = new Random();
            TextureManager = new TextureManager();
            FontManager = new FontManager();
        }

        protected override void Initialize()
        {
            TextureManager.Load();
            FontManager.Load();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            CurrentScene = new MenuScene();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            CurrentScene.Update(gameTime);

            MouseMenu?.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            CurrentScene.Draw(_spriteBatch, gameTime);

            MouseMenu?.Draw(_spriteBatch, gameTime);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}