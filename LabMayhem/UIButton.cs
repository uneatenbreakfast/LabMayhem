using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace LabMayhem {
    class UIButton : DisplayObject {

        private ContentManager content;
        private Texture2D buttonTexture;
        private Texture2D finalTexture;
        private GameMain gameMain;

        private SpriteFont fontFeaturedItem;
        private string _text = "";
        public string text { get { return _text; } set { _text = value; setUpTextField(); } }

        public UIButton(ContentManager cloader) {
            content = cloader;
            finalTexture = buttonTexture = content.Load<Texture2D>("Images/button");
            fontFeaturedItem = content.Load<SpriteFont>("Fonts/featureditem-20");
            gameMain = GameMain.getInstance();

            width = buttonTexture.Width;
            height = buttonTexture.Height;
        }
        public void onClickAction(EventHandler cusEvent)
        {
            MouseManager.getInstance().addClickListener(this, cusEvent);
        }

        private void setUpTextField()
        {
            RenderTarget2D renderTarget2D = new RenderTarget2D(gameMain.GraphicsDevice, buttonTexture.Width, buttonTexture.Height);
            
            gameMain.GraphicsDevice.SetRenderTarget(renderTarget2D);
            gameMain.spriteBatch.Begin();

            gameMain.spriteBatch.Draw(buttonTexture, Vector2.Zero, new Rectangle(0,0, buttonTexture.Width, buttonTexture.Height), Color.White);
            gameMain.spriteBatch.DrawString(fontFeaturedItem, _text, new Vector2(buttonTexture.Height/2,20), Color.Black);

            gameMain.spriteBatch.End();
            gameMain.GraphicsDevice.SetRenderTarget(null);

            finalTexture = renderTarget2D;
        }

        public override Texture2D getTexture() {
            return finalTexture;
        }

        public override Rectangle getDrawRectangle() {
            return new Rectangle((int) x, (int) y, buttonTexture.Width, buttonTexture.Height);
        }

        public override void update(GameTime gameTime) {
        }
    }
}
