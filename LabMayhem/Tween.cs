using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabMayhem
{
    class Tween
    {
        private int updatesTilDeletion = 0;
        private float incrementBy;
        private int targetNumX;
        private DisplayObject ds;

        public enum PropType
        {
            X, Y, WIDTH, HEIGHT, ALPHA, COLOUR
        }
        private PropType propertyType;

        private TweenManager tweenManager;


        public Tween(DisplayObject dob, PropType propType, int targetNum, int timeTakenMils)
        {
            tweenManager = TweenManager.getInstance();
            ds = dob;
            propertyType = propType;
            targetNumX = targetNum;

            updatesTilDeletion = timeTakenMils / 60;

            switch(propType){
                case PropType.X:
                    incrementBy = (targetNumX - dob.x) / (timeTakenMils / 60);
                    break;
                case PropType.Y:
                    incrementBy = (targetNumX - dob.y) / (timeTakenMils / 60);
                    break;
            }
        }

        public void update(int millsElapsed)
        {
            if (updatesTilDeletion <= 0)
            {
                tweenManager.removeTween(this);
            }
            else
            {
                switch (propertyType)
                {
                    case PropType.X:
                        ds.x += incrementBy;
                        break;
                    case PropType.Y:
                        ds.y += incrementBy;
                        break;
                }
            }
            updatesTilDeletion--;
        }
    }
}
