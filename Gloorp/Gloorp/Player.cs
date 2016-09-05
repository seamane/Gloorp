
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Input;

namespace Gloorp
{
    public enum State
    {
        Idle,
        Walking,//going side to side
        Jumping,//going up
        Falling,//going down
        Disguised,//is transformed
        Found//spotted by enemy
    }
    class Player
    {
        public State mCurrentState = State.Walking;
        public Vector2 initialPosition;
        public Sprite sprite;
        public Vector2 jumpStartPosition;
        public Sprite nearObject;
        public bool isInAir;

        public Player()
        {
            sprite = new Sprite();
            jumpStartPosition = Vector2.Zero;
            nearObject = null;
        }
        public Rectangle getRect()
        { return new Rectangle((int)sprite.position.X-32, (int)sprite.position.Y-64, 64, 64); }

        public void CheckWhenPlayerIsOnGround(Player player, PlatformManager platformManager, KeyboardState state)
        {
            if (player.mCurrentState == State.Walking || player.mCurrentState == State.Idle)
            {
                if (!platformManager.CheckPlatformPlayerCollision(player))// && player.sprite.position.Y > initialPosition.Y)
                {
                    player.isInAir = true;
                    player.mCurrentState = State.Falling;
                    //player.isInAir = true;
                }
                //if (player.sprite.position.Y!=playerInitialPosition)
                //{
                //    player.sprite.position.Y = playerInitialPosition;

                //}
                if (state.IsKeyDown(Keys.Space) == true)
                {
                    Jump(player);
                }

            }
        }

        private void Jump(Player player)
        {
            if (player.mCurrentState != State.Jumping)
            {
                player.mCurrentState = State.Jumping;
                player.isInAir = true;
                player.jumpStartPosition = player.sprite.position;
            }

        }

        public void CheckWhenPlayerIsNotOnGround(Player player, PlatformManager platformManager, KeyboardState state)
        {
            if (player.mCurrentState == State.Jumping)
            {
                //if(platformManager.CheckCollisionFromBottom(player))
                //{
                //    player.mCurrentState = State.Falling;
                //}

                if (player.jumpStartPosition.Y - player.sprite.position.Y >= 100)// is it at max jump height
                {
                    player.mCurrentState = State.Falling;
                }

            }
            else if (player.mCurrentState == State.Falling)
            {
                if (!platformManager.CheckPlatformPlayerCollision(player) && (player.initialPosition.Y <= player.sprite.position.Y))
                {
                    //player.mCurrentState = state.IsKeyDown(Keys.A) || state.IsKeyDown(Keys.D) ? State.Walking : State.Idle;
                    player.jumpStartPosition = player.sprite.position;
                    player.isInAir = false;
                }

            }
        }
    }
}
