using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabMayhem
{
    public abstract class ImageDisplayObject : DisplayObject
    {
        public ImageDisplayObject()
        {
            this.displayObjectType = DisplayObjectType.IMAGE;
        }
        public abstract Texture2D getTexture();
        public abstract Rectangle getDrawRectangle();
        public abstract void update(GameTime gameTime);
    }
}
