using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace LabMayhem
{
    class MouseEventListenerManager
    {
        private static MouseEventListenerManager mouseManager;
        private static object padLock = new object();

        public Dictionary<ImageDisplayObject, EventHandler> eventHandlerDic = new Dictionary<ImageDisplayObject,EventHandler>();
        private List<ImageDisplayObject> buttons = new List<ImageDisplayObject>();
        private BackgroundWorker bw;

        private MouseState lastMouseState;

        public MouseEventListenerManager()
        {
            if (mouseManager != null)
            {
                return;
            }

            lastMouseState = Mouse.GetState();
        }

        public static MouseEventListenerManager getInstance(){
            if (mouseManager == null)
            {
                mouseManager = new MouseEventListenerManager();
            }
            return mouseManager;
        }

        public void addClickListener(ImageDisplayObject ob, EventHandler funcdelegate)
        {
            eventHandlerDic.Add(ob, funcdelegate);

            lock (padLock)
            {
                buttons.Add(ob);
            }
           

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


                        List<ImageDisplayObject> blist = new List<ImageDisplayObject>();
                        lock (padLock)
                        {
                            blist = buttons.ToList();
                        }
                        
                        foreach (ImageDisplayObject btn in blist)
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
