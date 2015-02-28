using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabMayhem
{
    class HelperMethods
    {
        // methods which are universally useful by many classes can go here

        public static Texture2D cropImage(Texture2D originalTexture,Rectangle srcRec )
        {
            Texture2D cropTexture = new Texture2D(GameMain.getInstance().graphics.GraphicsDevice, srcRec.Width, srcRec.Height);

            Color[] data = new Color[srcRec.Width * srcRec.Height];
            originalTexture.GetData(0, srcRec, data, 0, data.Length);
            cropTexture.SetData(data);

            return cropTexture;
        }

        //
    }
}
