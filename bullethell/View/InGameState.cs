﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bullethell.Controller;
using bullethell.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace bullethell.View {
    class InGameState : GameState {

        private GameContent MainContent;
        private KeyboardState oldKeyboardState;
        private SpriteFont font;


        public InGameState(GraphicsDevice graphicsDevice, ContentManager Content, Stack<GameState> Screens) : base(graphicsDevice, Content, Screens) {
            LoadContent();
        }


        public override void Initialize() {
            throw new NotImplementedException();
        }

        public override void LoadContent() {
            font = Content.Load<SpriteFont>("HUD");


            MainContent = new GameContent(
                PlayerShip: Content.Load<Texture2D>("ship"),
                Baddie1A: Content.Load<Texture2D>("baddie1-A"),
                Baddie1B: Content.Load<Texture2D>("baddie1-B"),
                Baddie2A: Content.Load<Texture2D>("baddie2-B"),
                Baddie2B: Content.Load<Texture2D>("baddie2-B"),
                MiddleBoss: Content.Load<Texture2D>("midBoss"),
                BadBullet: Content.Load<Texture2D>("badMissile"),
                GoodBullet: Content.Load<Texture2D>("goodMissile"),
                MainBoss: Content.Load<Texture2D>("galaga_mainboss"),
                BaddieDie1: Content.Load<Texture2D>("baddieDie-1"),
                BaddieDie2: Content.Load<Texture2D>("baddieDie-2"),
                BaddieDie3: Content.Load<Texture2D>("baddieDie-3"),
                BaddieDie4: Content.Load<Texture2D>("baddieDie-4"),
                BaddieDie5: Content.Load<Texture2D>("baddieDie-5")
            );

            MainContent.InitializeModels();
            MainContent.InitializeEvents();

            MainContent.Start();
        }


        public override void UnloadContent() {
            // unload any extra content
        }

        public override void Update(GameTime gameTime) {
            KeyboardState newKeyboardState = Keyboard.GetState();

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


            foreach (BulletModel gb in MainContent.GoodBulletList) {
                gb.Move(Direction.Stay, Direction.Up);
            }

            // update the keyboard state
            oldKeyboardState = newKeyboardState;

            MainContent.Events.ExecuteScheduledEvents();

        }

        public override void Draw(SpriteBatch spriteBatch) {
            graphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            // player 
            spriteBatch.Draw(MainContent.PlayerShip.Texture,
                new Rectangle(MainContent.PlayerShip.DrawingLocation, MainContent.PlayerShip.Texture.Bounds.Size),
                Color.White);





            // draw each enemy
            foreach (EnemyModel enemy in MainContent.EnemyShipList.ToList()) {

                // "Check each enemy bullet to see if it collides with a good bullet"
                foreach (BulletModel goodBullet in MainContent.GoodBulletList.ToList()) {
                    if (MainContent.IsColliding(enemy, goodBullet)) {
                        MainContent.GoodBulletList.Remove(goodBullet);
                        MainContent.EnemyShipList.Remove(enemy);
                        // draw explosions lol
                        MainContent.DrawMediumExplosion(enemy.Location);
                        MainContent.Events.RemoveTaggedEvents(enemy);
                    }

                }

                spriteBatch.Draw(enemy.Texture, enemy.DrawingLocationVector,
                    new Rectangle(0, 0, enemy.Texture.Height, enemy.Texture.Height), Color.White, enemy.Rotation,
                    new Point(0, 0).ToVector2(), enemy.Scale, SpriteEffects.None, 1.0f);

            }


            foreach (BulletModel gb in MainContent.GoodBulletList) {
                spriteBatch.Draw(gb.Texture, gb.DrawingLocationVector,
                    new Rectangle(0, 0, gb.Texture.Width, gb.Texture.Height), Color.White, gb.Rotation,
                    new Point(0, 0).ToVector2(), gb.Scale, SpriteEffects.None, 1.0f);
            }

            foreach (BaseModel bm in MainContent.MiscModelList) {
                spriteBatch.Draw(bm.Texture, bm.DrawingLocationVector,
                    new Rectangle(0, 0, bm.Texture.Height, bm.Texture.Height), Color.White, bm.Rotation,
                    new Point(0, 0).ToVector2(), bm.Scale, SpriteEffects.None, 1.0f);

            }

            foreach (BulletModel enemyBullet in MainContent.EnemyBulletList.ToList()) {

                // "Check each enemy bullet to see if it collides with a good bullet"
                foreach (BulletModel goodBullet in MainContent.GoodBulletList.ToList()) {
                    if (MainContent.IsColliding(enemyBullet, goodBullet)) {
                        MainContent.GoodBulletList.Remove(goodBullet);
                        MainContent.EnemyBulletList.Remove(enemyBullet);
                        // draw explosions lol
                        MainContent.DrawTinyExplosion(MainContent.CollisionPoint(enemyBullet, goodBullet));
                    }
                }

                // "Check the enemey bullet to see if it is colliding with player"
                if (MainContent.IsColliding(enemyBullet, MainContent.PlayerShip)) {
                    MainContent.PlayerShip.TakeDamage();
                    MainContent.EnemyBulletList.Remove(enemyBullet);
                    Point collisionPoint = MainContent.CollisionPoint(enemyBullet, MainContent.PlayerShip);
                    MainContent.DrawTinyExplosion(collisionPoint);
                } else {
                    spriteBatch.Draw(enemyBullet.Texture, enemyBullet.DrawingLocationVector,
                        new Rectangle(0, 0, enemyBullet.Texture.Width, enemyBullet.Texture.Height), Color.White,
                        enemyBullet.Rotation,
                        new Point(0, 0).ToVector2(), enemyBullet.Scale, SpriteEffects.None, 1.0f);
                }
            }

            spriteBatch.DrawString(font, "Remaining Enemies: " + MainContent.EnemyShipList.Count, new Vector2(25, 550),
                Color.White);

            spriteBatch.DrawString(font, "Health: " + MainContent.PlayerShip.Health, new Vector2(25, 650),
                Color.White);
            spriteBatch.DrawString(font, "Time Elapsed " + MainContent.Events.TimeElapsed(), new Vector2(25, 750),
                Color.White);
            spriteBatch.DrawString(font,
                "ship location: X " + MainContent.PlayerShip.DrawingLocation.X + " Y " +
                MainContent.PlayerShip.DrawingLocation.Y, new Vector2(25, 700), Color.White);


            if (MainContent.PlayerShip.IsDead()) {
                EndGameLostState es = new EndGameLostState(graphicsDevice, Content, Screens);
                MainContent.Events.StopTimer();
                Screens.Push(es);
            }

            if (MainContent.Events.TimeElapsed() > 120) {
                EndGameWonState es = new EndGameWonState(graphicsDevice, Content, Screens);
                MainContent.Events.StopTimer();
                Screens.Push(es);
            }


            spriteBatch.End();
        }




        public override Stack<GameState> GetScreens() {
            return Screens;
        }


    }
}
