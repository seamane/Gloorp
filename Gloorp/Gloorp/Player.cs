
using Microsoft.Xna.Framework;
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

        public Sprite sprite;
        public Vector2 jumpStartPosition;
        public Sprite nearObject;

        public Player()
        {
            sprite = new Sprite();
            jumpStartPosition = Vector2.Zero;
            nearObject = null;
        }
        public Rectangle getRect()
        { return new Rectangle((int)sprite.position.X, (int)sprite.position.X, 64, 64); }
    }
}
