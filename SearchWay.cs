using System;

namespace Mehroz
{
    /// <summary>

    /// constructors (Конструктори):
    /// 	(int [,]): приймає 2D цілий масив
    /// 	( int Rows, int Cols )	ініціалізує розміри,індексатори можуть встановити значення окремих елементів 
    /// 
    /// Properties (Властивості):
    /// 	Rows: повертає рядки у поточному лабіринті
    /// 	Cols: повертає колонки у поточному лабіринті
    /// 	Maze: повертає поточний лабіринт як 2D-масив
    /// 	PathCharacter: щоб отримати / встановити значення шляху трасування символу
    /// 	AllowDiagonal: Чи допускаються діагональні шляхи
    /// 
    /// Indexers (індекси):
    /// 	[i,j] = використовується для встановлення / отримання елементів лабіринту
    /// 
    /// Public Methods (Загальнодоступні методи):
    ///		int[,] FindPath(int iFromY, int iFromX, int iToY, int iToX)
    /// 
    /// Private Methods ( Приватні методи):
    ///		void GetNodeContents(int[,] iMaze, int iNodeNo)
    ///		void ChangeNodeContents(int[,] iMaze, int iNodeNo, int iNewValue)
    ///		int[,] Search(int iBeginningNode, int iEndingNode)
    /// 
    /// </summary>
    delegate void MazeChangedHandler(int iChanged, int jChanged);

    class MazeSolver
    {

        /// <summary>
        /// Атрибути / члени класу
        /// </summary>
        int[,] m_iMaze;
        int m_iRows;
        int m_iCols;
        int iPath = 100;
        bool diagonal = false;
        public event MazeChangedHandler OnMazeChangedEvent;

        /// <summary>
        /// Конструктор 1: приймає двомірний цілий масив
        /// </summary>
        public MazeSolver(int[,] iMaze)
        {
            m_iMaze = iMaze;
            m_iRows = iMaze.GetLength(0);
            m_iCols = iMaze.GetLength(1);
        }

        /// <summary>
        /// Конструктор 2: ініціалізує розміри лабіринту, 
        /// далі індексатори використовуються для встановлення значень окремих елементів
        /// </summary>
        public MazeSolver(int iRows, int iCols)
        {
            m_iMaze = new int[iRows, iCols];
            m_iRows = iRows;
            m_iCols = iCols;
        }

        /// <summary>
        /// Властивості:
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
        /// Індексатор
        /// </summary>
        public int this[int iRow, int iCol]
        {
            get { return m_iMaze[iRow, iCol]; }
            set
            {
                m_iMaze[iRow, iCol] = value;
                if (this.OnMazeChangedEvent != null)  //подія
                    OnMazeChangedEvent(iRow, iCol);
            }
        }

      
       

        /// <summary>
        /// Ця публічна функція знаходить найкоротший шлях між двома точками
        /// в лабіринті і повертає рішення як масив із зазначеним шляхом
        /// за допомогою "iPath" (можна змінити за допомогою властивості "PathCharacter")
        /// якщо немає шляху, функція повертає нуль
        /// </summary>
        public int[,] FindPath(int iFromY, int iFromX, int iToY, int iToX)
        {
            int iBeginningNode = iFromY * this.Cols + iFromX;
            int iEndingNode = iToY * this.Cols + iToX;
            return (Search(iBeginningNode, iEndingNode));
        }


        /// <summary>
        /// Внутрішня функція для цього знаходить найкоротший шлях, використовуючи техніку
        /// подібно до широкого першого пошуку.
        /// Він призначає вузол до кожного вузла (2D елемент масиву) і застосовує алгоритм
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

            // перевіряє, чи дійсні початкові та кінцеві точки (відкриті)
            if (MazeSolvers.GetNodeContents.getNodeContents(m_iMaze, iStart) != empty || MazeSolvers.GetNodeContents.getNodeContents(m_iMaze, iStop) != empty)
            {
                return null;
            }

            //створює підроблений масив для зберігання статусу
            int[,] iMazeStatus = new int[iRows, iCols];
            // спочатку всі вузли готові
            for (int i = 0; i < iRows; i++)
                for (int j = 0; j < iCols; j++)
                    iMazeStatus[i, j] = (int)Status.Ready;

