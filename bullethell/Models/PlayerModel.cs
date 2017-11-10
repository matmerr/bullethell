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


        public PlayerModel(int startX, int startY, double startRate, Texture2D startTexture) : base(startX, startY, startRate, startTexture) {
            health = 20;
        }

        public int Health => health;

        public void TakeDamage() {
            health -= 1;
        }

        public bool IsDead() {
            if (health > 0) {
                return false;
            }
            return true;
        }

    }

}
