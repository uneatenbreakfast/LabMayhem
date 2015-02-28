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
        public int materialKey;

        public UIHoverTexture(int mkey, Texture2D tex)
        {
            materialKey = mkey;
            thisTx = tex;
            width = thisTx.Width;
            height = thisTx.Height;
        }

        public void onClickAction(EventHandler cusEvent)
        {
            MouseEventListenerManager.getInstance().addClickListener(this, cusEvent);
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
