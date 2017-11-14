using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bullethell.Models {
    public class StatsModel {
        private int totalBulletsFired;
        private int totalEnemiesDestroyed;

        public int TotalBulletsFired => totalBulletsFired;
        public int TotalEnemiesDestroyed => totalEnemiesDestroyed;


        public StatsModel() {
            totalBulletsFired = 0;
            totalEnemiesDestroyed = 0;
        }

        public void BulletFired() {
            totalBulletsFired++;
        }

        public void EnemyDestroyed() {
            totalEnemiesDestroyed++;
        }

        public double GetBulletAccuracy() {

            if (totalBulletsFired > 0) {
                return totalEnemiesDestroyed / (double)totalBulletsFired;
            }
            return 0;
        }
    }
}
