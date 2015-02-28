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
        MouseEventListenerManager mouseManager;
        UIManager uiManager;
        List<ImageDisplayObject> displayList = new List<ImageDisplayObject>();
        List<ImageDisplayObject> tempDisplayList = new List<ImageDisplayObject>();

        public int gameWidth;
        public int gameHeight;

        public GameMain()  : base()
        {
            gameMain = this;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            gameWidth = graphics.PreferredBackBufferWidth = 1000;  // set this value to the desired width of your window
            gameHeight = graphics.PreferredBackBufferHeight = 600;   // set this value to the desired height of your window

            this.IsMouseVisible = true;

            Components.Add(new FrameRateCounter(this));
        }
        public static GameMain getInstance() // singleton - To refer back to GameMain
        {
            return gameMain;
        }

        protected override void Initialize() {
            base.Initialize();

            // Set up
            mapManager = MapManager.getInstance();
            mouseManager = MouseEventListenerManager.getInstance();
            uiManager = UIManager.getInstance();

            // Set up GUI
            uiManager.init();

            // Add characters

            Person emily = new Person(this.Content);
            emily.x = gameWidth/2;
            emily.y = gameHeight/2;
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

            uiManager.update(gameTime);
            // add the new display objects onto the real displaylist
            displayList.AddRange(tempDisplayList);
            tempDisplayList.Clear();

            foreach (ImageDisplayObject dis in displayList)
            {
                dis.update(gameTime);
            }


            displayList = (from d in displayList orderby d.y select d).ToList<ImageDisplayObject>();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.NavajoWhite);
            spriteBatch.Begin();

            // draw grid
            drawGrid();

            // display objects
            foreach (ImageDisplayObject dis in displayList)
            {
                spriteBatch.Draw(dis.getTexture(), dis.getDrawRectangle(), Color.White);
            }




            // GUI - display and text
            foreach (DisplayObject dsb in uiManager.getGUIDisplayList() )
            {
                if (dsb.displayObjectType == DisplayObject.DisplayObjectType.TEXT)
                {
                    // is a textfield
                    TextField dtxt = (TextField)dsb;
                    spriteBatch.DrawString(dtxt.getFont(), dtxt.text, new Vector2(dtxt.x, dtxt.y), dtxt.colour);
                }
                else
                {
                    // is an image with texture2D
                    ImageDisplayObject idsb = (ImageDisplayObject)dsb;
                    spriteBatch.Draw(idsb.getTexture(), idsb.getDrawRectangle(), Color.White);
                }
            }
            
            //
            
            spriteBatch.End();

            base.Draw(gameTime);
        }

      
        // Our functions
        private void drawGrid()
        {
            Texture2D pixel = new Texture2D(GraphicsDevice, 1,1);
            pixel.SetData(new Color[] { Color.Gray * 0.1f });

            int width = gameWidth;
            int height = gameHeight;
            
            for (int i = 0; i < width; i += 50)
            {
                spriteBatch.Draw(pixel, new Rectangle(i,0,1, height), Color.AliceBlue);
            }

            for (int i = 0; i < height; i += 50)
            {
                spriteBatch.Draw(pixel, new Rectangle(0, i, width, 1), Color.Gray);
            }
        }
        public void addToStage(ImageDisplayObject ds)
        {
            // a temporary list is needed so it prevents mid displaylist loop new insertion errors
            tempDisplayList.Add(ds);
        }

    }
}
