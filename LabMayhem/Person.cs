using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabMayhem
{
   
    class Person : ImageDisplayObject
    {
        private ContentManager content;
        private Texture2D characterTexture;
        private GameMain gameMain;

        protected TaskManager taskManager;
        protected Task activeTask;

        // character state
        public enum CharacterState
        {
            IDLE,
            MOVING,
            WORKING
        }
        public CharacterState characterState = CharacterState.IDLE;

        public enum CharacterType {
            WORKER, SCIENTIST
        }
        public CharacterType characterType;
        
        private Point moveTarget = new Point(0, 0);
        private float moveSpeed = 2.0f;
        
        // spriting vars
        public enum AnimationState
        {
            IDLE_BACK   = 0,
            MOVE_BACK   = 1,
            IDLE_FRONT  = 2,
            MOVE_FRONT  = 3
        }
        private AnimationState animationNum = AnimationState.IDLE_FRONT;
        private int procrastinationInterval = 0;

        private Texture2D[,] textureStorage;
        private int spriteHeight = 48;
        private int spriteWidth = 48;
        private int currentFrame = 0;
        private int[] maxFrame = new int[] { 2, 4, 2, 4 };
        private float[] frameInterval = new float[] { 300f, 150f, 300f, 150f };
        private float spriteIntervalTimer;

        //
        public Person(string imgsrc) :base ()
        {
            gameMain = GameMain.getInstance();
            taskManager = TaskManager.getInstance();
            content = gameMain.Content;

            characterTexture = content.Load<Texture2D>(imgsrc);
            int col = (int)characterTexture.Width / spriteWidth;
            int row = (int)characterTexture.Height/spriteHeight;
            textureStorage = new Texture2D[col, row];
            width = spriteWidth;
            height = spriteHeight;
        }


        public void moveTo(int tx, int ty)
        {
            moveTarget.X = tx;
            moveTarget.Y = ty;
        }

        public void animate(AnimationState ani)
        {
            if (animationNum != ani)
            {
                animationNum = ani;
                currentFrame = 0;
            }
            
        }
        // helper functions
        private Texture2D getImagePart(Texture2D originalTexture)
        {
            Texture2D aniFrame;
            if (textureStorage[(int) animationNum, currentFrame] == null)
            {
                Rectangle srcRec = new Rectangle(currentFrame * spriteWidth, (int)animationNum * spriteHeight, spriteWidth, spriteHeight);
                textureStorage[(int)animationNum, currentFrame] = aniFrame = HelperMethods.cropImage(originalTexture, srcRec);
            }
            else
            {
                aniFrame = textureStorage[(int)animationNum, currentFrame];
            }
            
            return aniFrame;
        }

        private void processAnimation(GameTime gameTime)
        {
            spriteIntervalTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (spriteIntervalTimer > frameInterval[(int) animationNum])
            {
                currentFrame++;
                spriteIntervalTimer = 0f;
                currentFrame = currentFrame % maxFrame[(int) animationNum];
            }
        }

        private void processMovement()
        {
            if (dist() < 2.0)
            {
                atLocation();
                return;
            }

            // move to the target location
            float dx = Math.Abs(moveTarget.X - x);
            float dy = Math.Abs(moveTarget.Y - y);
            float largest = Math.Max(dx, dy);

            float mX = (dx / largest) * moveSpeed;
            float mY = (dy / largest) * moveSpeed;

            if (x > moveTarget.X)
            {
                x -= mX;
            }else if(x< moveTarget.X) {
                x += mX;
            }

            if (y > moveTarget.Y)
            {
                y -= mY;
                animate(AnimationState.MOVE_BACK);
            }
            else if (y < moveTarget.Y)
            {
                y += mY;
                animate(AnimationState.MOVE_FRONT);
            }
            characterState = CharacterState.MOVING;
        }
        private double dist()
        {
            double dx = moveTarget.X - x;
            double dy = moveTarget.Y - y;
            
            double d = Math.Sqrt( dx*dx + dy*dy );
            return d;
        }
        private void atLocation()
        {
            if (activeTask != null)
            { // this person has a task to do
                characterState = CharacterState.WORKING;
                animate(AnimationState.IDLE_FRONT);
            }
            else
            {
                characterState = CharacterState.IDLE;
                // has reached target location
                if (AnimationState.MOVE_BACK == animationNum)
                {
                    animate(AnimationState.IDLE_BACK);
                }
                else
                {
                    animate(AnimationState.IDLE_FRONT);
                }
            }
           
        }

        private void processCurrentTask()
        {
            switch (characterState)
            {
                case CharacterState.IDLE:
                    if (procrastinationInterval < 0)
                    {
                        nextTaskDecider();
                    }
                    procrastinationInterval--;
                    break;
                case CharacterState.WORKING:
                    doWork();
                    break;
            }
           
        }

        protected virtual void nextTaskDecider()
        {
            wander();
        }

        protected virtual void doWork()
        {
            Console.WriteLine("DO WORK - Override this");
        }

        protected void wander()
        {
            // walk to new area
            Random r = new Random();
            int tox = r.Next(600);
            int toy = r.Next(400);
            moveTo(tox, toy);
            procrastinationInterval = r.Next(200);
            //Console.WriteLine("PROCRASTINATE:" + procrastinationInterval+" tox:"+tox+" | "+toy);
        }

        // override functions
        public override void update(GameTime gameTime)
        {
            processMovement();
            processAnimation(gameTime);
            processCurrentTask();
        }

        public override Texture2D getTexture()
        {
            return getImagePart(characterTexture);
        }

        public override Rectangle getDrawRectangle()
        {
            // positions the character on the stage
            return new Rectangle((int)(x - spriteWidth/2), (int)(y-spriteHeight+4), spriteWidth, spriteHeight);
        }
    }
}
