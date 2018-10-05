using System;
using System.Collections.Generic;
using System.Text;

namespace MazeSolver
{
    public class GenerationMaze
    {  

        private static Random random = new Random();

        public static void Shuffle(ref List<int> list) // метод перемішування
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                int value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        static public int[] generateRandomDirections() // генерация рандомних напрямків
        {
            List<int> randoms = new List<int>();
            for (int i = 0; i < 4; i++)
                randoms.Add(i + 1);
            Shuffle(ref randoms);

            return randoms.ToArray();
        }   

        public static void recursion(int r, int c) //заповнення клітинок рекурсією
        {
            // 4 випадкових напрямків
            int[] randDirs = generateRandomDirections();
            // Перевірка кожного напрямка
            for (int i = 0; i < randDirs.Length; i++)
            {
                switch (randDirs[i])
                {
                    case 1: // Вверх
                            //　Чи є 2 клітинки вгорі, чи ні
                        if (r - 2 <= 0)
                            continue;
                        if (MazeSolverDemo.frmMain.m_iMaze[r - 2, c] != 1)
                        {
                            MazeSolverDemo.frmMain.m_iMaze[r - 2, c] = 1;
                            MazeSolverDemo.frmMain.m_iMaze[r - 1, c] = 1;
                            recursion(r - 2, c);
                        }
                        break;
                    case 2: // Вправо
                            // Чи є 2 клітинки праворуч чи ні
                        if (c + 2 >= MazeSolverDemo.frmMain.m_iColDimensions - 1)
                            continue;
                        if (MazeSolverDemo.frmMain.m_iMaze[r, c + 2] != 1)
                        {
                            MazeSolverDemo.frmMain.m_iMaze[r, c + 2] = 1;
                            MazeSolverDemo.frmMain.m_iMaze[r, c + 1] = 1;
                            recursion(r, c + 2);
                        }
                        break;
                    case 3: // Вниз
                            // Чи є 2 клітинки внизу чи ні
                        if (r + 2 >= MazeSolverDemo.frmMain.m_iRowDimensions - 1)
                            continue;
                        if (MazeSolverDemo.frmMain.m_iMaze[r + 2, c] != 1)
                        {
                            MazeSolverDemo.frmMain.m_iMaze[r + 2, c] = 1;
                            MazeSolverDemo.frmMain.m_iMaze[r + 1, c] = 1;
                            recursion(r + 2, c);
                        }
                        break;
                    case 4: // Вліво
                            // Чи є 2 клітинки зліва чи ні
                        if (c - 2 <= 0)
                            continue;
                        if (MazeSolverDemo.frmMain.m_iMaze[r, c - 2] != 1)
                        {
                            MazeSolverDemo.frmMain.m_iMaze[r, c - 2] = 1;
                            MazeSolverDemo.frmMain.m_iMaze[r, c - 1] = 1;
                            recursion(r, c - 2);
                        }
                        break;
                }
            }
        }
    }
}

