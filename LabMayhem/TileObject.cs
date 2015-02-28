using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabMayhem
{
    public class TileObject : ImageDisplayObject
    {

        private int col;
        private int row;

        public MaterialObject tileType;
        public TileObject(int column, int rowx, MaterialObject mob)
        {
            col = column;
            row = rowx;
            tileType = mob;

            x = col * GameMain.gridSize;
            y = row * GameMain.gridSize;

            width = tileType.texture.Width;
            height = tileType.texture.Height;
        }
        public override Texture2D getTexture()
        {
            return tileType.texture;
        }

        public override Rectangle getDrawRectangle()
        {
            return new Rectangle((int) x, (int) y, width, height);
        }

        public override void update(GameTime gameTime)
        {
           //
        }

        new public Color getColor()
        {
            return Color.White * 0.5f;
        }
    }
}
