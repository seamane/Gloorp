﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gloorp
{
    public enum TargetState
    {
        Success,
        Failure,
        Trigger,
        Neutral
    }
    class DirectionTarget
    {
        public Texture2D neutralTexture;
        public Texture2D triggerdTexture;
        public Texture2D successTexture;
        public Texture2D failureTexture;
        public Vector2 position;
        public TargetState currState = TargetState.Neutral;

        public void Draw(SpriteBatch batch, Sprite nearObject)
        {           
            Vector2 pos = new Vector2(nearObject.position.X+((nearObject.texture.Width/2)-32), nearObject.position.Y- 60);
            position = pos;
<<<<<<< HEAD
            //Debug.WriteLine(position+" in draw");
=======
>>>>>>> origin/master
            switch (currState)
            {
                case TargetState.Failure:
                    batch.Draw(failureTexture, pos, Color.White);
                    break;
                case TargetState.Neutral:
                    batch.Draw(neutralTexture, pos, Color.White);
                    break;
                case TargetState.Trigger:
                    batch.Draw(triggerdTexture, pos, Color.White);
                    break;
                case TargetState.Success:
                    batch.Draw(successTexture, pos, Color.White);
                    break;
            }
        }
    }
}
