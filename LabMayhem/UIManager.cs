using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabMayhem
{
    class UIManager
    {
        private static UIManager uiManager;
        private GameMain gameMain;

        public UIManager()
        {
            if (uiManager != null)
            {
                return;
            }
        }

        public static UIManager getInstance()
        {
            if (uiManager == null)
            {
                uiManager = new UIManager();
            }
            return uiManager;
        }

        public void init()
        {
            gameMain = GameMain.getInstance();

            // set up buttons
            UIButton addScientist = new UIButton(gameMain.Content);
            addScientist.y = 600 - addScientist.height;
            addScientist.onClickAction(addNewScientist);
            gameMain.addToStage(addScientist);

            UIButton addwall = new UIButton(gameMain.Content);
            addwall.x = addwall.width;
            addwall.y = 600 - addwall.height;
            gameMain.addToStage(addwall);
        }

        private void addNewScientist(Object sender, EventArgs e)
        {
            Random r = new Random();
            int nx = r.Next(1000);
            int ny = r.Next(600);

            Person emily = new Person(gameMain.Content);
            emily.x = nx;
            emily.y = ny;
            emily.moveTo(100, 300);

            gameMain.addToStage(emily);
        }

    }
}
