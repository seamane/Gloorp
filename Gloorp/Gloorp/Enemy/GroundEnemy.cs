using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;

namespace Gloorp
{
    class GroundEnemy : Enemy
    {
        //position of  "eye" is (60,12) in texture space
        const float visionDepth = 110;
        const float viewRadius = (float)Math.PI / 12.0f;//this equates to 15 degrees
        Vector2 leftEye = new Vector2(60, 16);
        Vector2 rightEye = new Vector2(36, 16);
        Vector2 leftLookAt;
        Vector2 rightLookAt;

        Vector2 playerPos;
        Vector2 sourcePoint;

        public GroundEnemy(Vector2 anchor, float speed, int radius) : base(anchor)
        {
            this.speed = speed;
            patrollingRadius = radius;
            leftLookAt = new Vector2(-60, 35);
            leftLookAt.Normalize();
            rightLookAt = new Vector2(60, 35);
            rightLookAt.Normalize();
        }

        public override bool CanSeePlayer(Player player)
        {
            float distToPlayer;// = Math.Abs(player.sprite.position.X - sprite.position.X);
            ////is enemy facing to the right?
            //if (direction == 1)
            //{
            //    //is player to the right of the enemy and in range
            //    return distToPlayer <= visionDepth;
            //}

            ////is player to the left of the enemy and in range
            //return distToPlayer <= visionDepth;
            
            Vector2 toPlayer;
            float angleBetweenEnemyAndPlayer;

            //is enemy facing to the right?
            if (direction == 1)
            {
                playerPos = (player.sprite.position);
                sourcePoint = sprite.position + rightEye;

                toPlayer = player.sprite.position - (sprite.position + rightEye);
                distToPlayer = toPlayer.Length();
                if (distToPlayer > visionDepth)
                {
                    return false;
                }
                toPlayer.Normalize();
                angleBetweenEnemyAndPlayer = (float)Math.Acos(Vector2.Dot(toPlayer, rightLookAt));
            }
            else
            {
                playerPos = (player.sprite.position);
                sourcePoint = sprite.position + leftEye;

                toPlayer = player.sprite.position - (sprite.position + leftEye);
                distToPlayer = toPlayer.Length();
                if(distToPlayer > visionDepth)
                {
                    return false;
                }
                toPlayer.Normalize();
                angleBetweenEnemyAndPlayer = (float)Math.Acos(Vector2.Dot(toPlayer, leftLookAt));
            }

            //Debug.WriteLine("Angle: " + (angleBetweenEnemyAndPlayer * 180 / Math.PI));
            Debug.WriteLine("dist: " + distToPlayer);

            if (Math.Abs(angleBetweenEnemyAndPlayer) <= viewRadius && distToPlayer <= visionDepth)
            {
                return true;
            }

            return Math.Abs(angleBetweenEnemyAndPlayer) <= viewRadius && distToPlayer <= visionDepth;
        }

        public override void Draw(SpriteBatch batch)
        {
            //    DrawLine(batch, //draw line
            //    new Vector2(200, 200), //start of line
            //    new Vector2(100, 50) //end of line

            
            // calculate angle to rotate line
            //float angle =
            //    (float)Math.Atan2((playerPos - sourcePoint).Y, (playerPos - sourcePoint).X);
            //batch.Draw(sprite.texture,
            //new Rectangle(// rectangle defines shape of line and position of start of line
            //    (int)sourcePoint.X,
            //    (int)sourcePoint.Y,
            //    (int)(playerPos - sourcePoint).Length(), //sb will strech the texture to fill this rectangle
            //    2), //width of line, change this to make thicker line
            //null,
            //Color.Red, //colour of line
            //angle,     //angle of line (calulated above)
            //new Vector2(0, 0), // point in line about which to rotate
            //SpriteEffects.None,
            //0);

            if (direction == -1)
            {
                batch.Draw(sprite.texture, sprite.position, Color.White);
            }
            else
            {
                batch.Draw(sprite.texture, sprite.position, null, Color.White, 0.0f, new Vector2(0,0), 1.0f, SpriteEffects.FlipHorizontally, 0.0f);
            }
        }
    }
}
