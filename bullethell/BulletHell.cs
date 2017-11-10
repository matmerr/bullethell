using bullethell.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using bullethell.Controller;
using bullethell.View;

namespace bullethell {
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class BulletHell : Game {
        // Menu Items





        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private GameStateManager StateManager;


        public enum GameStates {
            Menu,
            InGame,
            Paused,
            GameLost,
            GameWon
        }



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
            //gameState = GameStates.Menu;
            graphics.PreferredBackBufferWidth = 500;
            graphics.PreferredBackBufferHeight = 800;
            graphics.ApplyChanges();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);


            StateManager = new GameStateManager(Content);

            StateManager.AddScreen(new InGameState(GraphicsDevice, StateManager.GetMainContent(), StateManager.GetScreens()));

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
            StateManager.UnloadContent();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);


            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here



            /*
            if (gameState == GameStates.InGame) {
                // this method calls all scheduled events in the game timeline
                MainContent.Events.ExecuteScheduledEvents(); //Jomar's Comment: Much like the propertieschanged{} back in 321, super lit
            }

            // this is just an example of moving the good bullets

            foreach (BulletModel gb in MainContent.GoodBulletList) {
                gb.Move(Direction.N);
            }
            */
            /*
            // Here we handle mouse click logic
            MouseState mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed) {
                if (startButton.ClickedWithinBounds(mouseState) && gameState == GameStates.Menu) {
                    gameState = GameStates.InGame;
                    MainContent.Start();
                }
            }*/


            StateManager.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        //  / <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            StateManager.Draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
}
