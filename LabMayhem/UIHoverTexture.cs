using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabMayhem
{
    class UIHoverTexture : ImageDisplayObject
    {
        private Texture2D thisTx;
        public UIHoverTexture(Texture2D tex)
        {
            thisTx = tex;
            width = thisTx.Width;
            height = thisTx.Height;
        }


        //
        public override Texture2D getTexture()
        {
            return thisTx;
        }

        public override Rectangle getDrawRectangle()
        {
            return new Rectangle((int) x,(int) y, width, height);
        }

        public override void update(GameTime gameTime)
        {
            //
        }
    }
}
