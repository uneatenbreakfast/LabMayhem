﻿using Microsoft.Xna.Framework;
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
        private List<DisplayObject> guiDisplayList = new List<DisplayObject>();

        
        private TextField fps_txt;

        //
        public int selectedMaterial = Materials.NONE;

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
            UIButton addScientist = new UIButton("New Scientist", Color.Black );
            addScientist.y = 600 - addScientist.height;
            addScientist.onClickAction(addNewScientist);
            addToGUI(addScientist);

            UIButton addwall = new UIButton();
            addwall.text = "M: Wall";
            addwall.x = 0;
            addwall.y = 540;
            addToGUI(addwall);

            TextField wel_txt = new TextField();
            wel_txt.text = "I've been expecting you, Mr Kuan.";
            wel_txt.x = 50;
            wel_txt.y = 50;
            addToGUI(wel_txt);

            TextField mselc_txt = new TextField();
            mselc_txt.text = "Selected Material:"+selectedMaterial;
            mselc_txt.x = gameMain.gameWidth - mselc_txt.getWidth();
            mselc_txt.y = 20;
            addToGUI(mselc_txt);

            fps_txt = new TextField();
            fps_txt.x = gameMain.gameWidth - fps_txt.getWidth();
            fps_txt.y = 5;
            addToGUI(fps_txt);
        }
        public void update(GameTime gameTime)
        {
            fps_txt.text = string.Format("FPS: {0}", FrameRateCounter.getFPS() );
            fps_txt.x = gameMain.gameWidth - fps_txt.getWidth(); 
        }

        private void addToGUI(DisplayObject dob)
        {
            guiDisplayList.Add(dob);
        }
        public List<DisplayObject> getGUIDisplayList()
        {
            return guiDisplayList;
        }

        private void addNewScientist(Object sender, EventArgs e)
        {

             Random r = new Random();
             for (int i = 0; i < 100; i++)
             {
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
}