using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace bullethell.Models {
    public class EnemyModel : BaseModel {
        private double health;

        public EnemyModel(int startX, int startY, double startRate, Texture2D startTexture) : base(startX, startY, startRate, startTexture) {
            SetHealth(1);
            name = "Regular Enemy";
            damage = 2;
        }

        public EnemyModel(Point start, double startRate, Texture2D startTexture) : base(start, startRate, startTexture) {
            SetHealth(1);
            name = "Regular Enemy";
        }

        public void SetHealth(int h) {
            health = h;
        }

        public double Health => health;

        public void TakeDamage(BaseModel b) {
            health -= b.Damage;
        }

        public bool IsDead() {
            if (health > 0) {
                return false;
            }
            return true;
        }
    }
}
