using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabMayhem
{
    class Worker : Person
    {
        MapManager mapManager;

        public Worker(string imgsrc) : base(imgsrc)
        {
            characterType = CharacterType.WORKER;
            mapManager = MapManager.getInstance();
        }

        protected override void nextTaskDecider()
        {
            activeTask = taskManager.getNextBuildTask();

            if (activeTask == null)
            {
                wander();
            }
            else
            {
                processBuildTask();
            }
        }
        private void processBuildTask()
        {
            Console.WriteLine("PROCESS BUILD TASK : MOVING TO:" + activeTask.x + " " + activeTask.y);
            moveTo(activeTask.x * GameMain.gridSize + (GameMain.gridSize / 2), activeTask.y * GameMain.gridSize + (GameMain.gridSize / 2));
        }
        protected override void doWork()
        {
            activeTask.reduceTaskTime();
            if (activeTask.taskTimeLength <= 0)
            {
                TileObject oto = mapManager.getTileObjectAt(activeTask.x, activeTask.y, Materials.getInstance().getMaterialObject(activeTask.materialKey).floorLevel);

                oto.changeColourDone();
                activeTask = null;
                Console.WriteLine("WORK IS COMPLETE");
            }
        }
    }
}
