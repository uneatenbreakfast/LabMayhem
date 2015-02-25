using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace LabMayhem
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class GameMain : Game
    {
        public static GameMain gameMain;
        public static GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        List<DisplayObject> displayList = new List<DisplayObject>();



        public GameMain()  : base()
        {
            gameMain = this;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
        }
        public static GameMain getInstance()
        {
            return gameMain;
        }

        protected override void Initialize()
        {
            base.Initialize();
         
            Random r = new Random();

            int nx = 100;// r.Next(1000);
            int ny = 100;// r.Next(1000);

            Person emily = new Person(this.Content);
            emily.x = nx;
            emily.y = ny;

            addToStage(emily);
        }

        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }


        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            foreach (DisplayObject dis in displayList)
            {
                dis.update(gameTime);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Salmon);
            spriteBatch.Begin();
            //

            foreach (DisplayObject dis in displayList)
            {
                spriteBatch.Draw(dis.getTexture(), dis.getDrawRectangle(), Color.AliceBlue);
            }

            //
            spriteBatch.End();
            base.Draw(gameTime);
        }
        // Our functions

        public void addToStage(DisplayObject ds)
        {
            displayList.Add(ds);
        }


        //
    }
}
