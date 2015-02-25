using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabMayhem
{
   
    class Person : DisplayObject
    {
        private ContentManager content;
        private Texture2D characterTexture;

        // spriting vars
        private float spriteInterval = 300f; // ms
        private float spriteIntervalTimer;
        private int animationNum = 2; // 0 - backIdle, 1-backMove, 2-frontIdle, 3-frontMove
        private int spriteHeight = 48;
        private int spriteWidth = 48;
        private int currentFrame = 0;
        private int[] maxFrame = new int[] { 2, 2 };

        //
        public Person(ContentManager cloader)
        {
            content = cloader;
            characterTexture = content.Load<Texture2D>("Images/girlscientist");
        }


        // helper functions
        private Texture2D getImagePart(Texture2D originalTexture)
        {
            Rectangle srcRec = new Rectangle(currentFrame * spriteWidth, animationNum * spriteHeight, spriteWidth, spriteHeight);
            Texture2D cropTexture = new Texture2D(GameMain.graphics.GraphicsDevice, srcRec.Width, srcRec.Height);
        
            Color[] data = new Color[srcRec.Width * srcRec.Height];
            originalTexture.GetData(0, srcRec, data, 0, data.Length);
            cropTexture.SetData(data);

            return cropTexture;
        }

        // override functions
        public override void update(GameTime gameTime)
        {
            x++;


            //
            spriteIntervalTimer += (float) gameTime.ElapsedGameTime.TotalMilliseconds;
            if (spriteIntervalTimer > spriteInterval)
            {
                currentFrame++;
                spriteIntervalTimer = 0f;
                currentFrame = currentFrame % 2;
            }   
           
            //
        }
        public override Texture2D getTexture()
        {
            return getImagePart(characterTexture);
        }


        public override Rectangle getDrawRectangle()
        {
            return new Rectangle(x, y, spriteWidth, spriteHeight);
        }
    }
}
