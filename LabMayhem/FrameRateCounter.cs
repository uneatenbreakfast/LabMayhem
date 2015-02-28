using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace LabMayhem
{
    public class FrameRateCounter : DrawableGameComponent
    {
        ContentManager content;
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;

        int frameRate = 0;
        int frameCounter = 0;
        TimeSpan elapsedTime = TimeSpan.Zero;

        static FrameRateCounter thisSingle;


        public FrameRateCounter(Game game)
            : base(game)
        {
            content = new ContentManager(game.Services);
            thisSingle = this;
        }


        protected void LoadGraphicsContent(bool loadAllContent)
        {
            if (loadAllContent)
            {
                spriteBatch = new SpriteBatch(GraphicsDevice);
                spriteFont = content.Load<SpriteFont>("Fonts/featureditem-14");
            }
        }


        protected void UnloadGraphicsContent(bool unloadAllContent)
        {
            if (unloadAllContent)
                content.Unload();
        }


        public override void Update(GameTime gameTime)
        {
            elapsedTime += gameTime.ElapsedGameTime;

            if (elapsedTime > TimeSpan.FromSeconds(1))
            {
                elapsedTime -= TimeSpan.FromSeconds(1);
                frameRate = frameCounter;
                frameCounter = 0;
            }
        }


        public override void Draw(GameTime gameTime)
        {
            frameCounter++;

            string fps = string.Format("fps: {0}", frameRate);

        }

        public static int getFPS(){
            return thisSingle.frameRate;
        }

    }
}