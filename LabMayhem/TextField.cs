using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabMayhem
{
    class TextField : DisplayObject
    {

        public int val;
        public string text = "";
        public Color colour;
        private GameMain gameMain;

        private SpriteFont activeFont;
        private SpriteFont featureditem14;

        public TextField()
        {
            displayObjectType = DisplayObjectType.TEXT;
            colour = Color.Black;

            gameMain = GameMain.getInstance();

            activeFont = featureditem14 = gameMain.Content.Load<SpriteFont>("Fonts/featureditem-14");
        }

        public void setColour(Color c)
        {
            colour = c;
        }


        public SpriteFont getFont()
        {
            return activeFont;
        }

        public int getWidth()
        {
            width = (int)activeFont.MeasureString(text).X; 
            return width;
        }
    }
}
