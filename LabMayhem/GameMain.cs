using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LabMayhem
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class GameMain : Game
    {
        public static GameMain gameMain;
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        
        MapManager mapManager;
        MouseManager mouseManager;
        List<DisplayObject> displayList = new List<DisplayObject>();

        public GameMain()  : base()
        {
            gameMain = this;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 1000;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = 600;   // set this value to the desired height of your window

            this.IsMouseVisible = true;
        }
        public static GameMain getInstance() // singleton - To refer back to GameMain
        {
            return gameMain;
        }

        protected override void Initialize() {
            base.Initialize();
            mapManager = MapManager.getInstance();
            mouseManager = MouseManager.getInstance();

            Random r = new Random();

            for (int i = 0; i < 10; i++) {
                int nx = r.Next(1000);
                int ny = r.Next(600);

                Person emily = new Person(this.Content);
                emily.x = nx;
                emily.y = ny;
                emily.moveTo(100, 300);

                addToStage(emily);
            }

            UIButton randomButton = new UIButton(this.Content);
            randomButton.y = 600 - randomButton.height;
            randomButton.onClickAction(doer);
            addToStage(randomButton);
        }

        private void doer(Object sender, EventArgs e)
        {
            Random r = new Random();
            int nx = r.Next(1000);
            int ny = r.Next(600);

            Person emily = new Person(this.Content);
            emily.x = nx;
            emily.y = ny;
            emily.moveTo(100, 300);

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

            foreach (DisplayObject dis in displayList.ToList())
            {
                dis.update(gameTime);
            }


            displayList = (from d in displayList orderby d.y select d).ToList<DisplayObject>();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.NavajoWhite);
            spriteBatch.Begin();
            //
            foreach (DisplayObject dis in displayList.ToList())
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

    }
}
