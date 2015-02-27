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
        UIManager uiManager;
        List<DisplayObject> displayList = new List<DisplayObject>();
        List<DisplayObject> tempDisplayList = new List<DisplayObject>();

        private SpriteFont oswald;
        private SpriteFont featureditem;

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

            // Set up
            mapManager = MapManager.getInstance();
            mouseManager = MouseManager.getInstance();
            uiManager = UIManager.getInstance();

            // Set up GUI
            uiManager.init();

            // Add characters
            Random r = new Random();
            for (int i = 0; i < 1; i++)
            {
                int nx = r.Next(1000);
                int ny = r.Next(600);

                Person emily = new Person(this.Content);
                emily.x = nx;
                emily.y = ny;
                emily.moveTo(100, 300);

                addToStage(emily);
            }
        }

        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            oswald = Content.Load<SpriteFont>("Fonts/oswald-20");
            featureditem = Content.Load<SpriteFont>("Fonts/featureditem-20");
        }


        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // add the new display objects onto the real displaylist
            displayList.AddRange(tempDisplayList);
            tempDisplayList.Clear();

            foreach (DisplayObject dis in displayList)
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
            foreach (DisplayObject dis in displayList)
            {
                spriteBatch.Draw(dis.getTexture(), dis.getDrawRectangle(), Color.AliceBlue);
            }
            //
            spriteBatch.DrawString(oswald, "Hello world", new Vector2(200, 200), Color.Black);


            spriteBatch.DrawString(featureditem, "Hello Flash MAN", new Vector2(300, 200), Color.Black);
            //
            spriteBatch.End();


 


            base.Draw(gameTime);
        }
        // Our functions

        public void addToStage(DisplayObject ds)
        {
            // a temporary list is needed so it prevents mid displaylist loop new insertion errors
            tempDisplayList.Add(ds);
        }

    }
}
