using System;
using System.Collections.Generic;
using System.Text;

namespace MazeSolverChangeNodeContents
{
    class ChangeNodeContents
    {
        /// <summary>
        /// Ця функція використовується для зміни вмісту даного вузла в даному лабіринті.
        /// </summary>
        public static void changeNodeContents(int[,] iMaze, int iNodeNo, int iNewValue)
        {
            int iCols = iMaze.GetLength(1);
            iMaze[iNodeNo / iCols, iNodeNo - iNodeNo / iCols * iCols] = iNewValue;
        }
    }
}
