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
        private EnemyModel enemyShip3;
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
            playerShip = new PlayerModel(100, 100, 32, 32, 2, Content.Load<Texture2D>("ship"));
            enemyShip = new EnemyModel(600, 400, 32, 32, 2, Content.Load<Texture2D>("baddie1-A"));
            enemyShip2 = new EnemyModel(300, 300, 32, 32, 2, 250, 250, Content.Load<Texture2D>("baddie1-A"));
            enemyShip3 = new EnemyModel(100, 100, 32, 32, 1, Content.Load<Texture2D>("baddie1-A"));
            enemyShip3 = new EnemyModel(100, 100, 32, 32, 1, Content.Load<Texture2D>("baddie1-A"));
            middleBoss = new MidBossModel(300, 100, 50, 50, 2, Content.Load<Texture2D>("midBoss"));
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

            // MOVE A SHIP IN AN ANGLE BASED ON THE UNIT CIRCLE (IN DEGREES)
            enemyShip.Move(150);

            // MOVE SHIP IN AN ORBIT
            enemyShip2.MoveOrbit();
            enemyShip2.Rotate(.1);

            // ROTATE A SHIP IN ONE DIRECTION
            enemyShip3.Move(310);
            enemyShip3.Rotate(-.1);

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

            // player 
            spriteBatch.Draw(playerShip.Sprite, new Rectangle(playerShip.Location, playerShip.Dimensions), Color.White);

            // non rotating
            spriteBatch.Draw(enemyShip.Sprite, new Rectangle(enemyShip.Location, enemyShip.Dimensions), Color.White);

            // rotating
            spriteBatch.Draw(enemyShip2.Sprite, enemyShip2.Location.ToVector2(), new Rectangle(0, 0, enemyShip2.Dimensions.X, enemyShip2.Dimensions.Y), Color.White, enemyShip2.Rotation, enemyShip2.Origin.ToVector2(), enemyShip.Scale, SpriteEffects.None, 1.0f);
            spriteBatch.Draw(enemyShip3.Sprite, enemyShip3.Location.ToVector2(), new Rectangle(0, 0, enemyShip3.Dimensions.X, enemyShip3.Dimensions.Y), Color.White, enemyShip3.Rotation, enemyShip3.Origin.ToVector2(), enemyShip.Scale, SpriteEffects.None, 1.0f);

            // MIDDLE BOSS
            spriteBatch.Draw(middleBoss.Sprite, new Rectangle(middleBoss.Location.X, middleBoss.Location.Y, 50, 50), Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
