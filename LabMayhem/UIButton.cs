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
    class UIButton : ImageDisplayObject {

        private ContentManager content;
        private Texture2D buttonTexture;
        private Texture2D finalTexture;
        private GameMain gameMain;

        private Color backgroundColour = Color.Chocolate;
        private string _text = "";
        public string text { get { return _text; } set { _text = value; setUpTextField(); } }
        public int val;

        public UIButton() {
            startUIButton();
        }
        public UIButton(string str)
        {
            startUIButton();
            text = str;
        }
        public UIButton(string str, Color cc)
        {
            backgroundColour = cc;
            startUIButton();
            text = str;            
        }
        private void startUIButton()
        {
            gameMain = GameMain.getInstance();
            content = gameMain.Content;

            buttonTexture = new Texture2D(gameMain.GraphicsDevice, 150, 25);
            Color[] data = new Color[150 * 25];
            for (int i = 0; i < data.Length; ++i) data[i] = backgroundColour;
            buttonTexture.SetData(data);
            finalTexture = buttonTexture;

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

            gameMain.spriteBatch.DrawString(new TextField().getFont(), _text, new Vector2(5,5), Color.White);

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
