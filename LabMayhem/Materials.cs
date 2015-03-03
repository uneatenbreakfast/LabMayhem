using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabMayhem
{
    public class Materials
    {
        public const int NONE       = 0;
        public const int WALL       = 1;
        public const int CHEST      = 2;
        public const int PAVEMENT   = 3;

        private Dictionary<int, MaterialObject> materialLib = new Dictionary<int, MaterialObject>();

        //
        private GameMain gameMain;
        private static Materials thisSingle;
        public Materials()
        {
            gameMain = GameMain.getInstance();

            addMaterial(WALL, "Images/WallLayouts",     0,  0,  2,  5);
            addMaterial(CHEST, "Images/Items",          0,  0,  1,  1);
            addMaterial(PAVEMENT, "Images/Pavements",   0,  0,  0,  1);
        }
        public static Materials getInstance()
        {
            if (thisSingle == null)
            {
                thisSingle = new Materials();
            }
            return thisSingle;
        }
       
        private void addMaterial(int materialKey, string srcstr, int xx, int yy, int floorLevel, int secondsToBuild)
        {
            /* xx/yy tells where on the layout to rip the texture2d out of - assuming size is gridSize
             * 
             * floorLevel   - 0 : ground pavement - can have things on top of them
             *              - 1 : objects - eg tables/chairs
             *              - 2 : walls - nothing else can go on that tile cell
             *              - 3 : underground cables?
             *              - 4 : overhanging lights?
             * 
             * 
             */ 

            MaterialObject mb = new MaterialObject();
            mb.key = materialKey;
            mb.texture = gameMain.Content.Load<Texture2D>(srcstr);
            mb.floorLevel = floorLevel;
            mb.secondsToBuild = secondsToBuild;
            
            materialLib.Add(materialKey, mb);
        }
        public Texture2D getMaterialTexture(int materialKey)
        {
            MaterialObject tx;
            materialLib.TryGetValue(materialKey, out tx);
            return tx.texture;
        }
        public MaterialObject getMaterialObject(int key)
        {
            MaterialObject tx;
            materialLib.TryGetValue(key, out tx);
            return tx;
        }
    }
}
