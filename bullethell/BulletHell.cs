using bullethell.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using bullethell.Controller;

namespace bullethell {
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class BulletHell : Game {
        // Menu Items
        private MenuButton startButton;


        private KeyboardState oldKeyboardState;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private GameContent MainContent;
        private SpriteFont font;

        public enum GameStates {
            Menu,
            InGame,
            Paused
        }

        private GameStates gameState;

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
            IsMouseVisible = true;
            gameState = GameStates.Menu;
            graphics.PreferredBackBufferWidth = 500;
            graphics.PreferredBackBufferHeight = 800;
            graphics.ApplyChanges();
            base.Initialize(); base.Initialize();
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
                BadBullet: Content.Load<Texture2D>("badMissile"),
                GoodBullet: Content.Load<Texture2D>("goodMissile"),
                MainBoss: Content.Load<Texture2D>("galaga_mainboss"),
                BaddieDie1: Content.Load<Texture2D>("baddieDie-1")

            );

            startButton = new MenuButton("start", 200, 200, Content.Load<Texture2D>("startButton"));


            MainContent.SetWindowDimensions(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            MainContent.InitializeModels();
            MainContent.InitializeEvents();
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



            int direction = 0;
            /*
            Right: 1
            Up: 2
            Left: 4
            Down: 8*/



            // MOVE PLAYER
            // We can use the Direction class that I made to avoid confusion
            if (Keyboard.GetState().IsKeyDown(Keys.Right)) {
                direction += 1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up)) {
                direction += 2;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left)) {
                direction += 4;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down)) {
                direction += 8;
            }

            if (direction != 0) MainContent.PlayerShip.Move(Direction.ConvertKeyDirection(direction));

            // TOGGLE SPEED
            // we have to check the key is down, and not just being spammed
            if (oldKeyboardState.IsKeyUp(Keys.LeftShift) && newKeyboardState.IsKeyDown(Keys.LeftShift)) {
                MainContent.PlayerShip.ToggleRate(5);
            }

            if (oldKeyboardState.IsKeyUp(Keys.Space) && newKeyboardState.IsKeyDown(Keys.Space)) {
                MainContent.AddGoodBullet(MainContent.PlayerShip.Location, 2);
            }

            //JUST TESTING FOR ENEMY BULLETS:
            if (oldKeyboardState.IsKeyUp(Keys.A) && newKeyboardState.IsKeyDown(Keys.A)) {
                MainContent.AddEnemyBullet(MainContent.MidBoss.Location, 2);
            }

            // update the keyboard state
            oldKeyboardState = newKeyboardState;


            if (gameState == GameStates.InGame) {
                // this method calls all scheduled events in the game timeline
                MainContent.Events.ExecuteScheduledEvents(); //Jomar's Comment: Much like the propertieschanged{} back in 321, super lit
            }

            // this is just an example of moving the good bullets

            foreach (BulletModel gb in MainContent.GoodBulletList) {
                gb.Move(Direction.N);
            }

            // Here we handle mouse click logic
            MouseState mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed) {
                if (startButton.ClickedWithinBounds(mouseState) && gameState == GameStates.Menu) {
                    gameState = GameStates.InGame;
                    MainContent.Start();
                }
            }






            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        //  / <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            if (gameState == GameStates.Menu) {

                spriteBatch.Draw(startButton.Texture, startButton.Location.ToVector2(), Color.White);

            } else if (gameState == GameStates.InGame) {

                // player 
                spriteBatch.Draw(MainContent.PlayerShip.Texture, new Rectangle(MainContent.PlayerShip.DrawingLocation, MainContent.PlayerShip.Texture.Bounds.Size), Color.White);



                // draw each enemy
                foreach (EnemyModel enemy in MainContent.EnemyShipList) {
                    spriteBatch.Draw(enemy.Texture, enemy.DrawingLocationVector, new Rectangle(0, 0, enemy.Texture.Width, enemy.Texture.Height), Color.White, enemy.Rotation, new Point(0, 0).ToVector2(), enemy.Scale, SpriteEffects.None, 1.0f);
                }

                foreach (BulletModel gb in MainContent.GoodBulletList) {
                    spriteBatch.Draw(gb.Texture, gb.DrawingLocationVector, new Rectangle(0, 0, gb.Texture.Width, gb.Texture.Height), Color.White, gb.Rotation, new Point(0, 0).ToVector2(), gb.Scale, SpriteEffects.None, 1.0f);
                }

                foreach (BaseModel bm in MainContent.MiscModelList) {
                    spriteBatch.Draw(bm.Texture, bm.DrawingLocationVector, new Rectangle(0, 0, bm.Texture.Height, bm.Texture.Height), Color.White, bm.Rotation, new Point(0, 0).ToVector2(), bm.Scale, SpriteEffects.None, 1.0f);

                }

                foreach (BulletModel enemyBullet in MainContent.EnemyBulletList.ToList()) {

                    // "Check each enemy bullet to see if it collides with a good bullet"
                    foreach (BulletModel goodBullet in MainContent.GoodBulletList.ToList()) {
                        if (MainContent.IsColliding(enemyBullet, goodBullet)) {
                            MainContent.GoodBulletList.Remove(goodBullet);
                            MainContent.EnemyBulletList.Remove(enemyBullet);
                            // draw explosions lol
                            double currTime = MainContent.Events.TimeElapsed();
                            Point collisionPoint = MainContent.CollisionPoint(enemyBullet, goodBullet);
                            BaseModel explosion1 = (BaseModel)MainContent.TimeToLive(currTime, currTime + .2,
                                new BaseModel(collisionPoint.X, collisionPoint.Y, 1, MainContent.BaddieDie1Texture));
                            MainContent.Events.AddScheduledEvent(currTime, currTime + .2, () => explosion1.Move(Direction.Stay, Direction.Stay));
                        }
                    }

                    // "Check the enemey bullet to see if it is colliding with player"
                    if (MainContent.IsColliding(enemyBullet, MainContent.PlayerShip)) {
                        MainContent.PlayerShip.TakeDamage();
                        MainContent.EnemyBulletList.Remove(enemyBullet);
                        Point collisionPoint = MainContent.CollisionPoint(enemyBullet, MainContent.PlayerShip);
                        double currTime = MainContent.Events.TimeElapsed();
                        BaseModel explosion1 = (BaseModel)MainContent.TimeToLive(currTime, currTime + .2,
                            new BaseModel(collisionPoint.X, collisionPoint.Y, 1, MainContent.BaddieDie1Texture));
                        MainContent.Events.AddScheduledEvent(currTime, currTime + .2, () => explosion1.Move(Direction.Stay, Direction.Stay));
                    } else {
                        spriteBatch.Draw(enemyBullet.Texture, enemyBullet.DrawingLocationVector,
                            new Rectangle(0, 0, enemyBullet.Texture.Width, enemyBullet.Texture.Height), Color.White,
                            enemyBullet.Rotation,
                            new Point(0, 0).ToVector2(), enemyBullet.Scale, SpriteEffects.None, 1.0f);
                    }

                }

                spriteBatch.DrawString(font, "Health: " + MainContent.PlayerShip.Health, new Vector2(25, 650), Color.White);
                spriteBatch.DrawString(font, "Time Elapsed " + MainContent.Events.TimeElapsed(), new Vector2(25, 750), Color.White);
                spriteBatch.DrawString(font, "ship location: X " + MainContent.PlayerShip.DrawingLocation.X + " Y " + MainContent.PlayerShip.DrawingLocation.Y, new Vector2(25, 700), Color.White);

            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
