using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabMayhem
{
    class MapManager
    {
        private static MapManager mapManager;

        private MapObject[,] map;
        private GameMain gameMain;
        private Materials materialManager;

        public MapManager()
        {
             gameMain = GameMain.getInstance();
             materialManager = Materials.getInstance();

            int rows = GameMain.gameHeight / GameMain.gridSize;
            int cols = GameMain.gameWidth / GameMain.gridSize;
            map = new MapObject[rows+1, cols+1];
        }

        public static MapManager getInstance() // singleton
        {
            if (mapManager == null)
            {
                mapManager = new MapManager();
            }
            return mapManager;
        }

        public Boolean buildMaterialAt(int materialKey, int cx, int cy)
        {
            Boolean canBuildThere = false;
            if (map[cy, cx] == null)
            {
                map[cy, cx] = new MapObject();
            }
            MapObject mb = map[cy, cx];

            // check if it can be placed there
            if (mb.floorLevelObjects[2] == null)
            {
                // no wall on this cell therefore is safe to place anything
                MaterialObject mob = materialManager.getMaterialObject(materialKey);

                int floorLvl = mob.floorLevel;
                if (mb.floorLevelObjects[floorLvl] == null)
                {
                    mb.floorLevelObjects[floorLvl] = new TileObject(cx, cy, mob);
                    canBuildThere = true;
                }
                else
                {
                    // occupied by something else
                }
            }
            else
            {
                // wall is in the way
            }

            return canBuildThere;
        }
        public List<ImageDisplayObject>[] getMapTiles()
        {
            List<ImageDisplayObject>[] mm = new List<ImageDisplayObject>[3];
            mm[0] = new List<ImageDisplayObject>();
            mm[1] = new List<ImageDisplayObject>();
            mm[2] = new List<ImageDisplayObject>();

            for (int iy = 0; iy < map.GetLength(0); iy++)
            {
                for (int ix = 0; ix < map.GetLength(1); ix++)
                {
                    if (map[iy, ix] != null)
                    {
                        MapObject mo = map[iy, ix];
                        for (int p = 0; p < mo.floorLevelObjects.Length; p++)
                        {
                            if (mo.floorLevelObjects[p] != null)
                            {
                                mm[p].Add((ImageDisplayObject) mo.floorLevelObjects[p]);
                            }
                        }

                    }
                }
            }
            return mm;

        }
        public TileObject getTileObjectAt(int x, int y, int floorLevel)
        {
            return ((MapObject)map[y, x]).floorLevelObjects[floorLevel];
        }

    }
}
