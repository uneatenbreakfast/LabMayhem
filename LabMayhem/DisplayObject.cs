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
        public int x;
        public int y;
        public int width;
        public int height;

        public Texture2D activeTexture;


        public abstract Texture2D getTexture();
        public abstract Rectangle getDrawRectangle();
    }
}
