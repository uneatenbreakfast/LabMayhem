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
        private GameMain gameMain;

        public UIButton(ContentManager cloader) {
            content = cloader;
            buttonTexture = content.Load<Texture2D>("Images/button");
            gameMain = GameMain.getInstance();

            width = buttonTexture.Width;
            height = buttonTexture.Height;

        }
        public void onClickAction(EventHandler cusEvent)
        {
            MouseManager.getInstance().addClickListener(this, cusEvent);
        }

        public override Texture2D getTexture() {
            return buttonTexture;
        }

        public override Rectangle getDrawRectangle() {
            return new Rectangle((int) x, (int) y, buttonTexture.Width, buttonTexture.Height);
        }

        public override void update(GameTime gameTime) {
        }
    }
}
