using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabMayhem
{
    class TweenManager
    {
        private static TweenManager thisTweenManager;
        private List<Tween> tweenList = new List<Tween>();
        private List<Tween> doneTweens = new List<Tween>();

        public TweenManager()
        {

        }
        public static TweenManager getInstance()
        {
            if (thisTweenManager == null)
            {
                thisTweenManager = new TweenManager();
            }
            return thisTweenManager;
        }

        public void To(DisplayObject ob, Tween.PropType propType, int targetNum, int millsTaken, Easing.Equations ease)
        {
            tweenList.Add(new Tween(ob, propType, targetNum, millsTaken, ease));
        }

        public void update(GameTime gameTime)
        {
            foreach (Tween tween in tweenList.ToList() ){
                tween.update(gameTime.ElapsedGameTime.Milliseconds);
            }

            foreach (Tween tween in doneTweens.ToList())
            {
                tweenList.Remove(tween);
            }
            doneTweens.Clear();

        }
        public void removeTween(Tween tw)
        {
            doneTweens.Add(tw);
        }
    }
}
