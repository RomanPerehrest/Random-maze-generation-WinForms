using System;

namespace Mehroz
{
    /// <summary>

    /// constructors (������������):
    /// 	(int [,]): ������ 2D ����� �����
    /// 	( int Rows, int Cols )	�������� ������,����������� ������ ���������� �������� ������� �������� 
    /// 
    /// Properties (����������):
    /// 	Rows: ������� ����� � ��������� �������
    /// 	Cols: ������� ������� � ��������� �������
    /// 	Maze: ������� �������� ������� �� 2D-�����
    /// 	PathCharacter: ��� �������� / ���������� �������� ����� ���������� �������
    /// 	AllowDiagonal: �� ������������ ��������� �����
    /// 
    /// Indexers (�������):
    /// 	[i,j] = ��������������� ��� ������������ / ��������� �������� ��������
    /// 
    /// Public Methods (��������������� ������):
    ///		int[,] FindPath(int iFromY, int iFromX, int iToY, int iToX)
    /// 
    /// Private Methods ( ������� ������):
    ///		void GetNodeContents(int[,] iMaze, int iNodeNo)
    ///		void ChangeNodeContents(int[,] iMaze, int iNodeNo, int iNewValue)
    ///		int[,] Search(int iBeginningNode, int iEndingNode)
    /// 
    /// </summary>
    delegate void MazeChangedHandler(int iChanged, int jChanged);

    class MazeSolver
    {

        /// <summary>
        /// �������� / ����� �����
        /// </summary>
        int[,] m_iMaze;
        int m_iRows;
        int m_iCols;
        int iPath = 100;
        bool diagonal = false;
        public event MazeChangedHandler OnMazeChangedEvent;

        /// <summary>
        /// ����������� 1: ������ �������� ����� �����
        /// </summary>
        public MazeSolver(int[,] iMaze)
        {
            m_iMaze = iMaze;
            m_iRows = iMaze.GetLength(0);
            m_iCols = iMaze.GetLength(1);
        }

        /// <summary>
        /// ����������� 2: �������� ������ ��������, 
        /// ��� ����������� ���������������� ��� ������������ ������� ������� ��������
        /// </summary>
        public MazeSolver(int iRows, int iCols)
        {
            m_iMaze = new int[iRows, iCols];
            m_iRows = iRows;
            m_iCols = iCols;
        }

        /// <summary>
        /// ����������:
        /// </summary>
        public int Rows
        {
            get { return m_iRows; }
        }

        public int Cols
        {
            get { return m_iCols; }
        }
        public int[,] GetMaze
        {
            get { return m_iMaze; }
        }
        public int PathCharacter
        {
            get { return iPath; }
            set
            {
                if (value == 0)
                    throw new Exception("Invalid path character specified");
                else
                    iPath = value;
            }
        }
        public bool AllowDiagonals
        {
            get { return diagonal; }
            set { diagonal = value; }
        }


        /// <summary>
        /// ����������
        /// </summary>
        public int this[int iRow, int iCol]
        {
            get { return m_iMaze[iRow, iCol]; }
            set
            {
                m_iMaze[iRow, iCol] = value;
                if (this.OnMazeChangedEvent != null)  //����
                    OnMazeChangedEvent(iRow, iCol);
            }
        }

      
       

        /// <summary>
        /// �� ������� ������� ��������� ����������� ���� �� ����� �������
        /// � ������� � ������� ������ �� ����� �� ���������� ������
        /// �� ��������� "iPath" (����� ������ �� ��������� ���������� "PathCharacter")
        /// ���� ���� �����, ������� ������� ����
        /// </summary>
        public int[,] FindPath(int iFromY, int iFromX, int iToY, int iToX)
        {
            int iBeginningNode = iFromY * this.Cols + iFromX;
            int iEndingNode = iToY * this.Cols + iToX;
            return (Search(iBeginningNode, iEndingNode));
        }


        /// <summary>
        /// �������� ������� ��� ����� ��������� ����������� ����, �������������� ������
        /// ������ �� �������� ������� ������.
        /// ³� �������� ����� �� ������� ����� (2D ������� ������) � ��������� ��������
        /// </summary>
        private enum Status
        { Ready, Waiting, Processed }

