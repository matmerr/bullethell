﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace bullethell.Models {
    class PlayerModel : BaseModel {

        private int health;
        private int score;


        public PlayerModel(int startX, int startY, int dimensionX, int dimensionY, double startRate, Texture2D startSprite) : base(startX, startY, dimensionX, dimensionY, startRate, startSprite) {
        }
    }

}