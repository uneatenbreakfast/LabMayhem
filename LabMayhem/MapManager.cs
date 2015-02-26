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

        private const int tileHeight = 50;
        private const int tileWidth = 50;
        public MapManager()
        {
             gameMain = GameMain.getInstance();

            int rows = gameMain.graphics.PreferredBackBufferWidth / tileWidth;
            int cols = gameMain.graphics.PreferredBackBufferHeight / tileHeight;
            map = new MapObject[rows, cols];            
        }

        public static MapManager getInstance() // singleton
        {
            if (mapManager == null)
            {
                mapManager = new MapManager();
            }
            return mapManager;
        }

    }
}
