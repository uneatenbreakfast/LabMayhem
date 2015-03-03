using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabMayhem
{
   
    class Task
    {
        public enum TaskType
        {
            BUILD, EXPERIMENT
        }
        public TaskType taskType;
        public int materialKey;

        public int x;
        public int y;

        public int taskTimeLength;

        public Task(int col, int row)
        {
            x = col;
            y = row;
        }

        public void reduceTaskTime()
        {
            taskTimeLength--;
        }
    }
}
