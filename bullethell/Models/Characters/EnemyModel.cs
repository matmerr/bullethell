﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace bullethell.Models {
    public class EnemyModel : BaseModel {
        private int health;

        public EnemyModel(int startX, int startY, double startRate, Texture2D startTexture) : base(startX, startY, startRate, startTexture) {
            SetHealth(1);
            name = "Regular Enemy";
        }

        public EnemyModel(Point start, double startRate, Texture2D startTexture) : base(start, startRate, startTexture) {
            SetHealth(1);
            name = "Regular Enemy";
        }

        public void SetHealth(int h) {
            health = h;
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