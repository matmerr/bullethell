using bullethell.Models;
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
        private PlayerModel playerShip;
        private EnemyModel enemyShip;
        private EnemyModel enemyShip2;
        private MidBossModel middleBoss;

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

            // TODO: use this.Content to load your game content here
            playerShip = new PlayerModel(100, 100, 2, Content.Load<Texture2D>("ship"));
            enemyShip = new EnemyModel(200, 200, -1, 200, 250, Content.Load<Texture2D>("baddie1-A"));
            enemyShip2 = new EnemyModel(600, 400, 1, Content.Load<Texture2D>("baddie1-A"));
            middleBoss = new MidBossModel(100, -100, 2, Content.Load<Texture2D>("midBoss"));

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

            if (gameTime.TotalGameTime.TotalSeconds > 3) {
                middleBoss.MoveToPoint(300, 100, Direction.Down, Direction.Right);
            }

            // MOVE PLAYER
            // We can use the Direction class that I made to avoid confusion
            if (Keyboard.GetState().IsKeyDown(Keys.Right)) {
                playerShip.Move(Direction.Stay, Direction.Right);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left)) {
                playerShip.Move(Direction.Stay, Direction.Left);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up)) {
                playerShip.Move(Direction.Up, Direction.Stay);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down)) {
                playerShip.Move(Direction.Down, Direction.Stay);
            }

            // TOGGLE SPEED
            // we have to check the key is down, and not just being spammed
            if (oldKeyboardState.IsKeyUp(Keys.Space) && Keyboard.GetState().IsKeyDown(Keys.Space)) {
                playerShip.ToggleRate(5);
            }
            oldKeyboardState = newKeyboardState;

            // MOVE SHIP IN AN ORBIT
            enemyShip.MoveOrbit();

            // MOVE A SHIP IN AN ANGLE BASED ON THE UNIT CIRCLE (IN DEGREES)
            enemyShip2.Move(150);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(playerShip.Sprite, new Rectangle(playerShip.X, playerShip.Y, 32, 32), Color.White);
            spriteBatch.Draw(enemyShip.Sprite, new Rectangle(enemyShip.X, enemyShip.Y, 32, 32), Color.White);
            spriteBatch.Draw(enemyShip.Sprite, new Rectangle(enemyShip2.X, enemyShip2.Y, 32, 32), Color.White);
            spriteBatch.Draw(middleBoss.Sprite, new Rectangle(middleBoss.X, middleBoss.Y, 50, 50), Color.White);


            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
