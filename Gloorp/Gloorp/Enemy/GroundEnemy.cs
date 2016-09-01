using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace Gloorp
{
    class GroundEnemy : Enemy
    {
        const float visionDepth = 75;
        public GroundEnemy(Vector2 anchor) : base(anchor)
        {
            speed = 3;
            patrollingRadius = 550;
        }

        public override bool CanSeePlayer(Player player)
        {
            float distToPlayer = Math.Abs(player.sprite.position.X - sprite.position.X);
            //is enemy facing to the right?
            if (direction == 1)
            {
                //is player to the right of the enemy and in range
                return distToPlayer <= visionDepth;
            }
            
            //is player to the left of the enemy and in range
            return distToPlayer <= visionDepth;
        }

        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
        }
    }
}
