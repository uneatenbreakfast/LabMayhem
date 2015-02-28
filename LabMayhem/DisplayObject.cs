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
        public enum DisplayObjectType
        {
            IMAGE, TEXT
        }
        public DisplayObjectType displayObjectType;
    }
}
