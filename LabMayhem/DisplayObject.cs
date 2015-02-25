using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabMayhem
{
    public abstract class DisplayObject
    {
        public float x;
        public float y;// in regards to depth (eg. zero is the point between the feet )
        public int width;
        public int height;

        public abstract Texture2D getTexture();
        public abstract Rectangle getDrawRectangle();
        public abstract void update(GameTime gameTime);
    }
}
