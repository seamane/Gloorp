using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Gloorp
{
    class AirEnemy : Enemy
    {
        const float viewRadius = (float)Math.PI / 9.0f;//this equates to 15 degrees
        Vector2 lookAt = new Vector2(0, 1);//direction the enemy is looking

        public AirEnemy(Vector2 anchor) : base(anchor)
        {
            speed = 3;
            patrollingRadius = 550;
        }

        public override bool CanSeePlayer(Player player)
        {
            // player - enemy position
            Vector2 towardPlayer = player.sprite.position - sprite.position;
            towardPlayer.Normalize();

            float angleBetweenEnemyAndPlayer = (float)Math.Acos(Vector2.Dot(towardPlayer, lookAt));

            //can enemy see player?
            if (Math.Abs(angleBetweenEnemyAndPlayer) <= viewRadius)
            {
                return true;
            }

            return false;
        }

        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
        }
    }
}
