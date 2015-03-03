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
                    Point firstClickPoint = new Point(0, 0); 
                    while (true)
                    {
                        var ms = Mouse.GetState();
                        Point msPoint = new Point(ms.X, ms.Y);

                        // listener only works if mouse is within game Window
                        if (ms.X > GameMain.gameWidth || ms.X < 0)
                        {
                            continue;
                        }
                        if (ms.Y > GameMain.gameHeight || ms.Y < 0)
                        {
                            continue;
                        }

                        //
                        if (lastMouseState.LeftButton == ButtonState.Released && ms.LeftButton == ButtonState.Pressed)
                        {
                            // onPress
                            firstClickPoint.X = ms.X;
                            firstClickPoint.Y = ms.Y;
                            lastMouseState = ms;
                            // ### Mouse inital Press Code


                            continue;
                        }
                        else {
                            if (lastMouseState.LeftButton == ButtonState.Pressed && ms.LeftButton == ButtonState.Released)
                            {
                                // on release
                                // only lets the mouse action go through if the use has released mouse click and mouse is still in the same position as the initial click
                                if (firstClickPoint.X != ms.X || firstClickPoint.Y != ms.Y)
                                {
                                    lastMouseState = ms;
                                    // ### Mouse has been dragged Code




                                    continue;
                                }
                            }
                            else
                            {
                                // no mouse event detected - user probably just waving mouse around the screen
                                lastMouseState = ms;
                                continue;
                            }
                        }

                        // ### Mouse Release Code

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

                            if (rc.Contains(msPoint))
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
