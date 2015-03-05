using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace LabMayhem
{
    class Tween
    {
        private TweenManager tweenManager;
        private int timerNum = 0;
        private int totalDuration;

        private float startingNumX;
        private int targetNumX;
        private DisplayObject ds;

        public enum PropType
        {
            X, Y, WIDTH, HEIGHT, ALPHA, COLOUR
        }
        private PropType propertyType;
        private Easing.Equations easeType;

        public Tween(DisplayObject dob, PropType propType, int targetNum, int timeTakenMils, Easing.Equations ease)
        {
            tweenManager = TweenManager.getInstance();
            ds = dob;
            propertyType = propType;
            targetNumX = targetNum;
            totalDuration = timeTakenMils;
            easeType = ease;            

            switch(propType){
                case PropType.X:
                    startingNumX = ds.x;
                    break;
                case PropType.Y:
                    startingNumX = ds.y;
                    break;
            }
        }

        public void update(int millsElapsed)
        {
            if (timerNum >= totalDuration)
            {
                tweenManager.removeTween(this);
            }
            else
            {
                Easing e = new Easing();
                MethodInfo mi = e.GetType().GetMethod(easeType.ToString());
                
                double n = (double) mi.Invoke(this, new object[] { timerNum, startingNumX, targetNumX, totalDuration });

                switch (propertyType)
                {
                    case PropType.X:
                        ds.x = (float) n;
                        break;
                    case PropType.Y:
                        ds.y = (float) n;
                        break;
                    case PropType.WIDTH:
                        ds.width = (int) n;
                        break;
                    case PropType.HEIGHT:
                        ds.height = (int) n;
                        break;
                }
            }
            timerNum += 60;
        }
    }
}
