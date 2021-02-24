
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Micro_Marine
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch _spriteBatch;
        private src.WorldManager worldManager;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {

            graphics.PreferredBackBufferWidth = src.Camera.Width;
            graphics.PreferredBackBufferHeight = src.Camera.Height;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            worldManager = new src.WorldManager(Content, _spriteBatch);
            worldManager.Load();
        }

        protected override void UnloadContent()
        {
            Content.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
            // player input
            src.Input.Update();

            // Exit Game
            if (src.Input.kState.IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            src.Camera.Update(gameTime);
            worldManager.Update(gameTime);

            // player input
            src.Input.UpdatePrev();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(
                SpriteSortMode.BackToFront,
                BlendState.AlphaBlend,
                null,
                null,
                null,
                null,
                src.Camera.GetTransformation()
            );

            worldManager.Draw();


            _spriteBatch.End();
            

            base.Draw(gameTime);
        }
    }
}
