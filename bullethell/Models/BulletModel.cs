using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace bullethell.Models {
    public class BulletModel : BaseModel {
        public BulletModel(int startX, int startY, double startRate, Texture2D startTexture) : base(startX, startY, startRate, startTexture) {
            name = "Bullet";
        }
    }
}