        private int[,] Search(int iStart, int iStop)
        {
            const int empty = 0;

            int iRows = m_iRows;
            int iCols = m_iCols;
            int iMax = iRows * iCols;
            int[] Queue = new int[iMax];
            int[] Origin = new int[iMax];
            int iFront = 0, iRear = 0;  

            // ��������, �� ���� �������� �� ����� ����� (������)
            if (MazeSolvers.GetNodeContents.getNodeContents(m_iMaze, iStart) != empty || MazeSolvers.GetNodeContents.getNodeContents(m_iMaze, iStop) != empty)
            {
                return null;
            }

            //������� ���������� ����� ��� ��������� �������
            int[,] iMazeStatus = new int[iRows, iCols];
            // �������� �� ����� �����
            for (int i = 0; i < iRows; i++)
                for (int j = 0; j < iCols; j++)
                    iMazeStatus[i, j] = (int)Status.Ready;

            // ������ ���������� ����� �� Queue(�����)
            Queue[iRear] = iStart;
            Origin[iRear] = -1;
            iRear++;
            int iCurrent, iLeft, iRight, iTop, iDown, iRightUp, iRightDown, iLeftUp, iLeftDown;
            while (iFront != iRear) // ���� Queue �� �������
            {
                if (Queue[iFront] == iStop)     // ������� �������
                    break;

                iCurrent = Queue[iFront];

                iLeft = iCurrent - 1;
                if (iLeft >= 0 && iLeft / iCols == iCurrent / iCols)   // ���� ���� ����� ����
                    if (MazeSolvers.GetNodeContents.getNodeContents(m_iMaze, iLeft) == empty)   // ���� ���� ����� �������� (���� ����)
                        if (MazeSolvers.GetNodeContents.getNodeContents(iMazeStatus, iLeft) == (int)Status.Ready)   // ���� ���� ����� �������
                        {
                            Queue[iRear] = iLeft; //�������� � ����� Queue
                            Origin[iRear] = iCurrent;
                           MazeSolverChangeNodeContents.ChangeNodeContents.changeNodeContents(iMazeStatus, iLeft, (int)Status.Waiting);    // ������ ������ �� ����������
                            iRear++;
                        }

                iRight = iCurrent + 1;
                if (iRight < iMax && iRight / iCols == iCurrent / iCols)   // ���� ������ ����� ����
                    if (MazeSolvers.GetNodeContents.getNodeContents(m_iMaze, iRight) == empty)  // ���� ������ ����� �������� (���� ����)
                        if (MazeSolvers.GetNodeContents.getNodeContents(iMazeStatus, iRight) == (int)Status.Ready) // ���� ������ ����� �������
                        {
                            Queue[iRear] = iRight; //�������� � ����� Queue
                            Origin[iRear] = iCurrent;
                            MazeSolverChangeNodeContents.ChangeNodeContents.changeNodeContents(iMazeStatus, iRight, (int)Status.Waiting);  // ������ ������ �� ����������
                            iRear++;
                        }

                iTop = iCurrent - iCols;
                if (iTop >= 0)  // ���� ������ ����� ����
                    if (MazeSolvers.GetNodeContents.getNodeContents(m_iMaze, iTop) == empty)    // ���� ������ ����� �������� (���� ����)
                        if (MazeSolvers.GetNodeContents.getNodeContents(iMazeStatus, iTop) == (int)Status.Ready)     // ���� ������ ����� �������
                        {
                            Queue[iRear] = iTop;//�������� � ����� Queue
                            Origin[iRear] = iCurrent;
                            MazeSolverChangeNodeContents.ChangeNodeContents.changeNodeContents(iMazeStatus, iTop, (int)Status.Waiting); // ������ ������ �� ����������
                            iRear++;
                        }

                iDown = iCurrent + iCols;
                if (iDown < iMax)   // ���� ����� ����� ����
                    if (MazeSolvers.GetNodeContents.getNodeContents(m_iMaze, iDown) == empty)   //���� ����� ����� �������� (���� ����)
                        if (MazeSolvers.GetNodeContents.getNodeContents(iMazeStatus, iDown) == (int)Status.Ready)   // ���� ����� ����� �������
                        {
                            Queue[iRear] = iDown; //�������� � ����� Queue
                            Origin[iRear] = iCurrent;
                            MazeSolverChangeNodeContents.ChangeNodeContents.changeNodeContents(iMazeStatus, iDown, (int)Status.Waiting);// ������ ������ �� ����������
                            iRear++;
                        }
                if (diagonal == true) //���� �� �������
                {
                    iRightDown = iCurrent + iCols + 1;
                    if (iRightDown < iMax && iRightDown >= 0 && iRightDown / iCols == iCurrent / iCols + 1)     // ���� ���� ����� ������ �����
                        if (MazeSolvers.GetNodeContents.getNodeContents(m_iMaze, iRightDown) == empty)  //���� ����� �������� (���� ����)
                            if (MazeSolvers.GetNodeContents.getNodeContents(iMazeStatus, iRightDown) == (int)Status.Ready)  // ���� ����� �������
                            {
                                Queue[iRear] = iRightDown; //�������� � ����� Queue
                                Origin[iRear] = iCurrent;
                                MazeSolverChangeNodeContents.ChangeNodeContents.changeNodeContents(iMazeStatus, iRightDown, (int)Status.Waiting); // ������ ������ �� ����������
                                iRear++;
                            }

                    iRightUp = iCurrent - iCols + 1;
                    if (iRightUp >= 0 && iRightUp < iMax && iRightUp / iCols == iCurrent / iCols - 1)   // ���� ���� ������ ������ �����
                        if (MazeSolvers.GetNodeContents.getNodeContents(m_iMaze, iRightUp) == empty)   //���� ����� �������� (���� ����)
                            if (MazeSolvers.GetNodeContents.getNodeContents(iMazeStatus, iRightUp) == (int)Status.Ready)    //���� ����� �������
                            {
                                Queue[iRear] = iRightUp; //�������� � ����� Queue
                                Origin[iRear] = iCurrent;
                                MazeSolverChangeNodeContents.ChangeNodeContents.changeNodeContents(iMazeStatus, iRightUp, (int)Status.Waiting); // ������ ������ �� ����������
                                iRear++;
                            }

                    iLeftDown = iCurrent + iCols - 1;
                    if (iLeftDown < iMax && iLeftDown >= 0 && iLeftDown / iCols == iCurrent / iCols + 1)    // ���� ���� ����� ���� �����
                        if (MazeSolvers.GetNodeContents.getNodeContents(m_iMaze, iLeftDown) == empty)   //���� ����� �������� (���� ����)
                            if (MazeSolvers.GetNodeContents.getNodeContents(iMazeStatus, iLeftDown) == (int)Status.Ready)   //���� ����� �������
                            {
                                Queue[iRear] = iLeftDown; //�������� � ����� Queue
                                Origin[iRear] = iCurrent;
                                MazeSolverChangeNodeContents.ChangeNodeContents.changeNodeContents(iMazeStatus, iLeftDown, (int)Status.Waiting); // ������ ������ �� ����������
                                iRear++;
                            }

                    iLeftUp = iCurrent - iCols - 1;
                    if (iLeftUp >= 0 && iLeftUp < iMax && iLeftUp / iCols == iCurrent / iCols - 1)  // ���� ���� ������ ���� �����
                        if (MazeSolvers.GetNodeContents.getNodeContents(m_iMaze, iLeftUp) == empty)     //���� ����� �������� (���� ����)
                            if (MazeSolvers.GetNodeContents.getNodeContents(iMazeStatus, iLeftUp) == (int)Status.Ready) //���� ����� �������
                            {
                                Queue[iRear] = iLeftUp; //�������� � ����� Queue
                                Origin[iRear] = iCurrent;
                                MazeSolverChangeNodeContents.ChangeNodeContents.changeNodeContents(iMazeStatus, iLeftUp, (int)Status.Waiting); // ������ ������ �� ����������
                                iRear++;
                            }
                }


                // ������ ������ ��������� ����� �� ����������
                MazeSolverChangeNodeContents.ChangeNodeContents.changeNodeContents(iMazeStatus, iCurrent, (int)Status.Processed);
                iFront++;

            }

            //��������� ������ (�������) ��� ������
            int[,] iMazeSolved = new int[iRows, iCols];
            for (int i = 0; i < iRows; i++)
                for (int j = 0; j < iCols; j++)
                    iMazeSolved[i, j] = m_iMaze[i, j];

            //��������� ���� � ��������� �������
            iCurrent = iStop;
            MazeSolverChangeNodeContents.ChangeNodeContents.changeNodeContents(iMazeSolved, iCurrent, iPath);
            for (int i = iFront; i >= 0; i--)
            {
                if (Queue[i] == iCurrent)
                {
                    iCurrent = Origin[i];
                    if (iCurrent == -1)     // ������� ��������
                        return (iMazeSolved);
                    MazeSolverChangeNodeContents.ChangeNodeContents.changeNodeContents(iMazeSolved, iCurrent, iPath);
                }
            }

            // ���� �����
            return null;
        }
    }

}