            // додати початковий вузол до Queue(черга)
            Queue[iRear] = iStart;
            Origin[iRear] = -1;
            iRear++;
            int iCurrent, iLeft, iRight, iTop, iDown, iRightUp, iRightDown, iLeftUp, iLeftDown;
            while (iFront != iRear) // поки Queue не порожній
            {
                if (Queue[iFront] == iStop)     // лабіринт вирішено
                    break;

                iCurrent = Queue[iFront];

                iLeft = iCurrent - 1;
                if (iLeft >= 0 && iLeft / iCols == iCurrent / iCols)   // якщо лівий вузол існує
                    if (MazeSolvers.GetNodeContents.getNodeContents(m_iMaze, iLeft) == empty)   // якщо лівий вузол відкритий (існує шлях)
                        if (MazeSolvers.GetNodeContents.getNodeContents(iMazeStatus, iLeft) == (int)Status.Ready)   // якщо лівий вузол готовий
                        {
                            Queue[iRear] = iLeft; //добавити в чергу Queue
                            Origin[iRear] = iCurrent;
                           MazeSolverChangeNodeContents.ChangeNodeContents.changeNodeContents(iMazeStatus, iLeft, (int)Status.Waiting);    // змінити статус на очікування
                            iRear++;
                        }

                iRight = iCurrent + 1;
                if (iRight < iMax && iRight / iCols == iCurrent / iCols)   // якщо правий вузол існує
                    if (MazeSolvers.GetNodeContents.getNodeContents(m_iMaze, iRight) == empty)  // якщо правий вузол відкритий (існує шлях)
                        if (MazeSolvers.GetNodeContents.getNodeContents(iMazeStatus, iRight) == (int)Status.Ready) // якщо правий вузол готовий
                        {
                            Queue[iRear] = iRight; //добавити в чергу Queue
                            Origin[iRear] = iCurrent;
                            MazeSolverChangeNodeContents.ChangeNodeContents.changeNodeContents(iMazeStatus, iRight, (int)Status.Waiting);  // змінити статус на очікування
                            iRear++;
                        }

                iTop = iCurrent - iCols;
                if (iTop >= 0)  // якщо верхній вузол існує
                    if (MazeSolvers.GetNodeContents.getNodeContents(m_iMaze, iTop) == empty)    // якщо верхній вузол відкритий (існує шлях)
                        if (MazeSolvers.GetNodeContents.getNodeContents(iMazeStatus, iTop) == (int)Status.Ready)     // якщо верхній вузол готовий
                        {
                            Queue[iRear] = iTop;//добавити в чергу Queue
                            Origin[iRear] = iCurrent;
                            MazeSolverChangeNodeContents.ChangeNodeContents.changeNodeContents(iMazeStatus, iTop, (int)Status.Waiting); // змінити статус на очікування
                            iRear++;
                        }

                iDown = iCurrent + iCols;
                if (iDown < iMax)   // якщо нижній вузол існує
                    if (MazeSolvers.GetNodeContents.getNodeContents(m_iMaze, iDown) == empty)   //якщо нижній вузол відкритий (існує шлях)
                        if (MazeSolvers.GetNodeContents.getNodeContents(iMazeStatus, iDown) == (int)Status.Ready)   // якщо нижній вузол готовий
                        {
                            Queue[iRear] = iDown; //добавити в чергу Queue
                            Origin[iRear] = iCurrent;
                            MazeSolverChangeNodeContents.ChangeNodeContents.changeNodeContents(iMazeStatus, iDown, (int)Status.Waiting);// змінити статус на очікування
                            iRear++;
                        }
                if (diagonal == true) //якщо по діагоналі
                {
                    iRightDown = iCurrent + iCols + 1;
                    if (iRightDown < iMax && iRightDown >= 0 && iRightDown / iCols == iCurrent / iCols + 1)     // якщо існує нижній правий вузол
                        if (MazeSolvers.GetNodeContents.getNodeContents(m_iMaze, iRightDown) == empty)  //якщо вузол відкритий (існує шлях)
                            if (MazeSolvers.GetNodeContents.getNodeContents(iMazeStatus, iRightDown) == (int)Status.Ready)  // якщо вузол готовий
                            {
                                Queue[iRear] = iRightDown; //добавити в чергу Queue
                                Origin[iRear] = iCurrent;
                                MazeSolverChangeNodeContents.ChangeNodeContents.changeNodeContents(iMazeStatus, iRightDown, (int)Status.Waiting); // змінити статус на очікування
                                iRear++;
                            }

                    iRightUp = iCurrent - iCols + 1;
                    if (iRightUp >= 0 && iRightUp < iMax && iRightUp / iCols == iCurrent / iCols - 1)   // якщо існує верхній правий вузол
                        if (MazeSolvers.GetNodeContents.getNodeContents(m_iMaze, iRightUp) == empty)   //якщо вузол відкритий (існує шлях)
                            if (MazeSolvers.GetNodeContents.getNodeContents(iMazeStatus, iRightUp) == (int)Status.Ready)    //якщо вузол готовий
                            {
                                Queue[iRear] = iRightUp; //добавити в чергу Queue
                                Origin[iRear] = iCurrent;
                                MazeSolverChangeNodeContents.ChangeNodeContents.changeNodeContents(iMazeStatus, iRightUp, (int)Status.Waiting); // змінити статус на очікування
                                iRear++;
                            }

                    iLeftDown = iCurrent + iCols - 1;
                    if (iLeftDown < iMax && iLeftDown >= 0 && iLeftDown / iCols == iCurrent / iCols + 1)    // якщо існує нижній лівий вузол
                        if (MazeSolvers.GetNodeContents.getNodeContents(m_iMaze, iLeftDown) == empty)   //якщо вузол відкритий (існує шлях)
                            if (MazeSolvers.GetNodeContents.getNodeContents(iMazeStatus, iLeftDown) == (int)Status.Ready)   //якщо вузол готовий
                            {
                                Queue[iRear] = iLeftDown; //добавити в чергу Queue
                                Origin[iRear] = iCurrent;
                                MazeSolverChangeNodeContents.ChangeNodeContents.changeNodeContents(iMazeStatus, iLeftDown, (int)Status.Waiting); // змінити статус на очікування
                                iRear++;
                            }

                    iLeftUp = iCurrent - iCols - 1;
                    if (iLeftUp >= 0 && iLeftUp < iMax && iLeftUp / iCols == iCurrent / iCols - 1)  // якщо існує верхній лівий вузол
                        if (MazeSolvers.GetNodeContents.getNodeContents(m_iMaze, iLeftUp) == empty)     //якщо вузол відкритий (існує шлях)
                            if (MazeSolvers.GetNodeContents.getNodeContents(iMazeStatus, iLeftUp) == (int)Status.Ready) //якщо вузол готовий
                            {
                                Queue[iRear] = iLeftUp; //добавити в чергу Queue
                                Origin[iRear] = iCurrent;
                                MazeSolverChangeNodeContents.ChangeNodeContents.changeNodeContents(iMazeStatus, iLeftUp, (int)Status.Waiting); // змінити статус на очікування
                                iRear++;
                            }
                }


                // змінити статус поточного вузла на оброблений
                MazeSolverChangeNodeContents.ChangeNodeContents.changeNodeContents(iMazeStatus, iCurrent, (int)Status.Processed);
                iFront++;

            }

            //створення масиву (лабіринт) для рішення
            int[,] iMazeSolved = new int[iRows, iCols];
            for (int i = 0; i < iRows; i++)
                for (int j = 0; j < iCols; j++)
                    iMazeSolved[i, j] = m_iMaze[i, j];

            //Створений шлях у вирішеному лабіринті
            iCurrent = iStop;
            MazeSolverChangeNodeContents.ChangeNodeContents.changeNodeContents(iMazeSolved, iCurrent, iPath);
            for (int i = iFront; i >= 0; i--)
            {
                if (Queue[i] == iCurrent)
                {
                    iCurrent = Origin[i];
                    if (iCurrent == -1)     // лабіринт вирішений
                        return (iMazeSolved);
                    MazeSolverChangeNodeContents.ChangeNodeContents.changeNodeContents(iMazeSolved, iCurrent, iPath);
                }
            }

            // немає шляху
            return null;
        }
    }

}
