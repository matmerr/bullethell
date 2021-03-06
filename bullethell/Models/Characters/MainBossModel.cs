﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace bullethell.Models {
    public class MainBossModel : EnemyModel {

        public MainBossModel(int startX, int startY, double startRate, Texture2D startTexture) : base(startX, startY, startRate, startTexture) {
            SetHealth(10);
            name = "Main Boss";
        }

        public MainBossModel(Point start, double startRate, Texture2D startTexture) : base(start, startRate, startTexture) {
            SetHealth(10);
            name = "Main Boss";
        }
    }
}
