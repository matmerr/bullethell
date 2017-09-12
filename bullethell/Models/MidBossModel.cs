﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace bullethell.Models {
    class MidBossModel : EnemyModel {
        private int health;

        public MidBossModel(int startX, int startY, int dimensionX, int dimensionY, double startRate, Texture2D startSprite) : base(startX, startY, dimensionX, dimensionY, startRate, startSprite) {
        }
    }
}