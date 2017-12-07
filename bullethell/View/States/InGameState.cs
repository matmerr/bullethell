using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using bullethell.Controller;
using bullethell.Models;
using bullethell.Models.Firing.FiringPatterns;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace bullethell.View
{
    class InGameState : AbstractGameState
    {

        private GameContent MainContent;
        private SpriteFont font;


        public InGameState(GraphicsDevice graphicsDevice, ContentManager Content, ref Stack<AbstractGameState> Screens) : base(graphicsDevice, Content, ref Screens)
        {
            LoadContent();
        }


        public override void Initialize()
        {
            throw new NotImplementedException();
        }

        public override void LoadContent()
        {
            font = Content.Load<SpriteFont>("HUD");

            Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D> {
                { TextureNames.PlayerShip, Content.Load<Texture2D>("ship") },
                { TextureNames.Baddie1A,Content.Load<Texture2D>("baddie1-A") },
                { TextureNames.Baddie1B, Content.Load<Texture2D>("baddie1-B") },
                { TextureNames.Baddie2A, Content.Load<Texture2D>("baddie2-A") },
                { TextureNames.Baddie2B, Content.Load<Texture2D>("baddie2-B") },
                { TextureNames.MidBoss, Content.Load<Texture2D>("midBoss") },
                { TextureNames.EnemyBullet, Content.Load<Texture2D>("badMissile") },
                { TextureNames.GoodBullet, Content.Load<Texture2D>("goodMissile") },
                { TextureNames.MainBoss, Content.Load<Texture2D>("galaga_mainboss") },
                { TextureNames.BaddieDie1, Content.Load<Texture2D>("baddieDie-1") },
                { TextureNames.BaddieDie2, Content.Load<Texture2D>("baddieDie-2") },
                { TextureNames.BaddieDie3, Content.Load<Texture2D>("baddieDie-3") },
                { TextureNames.BaddieDie4, Content.Load<Texture2D>("baddieDie-4") },
                { TextureNames.BaddieDie5, Content.Load<Texture2D>("baddieDie-5") },
                { TextureNames.LaserBullet, Content.Load<Texture2D>("laser") },
                { TextureNames.Invisible, Content.Load<Texture2D>("invisible") },
            };


            MainContent = new GameContent(textures);

            MainContent.InitializeModels(GetWindowBounds());
            if (MainContent.InitializeEvents(GetWindowBounds()) == false)
            {
                MainContent = null;
            }
            else
            {
                MainContent.Start();
            }

        }


        public override void UnloadContent()
        {
            // unload any extra content
        }

        public override void Update(GameTime gameTime)
        {
            if (MainContent == null)
            {
                Screens.Pop();
                return;
            }
            NewKeyboardState = Keyboard.GetState();

            int direction = 0;
            /*
            Right: 1
            Up: 2
            Left: 4
            Down: 8*/


            // MOVE PLAYER
            // We can use the Direction class that I made to avoid confusion
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                direction += 1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                direction += 2;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                direction += 4;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                direction += 8;
            }

            if (direction != 0) MainContent.PlayerShip.Move(Direction.ConvertKeyDirection(direction));

            // TOGGLE SPEED
            // we have to check the key is down, and not just being spammed
            if (OldKeyboardState.IsKeyUp(Keys.LeftShift) && NewKeyboardState.IsKeyDown(Keys.LeftShift))
            {
                MainContent.PlayerShip.ToggleRate(2);
            }

            // fire a bullet
            if (OldKeyboardState.IsKeyUp(Keys.Space) && NewKeyboardState.IsKeyDown(Keys.Space))
            {
                MainContent.FiringController.From(MainContent.PlayerShip).Between(MainContent.Events.TimeElapsed(),
                    MainContent.Events.TimeElapsed() + .1).Pattern(new SingleBulletFiringPattern());
                Stats.BulletFired();
            }
            if (OldKeyboardState.IsKeyUp(Keys.Escape) && NewKeyboardState.IsKeyDown(Keys.Escape))
            {
                Screens.Pop();
                Screens.Peek().OldKeyboardState = this.NewKeyboardState;
            }

            if (OldKeyboardState.IsKeyUp(Keys.Q) && NewKeyboardState.IsKeyDown(Keys.Q)) {
                MainContent.ManualInvincibility();
            }

            foreach (BulletModel gb in MainContent.GoodBulletList)
            {
                gb.Move(Direction.Stay, Direction.Up);
            }

            // update the keyboard state
            OldKeyboardState = NewKeyboardState;

            MainContent.Events.ExecuteScheduledEvents();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (MainContent == null)
            {
                Screens.Pop();
                return;
            }
            graphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            // player 
            spriteBatch.Draw(MainContent.PlayerShip.Texture,
                new Rectangle(MainContent.PlayerShip.Location, MainContent.PlayerShip.Texture.Bounds.Size),
                Color.White);


            // draw each enemy
            foreach (EnemyModel enemy in MainContent.EnemyShipList.ToList())
            {
                if (MainContent.RemoveIfOffScreen(enemy) == false)
                {
                    if (MainContent.IsColliding(enemy, MainContent.PlayerShip))
                    {
                        MainContent.PlayerShip.TakeDamage(enemy);
                        enemy.TakeDamage(MainContent.PlayerShip);
                        if (enemy.IsDead())
                        {
                            Stats.EnemyDestroyed();
                            MainContent.RemoveEnemy(enemy);
                        }
                        MainContent.DrawTinyExplosion(MainContent.CollisionPoint(enemy, MainContent.PlayerShip));
                    }
                    // "Check each enemy bullet to see if it collides with a good bullet"

                    foreach (BulletModel goodBullet in MainContent.GoodBulletList.ToList()) {
                        if (MainContent.IsColliding(enemy, goodBullet)) {
                            Stats.AddPoints(5);
                            MainContent.GoodBulletList.Remove(goodBullet);
                            MainContent.DrawTinyExplosion(MainContent.CollisionPoint(enemy, goodBullet));
                            enemy.TakeDamage(goodBullet);
                            if (enemy.IsDead())
                            {
                                Stats.EnemyDestroyed();
                                MainContent.RemoveEnemy(enemy);
                            }
                            MainContent.Events.RemoveFutureTaggedEvents(enemy);
                        }
                    }

                    spriteBatch.Draw(enemy.Texture, enemy.DrawingLocationVector,
                        new Rectangle(0, 0, enemy.Texture.Height, enemy.Texture.Height), Color.White, enemy.Rotation,
                        new Point(0, 0).ToVector2(), enemy.Scale, SpriteEffects.None, 1.0f);
                }

            }


            foreach (BulletModel gb in MainContent.GoodBulletList)
            {
                spriteBatch.Draw(gb.Texture, gb.DrawingLocationVector,
                    new Rectangle(0, 0, gb.Texture.Width, gb.Texture.Height), Color.White, gb.Rotation,
                    new Point(0, 0).ToVector2(), gb.Scale, SpriteEffects.None, 1.0f);
            }

            foreach (BaseModel bm in MainContent.MiscModelList)
            {
                spriteBatch.Draw(bm.Texture, bm.DrawingLocationVector,
                    new Rectangle(0, 0, bm.Texture.Height, bm.Texture.Height), Color.White, bm.Rotation,
                    new Point(0, 0).ToVector2(), bm.Scale, SpriteEffects.None, 1.0f);

            }

            foreach (BulletModel enemyBullet in MainContent.EnemyBulletList.ToList())
            {

                // "Check each enemy bullet to see if it collides with a good bullet"

                foreach (BulletModel goodBullet in MainContent.GoodBulletList.ToList()) {
                    if (MainContent.IsColliding(enemyBullet, goodBullet)) {
                        Stats.AddPoints(1);

                        MainContent.GoodBulletList.Remove(goodBullet);
                        MainContent.RemoveBullet(enemyBullet);
                        MainContent.Events.RemoveFutureTaggedEvents(enemyBullet);
                        // draw explosions lol
                        MainContent.DrawTinyExplosion(MainContent.CollisionPoint(enemyBullet, goodBullet));
                    }
                }

                // "Check the enemey bullet to see if it is colliding with player"
                if (MainContent.IsColliding(enemyBullet, MainContent.PlayerShip))
                {
                    MainContent.PlayerShip.TakeDamage(enemyBullet);
                    MainContent.RemoveBullet(enemyBullet);
                    MainContent.Events.RemoveFutureTaggedEvents(enemyBullet);
                    Point collisionPoint = MainContent.CollisionPoint(enemyBullet, MainContent.PlayerShip);
                    MainContent.DrawTinyExplosion(collisionPoint);
                }
                else
                {
                    spriteBatch.Draw(enemyBullet.Texture, enemyBullet.DrawingLocationVector,
                        new Rectangle(0, 0, enemyBullet.Texture.Width, enemyBullet.Texture.Height), Color.White,
                        enemyBullet.Rotation,
                        new Point(0, 0).ToVector2(), enemyBullet.Scale, SpriteEffects.None, 1.0f);
                }

            }
            int j = 600;
            foreach (EnemyModel en in MainContent.EnemyShipList)
            {
                if (j < 300)
                {
                    break;
                }
                spriteBatch.DrawString(font, en.Name + " Health: " + en.Health, new Vector2(25, j),
                    Color.White);
                j -= 25;
            }
            spriteBatch.DrawString(font, "Score: " + Stats.GetPoints(), new Vector2(25, 650),
                Color.Aqua);
            spriteBatch.DrawString(font, "Health: " + MainContent.PlayerShip.Health, new Vector2(25, 675),
                Color.Aqua);
            spriteBatch.DrawString(font, "Lives Remaining: " + MainContent.PlayerShip.Lives, new Vector2(25, 700),
                Color.DeepPink);
            spriteBatch.DrawString(font, "Time Elapsed: " + MainContent.Events.TimeElapsed(), new Vector2(25, 750),
                Color.White);


            if (MainContent.PlayerShip.IsInvincible)
            {
                spriteBatch.DrawString(font, "TEMPORARILY INVINCIBLE", new Vector2(25, 775), Color.Red);
            }


            if (MainContent.PlayerShip.IsDead())
            {
                MainContent.PlayerShip.Lives -= 1;
                if (MainContent.PlayerShip.Lives == 0)
                {
                    EndGameLostState es = new EndGameLostState(graphicsDevice, Content, ref Screens);
                    MainContent.Events.StopTimer();
                    Screens.Push(es);
                }
                else
                {
                    MainContent.PlayerShip.Respawn();
                    MainContent.EnablePlayerInvincibility();
                }
            }

            if (MainContent.HasWon())
            {
                EndGameWonState es = new EndGameWonState(graphicsDevice, Content, ref Screens);
                MainContent.Events.StopTimer();
                Screens.Push(es);
            }


            spriteBatch.End();
        }
        public override StatsModel GetStats()
        {
            return Stats;
        }

        public override Rectangle GetWindowBounds()
        {
            return new Rectangle(graphicsDevice.Viewport.X, graphicsDevice.Viewport.Y, graphicsDevice.Viewport.Width,
                graphicsDevice.Viewport.Height);
        }
    }
}
