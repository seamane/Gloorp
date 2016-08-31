using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace MyFirstMonoGame
{
    class EnemyManager
    {
        List<Enemy> enemies = new List<Enemy>();
        Player player;

        public EnemyManager(Player player)
        {
            this.player = player;
        }

        public void AddEnemy(Enemy enemy)
        {
            enemies.Add(enemy);
        }

        public void UpdateMovement()
        {
            foreach (Enemy e in enemies)
            {
                e.UpdateMovement();
            }
        }

        public void PlayerMoved(int amount)
        {
            foreach (Enemy e in enemies)
            {
                e.PlayerMoved(amount);
            }
        }

        public bool CanEnemySeePlayer()
        {
            if(player.mCurrentState == State.Disguised)
            {
                return false;
            }

            foreach(Enemy e in enemies)
            {
                if(e.CanSeePlayer(player))
                {
                    return true;
                }
            }
            return false;
        }

        public void ResetEnemies(float offset)
        {
            foreach(Enemy e in enemies)
            {
                e.Reset(offset);
            }
        }

        public void Draw(SpriteBatch batch)
        {
            foreach(Enemy e in enemies)
            {
                e.Draw(batch);
            }
        }
    }
}
