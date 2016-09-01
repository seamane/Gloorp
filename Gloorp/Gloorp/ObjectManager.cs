using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;

namespace Gloorp
{
    class ObjectManager
    {
        public List<Sprite> objects = new List<Sprite>();

        public ObjectManager()
        {

        }

        public void AddObject(Sprite sprite)
        {
            objects.Add(sprite);
        }

        public void RemoveObject(Sprite sprite)
        {
            objects.Remove(sprite);
        }

        public void PlayerMoved(float amount)
        {
            foreach (Sprite e in objects)
            {
                e.PlayerMoved(amount);
            }
        }
        public void ResetObjects(float offset)
        {
            foreach (Sprite e in objects)
            {
                e.Reset(offset);
            }
        }

        public void Draw(SpriteBatch batch)
        {
            foreach (Sprite s in objects)
            {
                s.Draw(batch);
            }
        }

        public bool CheckPlayerCollisionWithObjects(Player player)
        {
            
            foreach (var o in objects)
            {
                var distance = Math.Abs(player.sprite.position.X - o.position.X);
                
                if (player.getRect().Intersects(o.getRect()))
                {
                    player.nearObject = o;
                    return true;
                }
            }
            player.nearObject = null;
            return false;

        }

    }
}
