using System;
using System.Collections.Generic;
using System.Text;

namespace MazeSolvers
{
    class GetNodeContents
    {
        /// <summary>
        /// Функція використовується для отримання вмісту даного вузла у заданому лабіринті
        /// </summary>
        public static int getNodeContents(int[,] iMaze, int iNodeNo)
        {
            int iCols = iMaze.GetLength(1);
            return iMaze[iNodeNo / iCols, iNodeNo - iNodeNo / iCols * iCols];
        }
    }
}
