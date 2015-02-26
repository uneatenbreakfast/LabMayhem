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

        public override Microsoft.Xna.Framework.Graphics.Texture2D getTexture() {
            return buttonTexture;
        }

        public override Microsoft.Xna.Framework.Rectangle getDrawRectangle() {
            return new Microsoft.Xna.Framework.Rectangle((int)x, (int)y, buttonTexture.Width, buttonTexture.Height);
        }

        public override void update(Microsoft.Xna.Framework.GameTime gameTime) {
            return;
        }
    }
}
