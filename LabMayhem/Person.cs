using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabMayhem
{
   
    class Person : DisplayObject
    {
        private ContentManager content;
        

        private Texture2D front;
        private Texture2D back;

        public Person(ContentManager cloader)
        {
            content = cloader;

            front = content.Load<Texture2D>("Images/scientist_girl_front");
            back = content.Load<Texture2D>("Images/scientist_girl_back");

            this.width = 50;
            this.height = 50;
            this.x = 50;

            this.activeTexture = front;
        }

        public override Texture2D getTexture()
        {
            return this.activeTexture;
        }

        public override Rectangle getDrawRectangle()
        {
            return new Rectangle(x, y, width, height);
        }
    }
}
