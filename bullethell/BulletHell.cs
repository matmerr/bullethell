using bullethell.Models;
using bullethell.Story;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System;

namespace bullethell {
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class BulletHell : Game {

        private KeyboardState oldKeyboardState;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private GameContent MainContent;
        private SpriteFont font;


        public BulletHell() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("HUD");

            // set window dimension


            MainContent = new GameContent(
                PlayerShip: Content.Load<Texture2D>("ship"),
                MiddleBoss: Content.Load<Texture2D>("midBoss"),
                Baddie1A: Content.Load<Texture2D>("baddie1-A"),
                GoodBullet: Content.Load<Texture2D>("goodMissile"),
                BadBullet: Content.Load<Texture2D>("badMissile")
            );

            MainContent.SetWindowDimensions(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            MainContent.InitializeModels();
            MainContent.InitializeEvents();
            MainContent.Start();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {

            KeyboardState newKeyboardState = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            // MOVE PLAYER
            // We can use the Direction class that I made to avoid confusion
            if (Keyboard.GetState().IsKeyDown(Keys.Right)) {
                MainContent.PlayerShip.Move(Direction.Right, Direction.Stay);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left)) {
                MainContent.PlayerShip.Move(Direction.Left, Direction.Stay);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up)) {
                MainContent.PlayerShip.Move(Direction.Stay, Direction.Up);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down)) {
                MainContent.PlayerShip.Move(Direction.Stay, Direction.Down);
            }

            // TOGGLE SPEED
            // we have to check the key is down, and not just being spammed
            if (oldKeyboardState.IsKeyUp(Keys.LeftShift) && newKeyboardState.IsKeyDown(Keys.LeftShift)) {
                MainContent.PlayerShip.ToggleRate(5);
            }

            if (oldKeyboardState.IsKeyUp(Keys.Space) && newKeyboardState.IsKeyDown(Keys.Space)) {
                MainContent.AddGoodBullet(MainContent.PlayerShip.Location, 2);
            }

            // update the keyboard state
            oldKeyboardState = newKeyboardState;

            // this method calls all scheduled events in the game timeline
            MainContent.Events.ExecuteScheduledEvents();

            // this is just an example of moving the bullets
            // TODO: actually implement this inside of MainContent
            // and remove all bullets that collide or go off screen
            foreach (BulletModel gb in MainContent.GoodBulletList) {
                gb.Move(Direction.Stay, Direction.Up);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        //  / <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            // player 
            spriteBatch.Draw(MainContent.PlayerShip.Sprite, new Rectangle(MainContent.PlayerShip.DrawingLocation, MainContent.PlayerShip.Dimensions), Color.White);

            // draw special enemies
            spriteBatch.Draw(MainContent.MidBoss.Sprite,
                MainContent.MidBoss.Location.ToVector2(),
                new Rectangle(0, 0, MainContent.MidBoss.Dimensions.X, MainContent.MidBoss.Dimensions.Y),
                Color.White,
                MainContent.MidBoss.Rotation,
                MainContent.MidBoss.Center.ToVector2(),
                MainContent.MidBoss.Scale,
                SpriteEffects.None,
                1.0f);

            // draw each enemy
            foreach (EnemyModel enemy in MainContent.EnemyShipList) {
                spriteBatch.Draw(enemy.Sprite, enemy.DrawingLocation.ToVector2(), new Rectangle(0, 0, enemy.Dimensions.X, enemy.Dimensions.Y), Color.White, enemy.Rotation, new Point(0, 0).ToVector2(), enemy.Scale, SpriteEffects.None, 1.0f);
            }

            foreach (BulletModel gb in MainContent.GoodBulletList) {
                spriteBatch.Draw(gb.Sprite, gb.DrawingLocation.ToVector2(), new Rectangle(0, 0, gb.Dimensions.X, gb.Dimensions.Y), Color.White, gb.Rotation, new Point(0, 0).ToVector2(), gb.Scale, SpriteEffects.None, 1.0f);
            }

            spriteBatch.DrawString(font, "Time Elapsed " + MainContent.Events.TimeElapsed(), new Vector2(50, 450), Color.Black);
            spriteBatch.DrawString(font, "ship location: X " + MainContent.PlayerShip.Location.X + " Y " + MainContent.PlayerShip.Location.Y, new Vector2(50, 400), Color.Black);


            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
