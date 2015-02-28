using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabMayhem
{
    public class Materials
    {
        public const int NONE = 0;
        public const int WALL = 1;

        private Dictionary<int, Texture2D> materialLib = new Dictionary<int, Texture2D>();

        //
        private GameMain gameMain;
        private static Materials thisSingle;
        public Materials()
        {
            gameMain = GameMain.getInstance();

            addMaterial(WALL, "Images/WallLayouts", 0, 0);

        }
        public static Materials getInstance()
        {
            if (thisSingle == null)
            {
                thisSingle = new Materials();
            }
            return thisSingle;
        }
        private void addMaterial(int materialKey, string srcstr, int xx, int yy)
        {
            Texture2D materialTexture = gameMain.Content.Load<Texture2D>(srcstr);


            materialLib.Add(materialKey, materialTexture);
        }
        public Texture2D getMaterial(int materialKey)
        {
            Texture2D tx;
            materialLib.TryGetValue(materialKey, out tx);
            return tx;
        }
    }
}
