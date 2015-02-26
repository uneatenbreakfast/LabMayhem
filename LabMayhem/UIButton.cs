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
            buttonTexture = content.Load<Texture2D>("Images/girlscientist");
            gameMain = GameMain.getInstance();
        }

        private Texture2D getImagePart(Texture2D originalTexture) {
            Rectangle srcRec = new Rectangle(48, 48, 48, 48);
            Texture2D cropTexture = new Texture2D(gameMain.graphics.GraphicsDevice, srcRec.Width, srcRec.Height);
            Color[] data = new Color[srcRec.Width * srcRec.Height];
            originalTexture.GetData(0, srcRec, data, 0, data.Length);
            cropTexture.SetData(data);

            return cropTexture;
        }

        public override Microsoft.Xna.Framework.Graphics.Texture2D getTexture() {
            return getImagePart(buttonTexture);
        }

        public override Microsoft.Xna.Framework.Rectangle getDrawRectangle() {
            return new Microsoft.Xna.Framework.Rectangle((int)x, (int)y, 48, 48);
        }

        public override void update(Microsoft.Xna.Framework.GameTime gameTime) {
            return;
        }
    }
}
