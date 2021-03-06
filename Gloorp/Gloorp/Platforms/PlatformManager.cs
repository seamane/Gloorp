﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                    //&& player.sprite.position.X > e.getBoundingBox().Left
                    //&& player.sprite.position.X < e.getBoundingBox().Right)
                {
                    if (player.getRect().Bottom == e.getBoundingBox().Top + 2)
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
                        if (player.getRect().Left <= e.getBoundingBox().Right 
                            && !(player.getRect().Bottom == e.getBoundingBox().Top + 2) 
                            && (player.sprite.position.X >= e.sprite.position.X))
                        {
                            return true;
                        }
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
                        if ((player.getRect().Right >= e.getBoundingBox().Left 
                            && !(player.getRect().Bottom == e.getBoundingBox().Top + 2)) 
                            && (player.sprite.position.X<=e.sprite.position.X))
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }
        internal bool CheckCollisionFromBottom(Player player)
        {
            foreach (Platform e in platforms)
            {
                if (player.getRect().Intersects(e.getBoundingBox()))
                {
                    if(player.getRect().Top <= e.getBoundingBox().Bottom && player.mCurrentState==State.Jumping)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public void ResetPlatforms(float offset)
        {
            foreach (Platform p in platforms)
            {
                p.Reset(offset);
            }
        }
    }
}
