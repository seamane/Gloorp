﻿using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstMonoGame
{
    public enum CollisionState
    {
        Early,
        OK,
        Late
    }
    class DirectionSpriteManager
    {
        public List<DirectionSprite> directions = new List<DirectionSprite>();
        public DirectionTarget directionTarget = new DirectionTarget();
        public CollisionState collisionState = CollisionState.Early;

        public DirectionSpriteManager()
        {

        }

        public void AddDirection(DirectionSprite direction)
        {
            directions.Add(direction);
        }

        public int UpdateMovement()
        {
            int directionNumber=0;
            foreach (DirectionSprite e in directions)
            {
                if (e.appearanceStatus)
                {
                    e.UpdateMovement();
                    directionNumber = e.directionNumber;
                    break;
                }
            }
            return directionNumber;
        }

        public void Draw(SpriteBatch batch, float nearObjectX)
        {
            directionTarget.Draw(batch,nearObjectX);

            bool tilesAppear = false;
            foreach (DirectionSprite e in directions)
            {
                if (e.appearanceStatus)
                {
                    
                    e.Draw(batch, nearObjectX);
                    tilesAppear = true;
                    break;
                }

            }
            if (!tilesAppear)
            {
                Random randomGenerator = new Random();
                int randomNumber = randomGenerator.Next(1, 5);
                foreach (DirectionSprite e in directions)
                {
                    if (e.directionNumber == randomNumber)
                    {
                        e.appearanceStatus = true;
                    }
                }
            }

        }

        public void Reset(bool resetSpeed)
        {
            foreach (DirectionSprite e in directions)
            {
                e.Reset(resetSpeed);
            }
            
        }

        public bool CollisionForDirectionSprites()
        {
            foreach (var directionSprite in directions)
            {
                var distance = directionSprite.sprite.position.Y - directionTarget.position.Y; //Distance should be greater than or equal to -10
                if (distance < 18 && distance > -20)
                {
                    collisionState = CollisionState.OK;
                    return true;
                }
                else if(distance >= 18)
                {
                    collisionState = CollisionState.Late;
                    return false;
                }
            }
            collisionState = CollisionState.Early;
            return false;

        }

        public void IncrementSpeed()
        {
            foreach(var dir in directions)
            {
                dir.IncrementSpeed();
            }   
        }
    }
}
