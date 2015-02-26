using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
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
        }

        public override Texture2D getTexture() {
            return buttonTexture;
        }

        public override Rectangle getDrawRectangle() {
            return new Rectangle(0, 600-buttonTexture.Height, buttonTexture.Width, buttonTexture.Height);
        }

        public override void update(Microsoft.Xna.Framework.GameTime gameTime) {
            return;
        }
    }
}
