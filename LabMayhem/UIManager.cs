using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        private Materials materialManager;
        private MapManager mapManager;
        private TaskManager taskManager;

        private List<DisplayObject> guiDisplayList = new List<DisplayObject>();

        
        private TextField fps_txt;
        private TextField mselc_txt;

        private UIHoverTexture cursorSelectorTexture;

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
            materialManager = Materials.getInstance();
            mapManager = MapManager.getInstance();
            taskManager = TaskManager.getInstance();

            // set up buttons
            UIButton addScientist = new UIButton("New Scientist", Color.Black );
            addScientist.y = 600 - addScientist.height;
            addScientist.onClickAction(addNewScientist);
            addToGUI(addScientist);

            UIButton addFlash = new UIButton("Wally", Color.Red);
            addFlash.y = 543;
            addFlash.onClickAction(bringtheflash);
            addToGUI(addFlash);

            Dictionary<int, string> mtrls = new Dictionary<int, string>();
            mtrls.Add( Materials.NONE, "None");
            mtrls.Add( Materials.WALL, "Wall");
            mtrls.Add( 2, "Chair");
            mtrls.Add( 3, "Pavement");

            int btnNum = 2;
            foreach (KeyValuePair<int, string> e in mtrls)
            {
                // do something with entry.Value or entry.Key
                UIButton btn = new UIButton();
                btn.text = "M: "+e.Value;
                btn.x = 0;
                btn.y = GameMain.gameHeight - (30 * btnNum)-30;
                btn.val = e.Key;
                btn.onClickAction(material_select);
                addToGUI(btn);

                btnNum++;
            }
          

            // Texts
            TextField wel_txt = new TextField();
            wel_txt.text = "I've been expecting you, Mr Kuan.";
            wel_txt.x = 50;
            wel_txt.y = 50;
            addToGUI(wel_txt);

            mselc_txt = new TextField();
            mselc_txt.text = "Selected Material:"+selectedMaterial;
            mselc_txt.x = GameMain.gameWidth - mselc_txt.getWidth();
            mselc_txt.y = 20;
            addToGUI(mselc_txt);

            fps_txt = new TextField();
            fps_txt.x = GameMain.gameWidth - fps_txt.getWidth();
            fps_txt.y = 5;
            addToGUI(fps_txt);
        }
        public void update(GameTime gameTime)
        {
            fps_txt.text = string.Format("FPS: {0}", FrameRateCounter.getFPS() );
            fps_txt.x = GameMain.gameWidth - fps_txt.getWidth();

            positionCursorMaterial();
        }

        private void addToGUI(DisplayObject dob, Boolean front=false)
        {
            if (front)
            {
                guiDisplayList.Insert(0, dob);
            }
            else
            {
                guiDisplayList.Add(dob);
            }
           
        }
        public List<DisplayObject> getGUIDisplayList()
        {
            return guiDisplayList.ToList();
        }

        private void positionCursorMaterial()
        {
            if (cursorSelectorTexture != null)
            {
                cursorSelectorTexture.x = (int)(Mouse.GetState().X / GameMain.gridSize) * GameMain.gridSize;
                cursorSelectorTexture.y = (int)(Mouse.GetState().Y / GameMain.gridSize) * GameMain.gridSize;
            }
        }

        // click event listner functions =========
        private void material_select(Object sender, EventArgs e)
        {
            UIButton mc = (UIButton)sender;

            selectedMaterial = mc.val; ;
            mselc_txt.text = "Selected Material:"+selectedMaterial;

            if (selectedMaterial == Materials.NONE)
            {
                guiDisplayList.Remove(cursorSelectorTexture);
                cursorSelectorTexture = null;
            }
            else
            {
                cursorSelectorTexture = new UIHoverTexture(selectedMaterial, materialManager.getMaterialTexture(selectedMaterial));
                cursorSelectorTexture.onClickAction(placeMaterial);
                addToGUI(cursorSelectorTexture, true);
            } 
        }
        private void placeMaterial(Object sender, EventArgs e)
        {
            if (cursorSelectorTexture != null)
            {
                int cx = Mouse.GetState().X / GameMain.gridSize;
                int cy = Mouse.GetState().Y / GameMain.gridSize;
                Boolean canBuildThere = mapManager.buildMaterialAt(cursorSelectorTexture.materialKey, cx, cy);
                if (canBuildThere)
                {
                    taskManager.addBuildTask(cursorSelectorTexture.materialKey, cx, cy);
                }
            }
           
        }
        private void addNewScientist(Object sender, EventArgs e)
        {

             Random r = new Random();
             for (int i = 0; i < 1; i++)
             {
                 int nx = r.Next(1000);
                 int ny = r.Next(600);


                 Scientist emily = new Scientist("Images/girlscientist");
                 emily.x = nx;
                 emily.y = ny;
                 emily.moveTo(100, 300);

                 gameMain.addToStage(emily);
             }

        }

        private void bringtheflash(Object sender, EventArgs e)
        {

            Worker wally = new Worker("Images/CharacterBase_flash");
            wally.x = 300;
            wally.y = 300;
            wally.moveTo(300, 300);

            gameMain.addToStage(wally);
        }

    }
}
