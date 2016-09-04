using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gloorp
{
    class PlatformManager
    {
        internal List<Platform> platforms = new List<Platform>();


        public PlatformManager()
        {

        }

        public void AddPlatform(Platform platform)
        {
            platforms.Add(platform);
        }

        public void PlayerMoved(float amount)
        {
            foreach (Platform e in platforms)
            {
                e.PlayerMoved(amount);
            }
        }
        public void Draw(SpriteBatch batch)
        {
            foreach (Platform e in platforms)
            {
                e.Draw(batch);

            }
        }
        public bool CheckPlatformPlayerCollision(Player player)
        {
            foreach (Platform e in platforms)
            {
                if (player.getRect().Intersects(e.getBoundingBox()))
                {
                    if (player.getRect().Bottom == e.getBoundingBox().Top + 10)
                    {
                        player.isInAir = false;
                        player.mCurrentState = State.Idle;
                        return true;
                    }
                }
                //if (player.getRect().Intersects(e.getBoundingBox()))
                //{                    
                //     Debug.WriteLine("Collission occurs");
                //}

                //else
                //    Debug.WriteLine("Collission doesn't occur");
            }
            return false;
        }

        internal bool CheckCollisionFromSides(KeyboardState state, Player player)
        {
            if (state.IsKeyDown(Keys.A))
            {
                foreach (Platform e in platforms)
                {
                    if (player.getRect().Intersects(e.getBoundingBox()))
                    {
                        if (player.getRect().Left <= e.getBoundingBox().Right && !(player.getRect().Bottom == e.getBoundingBox().Top + 10))
                            return true;
                    }
                }
                return false;
            }
            else
            {
                foreach (Platform e in platforms)
                {
                    if (player.getRect().Intersects(e.getBoundingBox()))
                    {
                        if (player.getRect().Right >= e.getBoundingBox().Left && !(player.getRect().Bottom == e.getBoundingBox().Top + 10))
                            return true;
                    }
                }
                return false;
            }
        }
    }
}
