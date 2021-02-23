
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
        private List<src.Unit> units;
        private src.Unit marineUnit;

        private List<src.Marker> markers;
        private bool readyForMarker;
        private float markerTimer = 0f;
        private src.SelectBox selectBox;
        private src.Line line;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            readyForMarker = true;
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

            // units
            units = new List<src.Unit>();
            marineUnit = new src.Unit(Content, "marine-Sheet32");
            units.Add(marineUnit);

            // ui
            markers = new List<src.Marker>();
            selectBox = new src.SelectBox(GraphicsDevice, marineUnit);
            line = new src.Line(_spriteBatch, Vector2.Zero, new Vector2(-400, 99), Color.White);
        }

        protected override void UnloadContent()
        {
            Content.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
            // player input
            src.Input.Update();

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;


            // gatekeep marker creation
            if (!readyForMarker)
            {
                markerTimer += dt;
                if (markerTimer >= 0.133f)
                {
                    readyForMarker = true;
                    markerTimer = 0f;
                }
            }

            // Exit Game
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            src.Camera.Update(dt);
            foreach (src.Unit unit in units)
            {
                unit.Update(dt);

                // show waypoint positions
                if (marineUnit.GetSelectionState() && src.Input.mState.RightButton == ButtonState.Pressed && readyForMarker)
                {
                    markers.Add(new src.Marker(Content, src.Input.GetMouseWorldPos()));
                    readyForMarker = false;
                }
            }


            selectBox.Update();

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

            foreach (src.Unit unit in units)
            {
                unit.Draw(_spriteBatch);
            }

            selectBox.Draw(_spriteBatch);
            line.Draw(_spriteBatch);

            // draw markers
            foreach (src.Marker marker in markers)
            {
                marker.Draw(_spriteBatch);
            }

            _spriteBatch.End();
            

            base.Draw(gameTime);
        }
    }
}
