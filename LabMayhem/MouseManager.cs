using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace LabMayhem
{
    class MouseManager
    {
        private static MouseManager mouseManager;

        public Dictionary<ImageDisplayObject, EventHandler> eventHandlerDic = new Dictionary<ImageDisplayObject,EventHandler>();
        private BackgroundWorker bw;

        private MouseState lastMouseState;

        public MouseManager()
        {
            if (mouseManager != null)
            {
                return;
            }

            lastMouseState = Mouse.GetState();
        }

        public static MouseManager getInstance(){
            if (mouseManager == null)
            {
                mouseManager = new MouseManager();
            }
            return mouseManager;
        }

        public void addClickListener(ImageDisplayObject ob, EventHandler funcdelegate)
        {
            eventHandlerDic.Add(ob, funcdelegate);

            if (bw == null)
            {
                bw = new BackgroundWorker();
                bw.DoWork += new DoWorkEventHandler(
                delegate(object o, DoWorkEventArgs args)
                {
                    BackgroundWorker b = o as BackgroundWorker;
                    while (true)
                    {
                        var ms = Mouse.GetState();
                        Point msPoint = new Point(ms.X, ms.Y);
                        Rectangle rc = new Rectangle((int)ob.x, (int)ob.y, ob.width, ob.height);

                        if (lastMouseState.LeftButton == ButtonState.Released && ms.LeftButton == ButtonState.Pressed && rc.Contains(msPoint))
                        {
                            
                            EventHandler ev; 
                            if (eventHandlerDic.TryGetValue(ob, out ev))
                            {
                                ev(this, null);
                            }
                        }
                        lastMouseState = ms;
                    }
                });
                bw.RunWorkerAsync();
            }
        }
    }
}
