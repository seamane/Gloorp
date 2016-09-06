﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Gloorp
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private enum GameState
        {
            Playing,
            Finished,
            Dead
        }

        GameState currState = GameState.Playing;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        KeyboardState prevKeyboardState;

        private Player player;
        Sprite frontBackgroundSprite;//tracks which background sprite is furthest left

        //Background tiles
        Sprite mBackgroundOne = new Sprite();
        Sprite mBackgroundTwo = new Sprite();
        Sprite mBackgroundThree = new Sprite();
        Sprite mBackgroundFour = new Sprite();
        Sprite mBackgroundFive = new Sprite();

        //player UI for start of game
        Sprite keyA = new Sprite();
        Sprite keyD = new Sprite();
        Sprite leftArrow = new Sprite();
        Sprite rightArrow = new Sprite();
        Sprite whiteTriggerd = new Sprite();

       

        //finish line
        Sprite finishLine = new Sprite();
        SpriteFont gameFont;
        string outPut;
        string replayText = "Replay?:\n'Y' or 'N'";
        string victoryText = "VICTORY\nReplay?:\n 'Y' or 'N'";
        string blank = "";
        

        //Banner sprites
        //Sprite tryAgain = new Sprite();
        Sprite victory = new Sprite();
        Sprite replay = new Sprite();
        
        float playerInitialPosition;
        
        Animation playerAnim;//holds the current animation for the player
        Animation walkLeftAnim,walkRightAnim,jumpingAnim,fallingAnim,idleAnimation,fallLeftAnim,jumpLeftAnim,pineAppleAnim,figureAnim;

        Animation testObject;
        Animation badGuy;

        Rectangle source;

        // the elapsed amount of time the frame has been shown for
        float time;
        // duration of time to show each frame
        float frameTime = 0.25f;
        // an index of the current frame being shown
        int frameIndex;
        // total number of frames in our spritesheet
        const int totalFrames = 4;
        // define the size of our animation frame
        int frameHeight = 64;
        int frameWidth = 64;

        ObjectManager objectManager = new ObjectManager();
        EnemyManager enemyManager;

        const float playerSpeed = 3;

        // contains all the directions
        DirectionSpriteManager directionManager = new DirectionSpriteManager();
        // constant circle where the direction sprites come in contact with
        //Sprite constantCircle = new Sprite();
        // The invisible frame size surrounding the constant circle object. Change the numbers according to the size of the object.
        //Point constantCircleFrameSize = new Point(100, 100);
        // The invisible frame size surrounding the moving direction circle object. Change the numbers according to the size of the object.
       // Point constantDirectionFrameSize = new Point(100, 100);
        int randomNumber;
        //Animation badGuy;
        Texture2D floor;
        PlatformManager platformManager = new PlatformManager();

        public SpriteFont GameFont
        {
            get
            {
                return gameFont;
            }

            set
            {
                gameFont = value;
            }
        }

        //Test objects
        //private Texture2D hideObject;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 550;
            graphics.PreferredBackBufferWidth = 1000;
            Content.RootDirectory = "Content";
            player = new Player();
            enemyManager = new EnemyManager(player);
            prevKeyboardState = new KeyboardState();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            player.sprite.position = new Vector2(450, 500);
            player.initialPosition = player.sprite.position;

       

            source = new Rectangle(frameIndex * frameWidth, 0, frameWidth, frameHeight);
            
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Animations
            
            walkLeftAnim = new Animation(Content, "Images/Player/blob_LeftWalk", 200f, 3, true);
            walkRightAnim = new Animation(Content, "Images/Player/blob_RightWalk", 200f, 3, true);
            jumpingAnim = new Animation(Content, "Images/Player/blob_Jump", 200f, 5, false);
            fallingAnim = new Animation(Content, "Images/Player/blob_Fall", 200f, 5, false);
            idleAnimation = new Animation(Content, "Images/Player/blobIdle", 200, 5, true);
            fallLeftAnim = new Animation(Content, "Images/Player/blob_FallLeft", 200f, 5, false);
            jumpLeftAnim = new Animation(Content, "Images/Player/blob_JumpLeft", 200f, 5, false);
            playerAnim = idleAnimation;
            playerAnim.Position = player.sprite.position;
            spriteBatch = new SpriteBatch(GraphicsDevice);

            
            pineAppleAnim = new Animation(Content, "Images/HideObjects/Blob_PineAppleAnim_137", 100, 7, false);
            figureAnim = new Animation(Content, "Images/HideObjects/Blob_figureAnim_150", 100, 8, false);

            
            testObject = pineAppleAnim;

            LoadDirectionSprites();
            //playerAnimator = new Animation(player.sprite.texture, frameWidth, totalFrames,frameTime);


            LoadBackground();
            LoadEnemies();
            LoadObjects();
            LoadPlatforms();


            //init destination target
            directionManager.directionTarget.neutralTexture = Content.Load<Texture2D>("DirectionSprites/iconWhite");
            directionManager.directionTarget.triggerdTexture = Content.Load<Texture2D>("DirectionSprites/iconWhite_Triggerd");
            directionManager.directionTarget.failureTexture = Content.Load<Texture2D>("DirectionSprites/arrow_Fail");
            directionManager.directionTarget.successTexture = Content.Load<Texture2D>("DirectionSprites/arrow_Success");
            directionManager.directionTarget.position = new Vector2(50, 200);//x value doesnt matter here

            floor = Content.Load<Texture2D>("Images/BackgroundArt/floorTexture");

            badGuy = new Animation(Content, "Images/BackgroundArt/Scientist", 400 ,9, true);
            badGuy.Position = new Vector2(850, 170);

            //load player UI for start of game
            keyA.texture = Content.Load<Texture2D>("DirectionSprites/A_Icon");
            keyA.position = new Vector2(370,390);
            keyD.texture = Content.Load<Texture2D>("DirectionSprites/D_Icon");
            keyD.position = new Vector2(450, 390);
            leftArrow.texture = Content.Load<Texture2D>("DirectionSprites/left arrow");
            leftArrow.position = new Vector2(375, 350);
            rightArrow.texture = Content.Load<Texture2D>("DirectionSprites/right arrow");
            rightArrow.position = new Vector2(462, 350);

            //finish line
            finishLine.texture = Content.Load<Texture2D>("Images/Player/rectangleSprite");
            finishLine.position = new Vector2(6150, 450);

            //banner art
            replay.texture = Content.Load<Texture2D>("Images/BannerArt/replay");
            replay.position = new Vector2(220, 100);
            victory.texture = Content.Load<Texture2D>("Images/BannerArt/VictorySprite");
            victory.position = new Vector2(240, 10);

            gameFont = Content.Load<SpriteFont>("Fonts/GameFont");
            outPut = blank;
            
        }

        private void LoadPlatforms()
        {
            //xLarge is 1150 wide
            int xPos = -400;
            int gapSize = 90;
            Platform platform = new Platform(new Vector2(xPos, 498), Content.Load<Texture2D>("Images/BackgroundArt/largePlatform"));
            platformManager.AddPlatform(platform);
            xPos += platform.sprite.texture.Width;
            platform = new Platform(new Vector2(xPos, 498), Content.Load<Texture2D>("Images/BackgroundArt/xLargePlatform"));
            platformManager.AddPlatform(platform);
            xPos += platform.sprite.texture.Width + gapSize;
            platform = new Platform(new Vector2(xPos, 408), Content.Load<Texture2D>("Images/BackgroundArt/smallPlatform"));
            platformManager.AddPlatform(platform);
            xPos += platform.sprite.texture.Width + gapSize;
            platform = new Platform(new Vector2(xPos, 338), Content.Load<Texture2D>("Images/BackgroundArt/mediumPlatform"));
            platformManager.AddPlatform(platform);
            xPos += platform.sprite.texture.Width + gapSize;
            platform = new Platform(new Vector2(xPos, 408), Content.Load<Texture2D>("Images/BackgroundArt/mediumPlatform"));
            platformManager.AddPlatform(platform);
            xPos += platform.sprite.texture.Width + gapSize;
            platform = new Platform(new Vector2(xPos, 408), Content.Load<Texture2D>("Images/BackgroundArt/mediumPlatform"));
            platformManager.AddPlatform(platform);
            xPos += platform.sprite.texture.Width + gapSize;
            platform = new Platform(new Vector2(xPos, 408), Content.Load<Texture2D>("Images/BackgroundArt/smallPlatform"));
            platformManager.AddPlatform(platform);
            xPos += platform.sprite.texture.Width + gapSize;
            platform = new Platform(new Vector2(xPos, 498), Content.Load<Texture2D>("Images/BackgroundArt/smallPlatform"));
            platformManager.AddPlatform(platform);
            xPos += platform.sprite.texture.Width + gapSize;
            platform = new Platform(new Vector2(xPos, 408), Content.Load<Texture2D>("Images/BackgroundArt/smallPlatform"));
            platformManager.AddPlatform(platform);
            xPos += platform.sprite.texture.Width + gapSize;
            platform = new Platform(new Vector2(xPos, 338), Content.Load<Texture2D>("Images/BackgroundArt/smallPlatform"));
            platformManager.AddPlatform(platform);
            xPos += platform.sprite.texture.Width + gapSize;
            platform = new Platform(new Vector2(xPos, 498), Content.Load<Texture2D>("Images/BackgroundArt/xLargePlatform"));
            platformManager.AddPlatform(platform);
            xPos += platform.sprite.texture.Width + gapSize;
        }

        private void LoadObjects()
        {
            Texture2D pineapple = Content.Load<Texture2D>("Images/HideObjects/pineApple_137");
            Texture2D figure = Content.Load<Texture2D>("Images/HideObjects/Figure_150");

            Sprite hideObject = new Sprite();
            hideObject.position = new Vector2(700, 365);
            hideObject.texture = pineapple;
            objectManager.AddObject(hideObject);

            hideObject = new Sprite();
            hideObject.position = new Vector2(1050, 350);
            hideObject.texture = figure;
            objectManager.AddObject(hideObject);

            hideObject = new Sprite();
            hideObject.position = new Vector2(2100, 190);
            hideObject.texture = figure;
            objectManager.AddObject(hideObject);

            hideObject = new Sprite();
            hideObject.position = new Vector2(2900, 275);
            hideObject.texture = pineapple;
            objectManager.AddObject(hideObject);

            hideObject = new Sprite();
            hideObject.position = new Vector2(4485, 260);
            hideObject.texture = figure;
            objectManager.AddObject(hideObject);

            hideObject = new Sprite();
            hideObject.position = new Vector2(5200, 350);
            hideObject.texture = figure;
            objectManager.AddObject(hideObject);

            hideObject = new Sprite();
            hideObject.position = new Vector2(5900, 365);
            hideObject.texture = pineapple;
            objectManager.AddObject(hideObject);
        }

        private void LoadEnemies()
        {
            Texture2D ground = Content.Load<Texture2D>("Images/Enemies/Ground_Enemy");
            Texture2D air = Content.Load<Texture2D>("Images/Enemies/flying_Enemy");

            Enemy enemy = new GroundEnemy(new Vector2(1100, 440), 2.5f, 200);
            enemy.sprite.texture = ground;
            enemyManager.AddEnemy(enemy);
            
            enemy = new AirEnemy(new Vector2(1150, 250), 2.0f, 350);
            enemy.sprite.texture = air;
            enemyManager.AddEnemy(enemy);

            enemy = new GroundEnemy(new Vector2(2140, 280), 2.5f, 250);
            enemy.sprite.texture = ground;
            enemyManager.AddEnemy(enemy);

            enemy = new AirEnemy(new Vector2(2800, 200), 2.0f, 250);
            enemy.sprite.texture = air;
            enemyManager.AddEnemy(enemy);

            enemy = new AirEnemy(new Vector2(4400, 200), 2.0f, 250);
            enemy.sprite.texture = air;
            enemyManager.AddEnemy(enemy);

            enemy = new GroundEnemy(new Vector2(5300, 440), 2.5f, 250);
            enemy.sprite.texture = ground;
            enemyManager.AddEnemy(enemy);

            enemy = new AirEnemy(new Vector2(5600, 300), 2.0f, 500);
            enemy.sprite.texture = air;
            enemyManager.AddEnemy(enemy);

            enemy = new GroundEnemy(new Vector2(5800, 440), 2.5f, 250);
            enemy.sprite.texture = ground;
            enemyManager.AddEnemy(enemy);
        }

        private void LoadDirectionSprites()
        {
            // Loading all the direction sprites.
            DirectionSprite directionUp = new DirectionSprite(new Vector2(50, 50), 1);
            directionUp.sprite.texture = Content.Load<Texture2D>("DirectionSprites/arrow_up");
            directionManager.AddDirection(directionUp);
            DirectionSprite directionDown = new DirectionSprite(new Vector2(50, 50), 2);
            directionDown.sprite.texture = Content.Load<Texture2D>("DirectionSprites/arrow_down");
            directionManager.AddDirection(directionDown);
            DirectionSprite directionLeft = new DirectionSprite(new Vector2(50, 50), 3);
            directionLeft.sprite.texture = Content.Load<Texture2D>("DirectionSprites/arrow_left");
            directionManager.AddDirection(directionLeft);
            DirectionSprite directionRight = new DirectionSprite(new Vector2(50, 50), 4);
            directionRight.sprite.texture = Content.Load<Texture2D>("DirectionSprites/arrow_right");
            directionManager.AddDirection(directionRight);

            // Loading a random number so that one object appears at a time.
            Random randomGenerator = new Random();
            randomNumber = randomGenerator.Next(1, 5);
            switch(randomNumber)
            {
                case 1:
                    directionUp.appearanceStatus = true;
                    break;
                case 2:
                    directionDown.appearanceStatus = true;
                    break;
                case 3:
                    directionLeft.appearanceStatus = true;
                    break;
                case 4:
                    directionRight.appearanceStatus = true;
                    break;

            }
        }

        private void LoadBackground()
        {
            //load background sprites
            mBackgroundOne.texture = Content.Load<Texture2D>("Images/BackgroundArt/the room3");
            mBackgroundOne.position = new Vector2(-mBackgroundOne.texture.Bounds.Width, -260);

            mBackgroundTwo.texture = Content.Load<Texture2D>("Images/BackgroundArt/the room3");
            mBackgroundTwo.position = new Vector2(mBackgroundOne.position.X + mBackgroundOne.texture.Bounds.Width, -260);

            mBackgroundThree.texture = Content.Load<Texture2D>("Images/BackgroundArt/the room3");
            mBackgroundThree.position = new Vector2(mBackgroundTwo.position.X + mBackgroundTwo.texture.Bounds.Width, -260);

            mBackgroundFour.texture = Content.Load<Texture2D>("Images/BackgroundArt/the room3");
            mBackgroundFour.position = new Vector2(mBackgroundThree.position.X + mBackgroundThree.texture.Bounds.Width, -260);

            mBackgroundFive.texture = Content.Load<Texture2D>("Images/BackgroundArt/the room3");
            mBackgroundFive.position = new Vector2(mBackgroundFour.position.X + mBackgroundFour.texture.Bounds.Width, -260);

            frontBackgroundSprite = mBackgroundOne;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            KeyboardState currKeyboardState = Keyboard.GetState();

            if (currState == GameState.Finished || currState == GameState.Dead)
            {
                //check for Y or N pressed
                if(currKeyboardState.IsKeyDown(Keys.Y) && !prevKeyboardState.IsKeyDown(Keys.Y))
                {
                    currState = GameState.Playing;
                    player.mCurrentState = State.Idle;
                    playerAnim = idleAnimation;
                    ReplayPressed();
                }
                else if(currKeyboardState.IsKeyDown(Keys.N) && !prevKeyboardState.IsKeyDown(Keys.N))
                {
                    Exit();
                }
                return;
            }

            //has player crossed finish line?
            if(player.sprite.position.X > finishLine.position.X)
            {
                currState = GameState.Finished;
                return;
            }

            base.Update(gameTime);
           
            //is the player near an object he can hide behind?
            if (player.nearObject != null)
            {
                randomNumber = directionManager.UpdateMovement();
            }
           
            // checked for collision
            // check if in range of object; if true, display UI in DirectionSpriteManager
            // if UI is displayed, check for user input; the first key stroke with start the animation of the UI
            if (directionManager.CollisionForDirectionSprites())
            {
                CheckKeyPressed();
            }
            else
            {
                if ((directionManager.collisionState == CollisionState.Late)
                    || currKeyboardState.IsKeyDown(Keys.Down) && !prevKeyboardState.IsKeyDown(Keys.Down)
                    || currKeyboardState.IsKeyDown(Keys.Up) && !prevKeyboardState.IsKeyDown(Keys.Up)
                    || currKeyboardState.IsKeyDown(Keys.Right) && !prevKeyboardState.IsKeyDown(Keys.Right)
                    || currKeyboardState.IsKeyDown(Keys.Left) && !prevKeyboardState.IsKeyDown(Keys.Left))
                {
                     player.mCurrentState = player.mCurrentState == State.Disguised ? State.Idle : player.mCurrentState;
                    directionManager.directionTarget.currState = TargetState.Failure;
                    directionManager.Reset(true); //Removed direction reset so that the arrow goes till the end.
                }
            }

            if(!objectManager.CheckPlayerCollisionWithObjects(player))
            {
                directionManager.directionTarget.currState = TargetState.Neutral;
                directionManager.Reset(true);
            }

            

            playerAnim.PlayAnim(gameTime);

            badGuy.PlayAnim(gameTime);

            UpdateMovement(currKeyboardState);
            UpdateJump(currKeyboardState);
            UpdateBackground();
            enemyManager.UpdateMovement();
           
            playerAnim.Position = new Vector2(player.sprite.position.X-33, player.sprite.position.Y - 64);

            testObject.Position = new Vector2(player.sprite.position.X - 60, player.sprite.position.Y - 139 );



            //set the player to the shape of the object if disguised
            if (directionManager.directionTarget.currState == TargetState.Success && player.mCurrentState == State.Disguised)
            {

                if (player.nearObject.getName() == "Images/HideObjects/pineApple_137")
                    playerAnim = pineAppleAnim;
                else
                    playerAnim = figureAnim;
                playerAnim.Position = player.nearObject.position;//snap to the position of the object we are mimicking.
            }
            else
            {
                pineAppleAnim.CurrentFrame = 0;//reset the frames on the objects animation.
            }

            // check if enemy can see player
            if (enemyManager.CanEnemySeePlayer() || player.sprite.position.Y > player.initialPosition.Y + 100)
            {
                //then game over
                player.mCurrentState = State.Found;
                currState = GameState.Dead;
            }
            
            prevKeyboardState = currKeyboardState;
        }

        private void UpdateBackground()
        {
            if (mBackgroundOne.position.X < -mBackgroundOne.texture.Bounds.Width * 2)
            {
                mBackgroundOne.position.X = mBackgroundFive.position.X + mBackgroundFive.texture.Bounds.Width;
                frontBackgroundSprite = mBackgroundTwo;
            }

            else if (mBackgroundTwo.position.X < -mBackgroundTwo.texture.Bounds.Width * 2)
            {
                mBackgroundTwo.position.X = mBackgroundOne.position.X + mBackgroundOne.texture.Bounds.Width;
                frontBackgroundSprite = mBackgroundThree;
            }

            else if (mBackgroundThree.position.X < -mBackgroundThree.texture.Bounds.Width * 2)
            {
                mBackgroundThree.position.X = mBackgroundTwo.position.X + mBackgroundTwo.texture.Bounds.Width;
                frontBackgroundSprite = mBackgroundFour;
            }

            else if (mBackgroundFour.position.X < -mBackgroundFour.texture.Bounds.Width * 2)
            {
                mBackgroundFour.position.X = mBackgroundThree.position.X + mBackgroundThree.texture.Bounds.Width;
                frontBackgroundSprite = mBackgroundFive;
            }

            else if (mBackgroundFive.position.X < -mBackgroundFive.texture.Bounds.Width * 2)
            {
                mBackgroundFive.position.X = mBackgroundFour.position.X + mBackgroundFour.texture.Bounds.Width;
                frontBackgroundSprite = mBackgroundOne;
            }
        }

        private void UpdateJump(KeyboardState state)
        {
            if (!player.isInAir)
            {
                player.CheckWhenPlayerIsOnGround(player, platformManager, state);
            }
            else
            {
                player.CheckWhenPlayerIsNotOnGround(player, platformManager, state);
            }
        }

        private void UpdateMovement(KeyboardState state)
        {
            if (state.IsKeyDown(Keys.A) && frontBackgroundSprite.position.X < 0)//move left
            {
                if (player.mCurrentState == State.Disguised)
                {
                    directionManager.directionTarget.currState = TargetState.Failure;
                }
                if (player.mCurrentState != State.Jumping && player.mCurrentState != State.Falling)
                {
                    player.mCurrentState = State.Walking;
                    playerAnim = walkLeftAnim;
                }

                if (!platformManager.CheckCollisionFromSides(state, player))
                {
                    MoveBackground(playerSpeed);

                    enemyManager.PlayerMoved(playerSpeed);
                    objectManager.PlayerMoved(playerSpeed);
                    platformManager.PlayerMoved(playerSpeed);
                    keyA.position.X += playerSpeed;
                    keyD.position.X += playerSpeed;
                    leftArrow.position.X += playerSpeed;
                    rightArrow.position.X += playerSpeed;
                    finishLine.position.X += playerSpeed;
                    badGuy.Position = new Vector2(badGuy.Position.X + 5, badGuy.Position.Y);
                }
            }
            else if (state.IsKeyDown(Keys.D))//move right
            {
                if (player.mCurrentState == State.Disguised)
                {
                    directionManager.directionTarget.currState = TargetState.Failure;
                }
                if (player.mCurrentState != State.Jumping && player.mCurrentState != State.Falling)
                {
                    player.mCurrentState = State.Walking;
                    playerAnim = walkRightAnim;
                }
                if (!platformManager.CheckCollisionFromSides(state, player))
                {
                    MoveBackground(-playerSpeed);
                    platformManager.PlayerMoved(-playerSpeed);
                    enemyManager.PlayerMoved(-playerSpeed);
                    objectManager.PlayerMoved(-playerSpeed);
                    keyA.position.X -= playerSpeed;
                    keyD.position.X -= playerSpeed;
                    leftArrow.position.X -= playerSpeed;
                    rightArrow.position.X -= playerSpeed;
                    finishLine.position.X -= playerSpeed;
                    badGuy.Position = new Vector2(badGuy.Position.X - playerSpeed, badGuy.Position.Y);
                }
            }

            if (player.mCurrentState == State.Jumping)//jumping. i.e. moving upward
            {
                if (state.IsKeyDown(Keys.A))
                    playerAnim = jumpLeftAnim;
                else
                    playerAnim = jumpingAnim;
                player.sprite.position.Y -= 5;
            }
            else if (player.mCurrentState == State.Falling)//moving downward
            {
                if (state.IsKeyDown(Keys.A))
                    playerAnim = fallLeftAnim;
                else
                    playerAnim = fallingAnim;

                player.sprite.position.Y += 5;
            }
            else if(!state.IsKeyDown(Keys.A) && !state.IsKeyDown(Keys.D))//idle
            {
                playerAnim = idleAnimation;
            }
        }

        private void MoveBackground(float amount)
        {
            mBackgroundOne.position.X += amount;
            mBackgroundTwo.position.X += amount;
            mBackgroundThree.position.X += amount;
            mBackgroundFour.position.X += amount;
            mBackgroundFive.position.X += amount;
           
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);

            // TODO: Add your drawing code here

            base.Draw(gameTime);


            spriteBatch.Begin();

            

            //draw background first
            mBackgroundOne.Draw(spriteBatch);
            mBackgroundTwo.Draw(spriteBatch);
            mBackgroundThree.Draw(spriteBatch);
            mBackgroundFour.Draw(spriteBatch);
            mBackgroundFive.Draw(spriteBatch);
            enemyManager.Draw(spriteBatch);
            platformManager.Draw(spriteBatch);

            
            //draw badguy
            //badGuy.Draw(spriteBatch);

            objectManager.Draw(spriteBatch);
            spriteBatch.Draw(finishLine.texture, finishLine.position, Color.White);

            //draw "blob"
            Vector2 origin = new Vector2(frameWidth / 2.0f, frameHeight);
           
            playerAnim.Draw(spriteBatch);
            //Draws the floor
            //spriteBatch.Draw(floor, new Rectangle(0, 497, 1000, 600), Color.DarkGray);

            if (player.nearObject!=null) {
                directionManager.Draw(spriteBatch,player.nearObject.position.X);               
                //constantCircle.Draw(spriteBatch);
            }

            spriteBatch.Draw(keyA.texture, keyA.position, null, Color.White, 0.0f, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0.0f);
            spriteBatch.Draw(keyD.texture, keyD.position, null, Color.White, 0.0f, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0.0f);
            //spriteBatch.Draw(leftArrow.texture, leftArrow.position, null, Color.White, 0.0f, new Vector2(0, 0), 0.15f, SpriteEffects.None, 0.0f);
            //spriteBatch.Draw(rightArrow.texture, rightArrow.position, null, Color.White, 0.0f, new Vector2(0, 0), 0.15f, SpriteEffects.None, 0.0f);

            if(currState == GameState.Dead || currState == GameState.Finished)
            {
                //spriteBatch.Draw(replay.texture, replay.position, null, Color.White, 0.0f, new Vector2(0, 0), 0.2f, SpriteEffects.None, 0.0f);
                outPut = replayText;//change the text so it asks for replay
            }
            if(currState == GameState.Finished)
            {
                //spriteBatch.Draw(victory.texture, victory.position, null, Color.White, 0.0f, new Vector2(0, 0), 0.35f, SpriteEffects.None, 0.0f);
                outPut = victoryText;//change the text so it says Victory
            }
            spriteBatch.DrawString(gameFont, outPut, new Vector2(400, 200), Color.White);//draws the font to the screen. its the top layer
            spriteBatch.End();
        }
        



        private void CheckKeyPressed()
        {
            //bool intersects = false;
            var newState = Keyboard.GetState();
            
            if ((newState.IsKeyDown(Keys.Down) && !prevKeyboardState.IsKeyDown(Keys.Down) && randomNumber == 2)
                || (newState.IsKeyDown(Keys.Up) && !prevKeyboardState.IsKeyDown(Keys.Up) && randomNumber == 1)
                || (newState.IsKeyDown(Keys.Left) && !prevKeyboardState.IsKeyDown(Keys.Left) && randomNumber == 3)
                || (newState.IsKeyDown(Keys.Right) && !prevKeyboardState.IsKeyDown(Keys.Right) && randomNumber == 4))
            {
                //intersects = true;
                player.mCurrentState = State.Disguised;
                directionManager.directionTarget.currState = TargetState.Success;               
                directionManager.Reset(false);
                directionManager.IncrementSpeed();
            }
            else if(directionManager.collisionState == CollisionState.Late)
            {
                player.mCurrentState = player.mCurrentState == State.Disguised ? State.Idle : player.mCurrentState;
                directionManager.directionTarget.currState = TargetState.Failure;
                directionManager.Reset(true);
            }
            else if(newState.IsKeyDown(Keys.Down) && !prevKeyboardState.IsKeyDown(Keys.Down)
                || newState.IsKeyDown(Keys.Up) && !prevKeyboardState.IsKeyDown(Keys.Up)
                || newState.IsKeyDown(Keys.Right) && !prevKeyboardState.IsKeyDown(Keys.Right)
                || newState.IsKeyDown(Keys.Left) && !prevKeyboardState.IsKeyDown(Keys.Left))
            { 
                player.mCurrentState = player.mCurrentState == State.Disguised ? State.Idle : player.mCurrentState;  
                directionManager.directionTarget.currState = TargetState.Failure;
                directionManager.Reset(true);  //Removed direction reset so that the arrow goes till the end.
            }
           
        }

        private void ReplayPressed()
        {
            //load background sprites
            mBackgroundOne.position = new Vector2(-mBackgroundOne.texture.Bounds.Width, -260);
            mBackgroundTwo.position = new Vector2(mBackgroundOne.position.X + mBackgroundOne.texture.Bounds.Width, -260);
            mBackgroundThree.position = new Vector2(mBackgroundTwo.position.X + mBackgroundTwo.texture.Bounds.Width, -260);
            mBackgroundFour.position = new Vector2(mBackgroundThree.position.X + mBackgroundThree.texture.Bounds.Width, -260);
            mBackgroundFive.position = new Vector2(mBackgroundFour.position.X + mBackgroundFour.texture.Bounds.Width, -260);
            frontBackgroundSprite = mBackgroundOne;

            float offset = 6150 - finishLine.position.X;
            objectManager.ResetObjects(offset);
            enemyManager.ResetEnemies(offset);
            platformManager.ResetPlatforms(offset);

            //reset player position
            player.sprite.position = player.initialPosition;

            //load player UI for start of game
            keyA.position.X += offset;
            keyD.position.X += offset;
            leftArrow.position.X += offset;
            rightArrow.position.X += offset;

            outPut = blank;//change the font back to not show anything
            //finish line
            finishLine.position.X += offset;
        }
    }
}
