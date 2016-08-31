using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace MyFirstMonoGame
{
    public enum EnemyState
    {
        Patrol,//moving back and forth
        Scanning,//standing still but rotating
        PlayerFound//can see the enemy
    }
    class Enemy
    {
        public Sprite sprite;
        private Vector2 anchor;
        int speed;
        int direction;//either -1 or 1; for now...
        int angleDirection;
        EnemyState currState;
        float angle = 0;//in RADIANS
        const float maxRotation = (float)Math.PI / 9.0f;//this equates to 15 degrees
        const int patrollingRadius = 400;

        public Enemy(Vector2 anchor)
        {
            this.anchor = anchor;
            sprite = new Sprite();
            sprite.position = anchor;
            speed = 2;
            direction = 1;
            angleDirection = 1;
            currState = EnemyState.Patrol;
        }

        public void UpdateMovement()
        {
            switch(currState)
            {
                case EnemyState.Patrol:
                    if (Math.Abs(anchor.X - sprite.position.X) >= patrollingRadius)
                    {
                        direction *= -1;
                    }
                    if(angle < -maxRotation || angle > maxRotation)
                    {
                        angleDirection *= -1;
                    }

                    sprite.position.X += direction * speed;
                    angle += 0.004f * angleDirection;
                    break;
                case EnemyState.Scanning:
                    break;
                case EnemyState.PlayerFound:
                    break;
            }
        }

        public void PlayerMoved(int amount)
        {
            anchor.X += amount;
            sprite.position.X += amount;
        }

        public bool CanSeePlayer(Player player)
        {
            Vector2 towardPlayer = player.sprite.position - sprite.position;// player - enemy position
            towardPlayer.Normalize();
            Vector2 enemyForwardVector = new Vector2((float)Math.Sin(-angle),(float)Math.Cos(-angle));

            float angleBetweenEnemyAndPlayer = (float)Math.Acos(Vector2.Dot(towardPlayer,enemyForwardVector));

            if(Math.Abs(angleBetweenEnemyAndPlayer) <= maxRotation)//can enemy see player?
            {
                return true;
            }

            return false;
        }

        public void Reset(float offset)
        {
            angle = 0.0f;
            anchor.X += offset;
            sprite.position = anchor;
            direction = 1;
            angleDirection = 1;
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Draw(sprite.texture, sprite.position, null, Color.White * 0.5f, angle, new Vector2(32,0), 12.0f, SpriteEffects.None, 0.0f);
        }
    }
}
