using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabMayhem
{
    class TaskManager
    {
        private static TaskManager thisTaskManager;

        private Materials materialsManager;

        private Queue<Task> builderTaskList = new Queue<Task>();
        private Queue<Task> scientistTaskList = new Queue<Task>();
        public TaskManager()
        {
            materialsManager = Materials.getInstance();
        }
        public static TaskManager getInstance()
        {
            if (thisTaskManager == null)
            {
                thisTaskManager = new TaskManager();
            }
            return thisTaskManager;
        }

        public void addBuildTask(int materialKey, int cx, int cy)
        {
            MaterialObject mo = materialsManager.getMaterialObject(materialKey);

            Task t = new Task(cx, cy);
            t.taskType = Task.TaskType.BUILD;
            t.materialKey = materialKey;
            t.taskTimeLength = mo.secondsToBuild * 60;

            builderTaskList.Enqueue(t);
        }

        public Task getNextBuildTask()
        {
            if (builderTaskList.Count() > 0)
            {
                return builderTaskList.Dequeue();
            }
            else
            {
                return null;
            }
        }
    }
}
