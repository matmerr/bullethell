using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace bullethell.Models {
    public class PlayerModel : BaseModel {

        private int health;
        public int Lives;

        private int initX, initY;
        private bool isInvincible;
        


        public PlayerModel(int startX, int startY, double startRate, Texture2D startTexture) : base(startX, startY, startRate, startTexture) {
            health = 10;
            Lives = 3;
            initX = startX;
            initY = startY;
            isInvincible = false;
        }

        public int Health => health;

        public bool IsInvincible => isInvincible;

        public void TakeDamage() {
            if (!isInvincible) {
                health -= 1;
            }
        }

        public bool IsDead() {
            if (health > 0) {
                return false;
            }
            return true;
        }

        public void Respawn() {
            health = 10;
            location.X = initX;
            location.Y = initY;
            drawingLocation.X = location.X - texture.Width / 2;
            drawingLocation.Y = location.Y - texture.Height / 2;
        }

        public void ToggleInvincibility() {
            isInvincible = !isInvincible;
        }
    }

}
