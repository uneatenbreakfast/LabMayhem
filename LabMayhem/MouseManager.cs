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
        private List<ImageDisplayObject> buttons = new List<ImageDisplayObject>();
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
            buttons.Add(ob);

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

                        foreach (ImageDisplayObject btn in buttons.ToList())
                        {
                            if (btn == null)
                            {
                                continue;
                            }
                            Rectangle rc = new Rectangle((int) btn.x, (int) btn.y, btn.width, btn.height);

                            if (lastMouseState.LeftButton == ButtonState.Released && ms.LeftButton == ButtonState.Pressed && rc.Contains(msPoint))
                            {

                                EventHandler ev;
                                if (eventHandlerDic.TryGetValue(btn, out ev))
                                {
                                    ev(btn, null);
                                }
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
