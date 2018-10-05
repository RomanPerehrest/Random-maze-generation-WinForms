using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Collections.Generic;
using System.Threading;

namespace MazeSolverDemo
{

    public class frmMain : System.Windows.Forms.Form
    {
        Mehroz.MazeSolver m_Maze;
        public static int[,] m_iMaze;
        public static int m_iSize = 20;
        public static int m_iRowDimensions = 30;
        public static int m_iColDimensions = 50;
        int iSelectedX, iSelectedY;

        private PictureBox pictureBox1;
        private Label lblPath;
        private GroupBox grpAction;
        private RadioButton bDraw;
        private RadioButton bFind;
        private Button cmdReset;
        private Button button1;
        private Button cmdExit;
        private CheckBox chkDiagonal;
        private TextBox txt_Row;
        private TextBox txt_Collumn;
        private Button btn_SizeFiled;
        private GroupBox groupBox1;
        private Label label2;
        private Label label1;
        private Button button2;
        private Label label4;
        private Label label3;
        private GroupBox groupBox2;
        private Label lblPathCaption;

        /// <summary>
        /// конструктор змінної
        /// </summary>
        private System.ComponentModel.Container components = null;

        public frmMain()
        {
            InitializeComponent();
        }

        public MazeSolver.FindWay FindWay
        {
            get => default(MazeSolver.FindWay);
            set
            {
            }
        }

        public MazeSolver.GenerationMaze GenerationMaze
        {
            get => default(MazeSolver.GenerationMaze);
            set
            {
            }
        }

        /// <summary>
        /// Очищення будь-яких ресурсів що використовуються
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }


        #region згенерований код Windows Form Designer 
        /// <summary>
        /// підтримка дизайну
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblPath = new System.Windows.Forms.Label();
            this.grpAction = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.bFind = new System.Windows.Forms.RadioButton();
            this.bDraw = new System.Windows.Forms.RadioButton();
            this.cmdReset = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.cmdExit = new System.Windows.Forms.Button();
            this.chkDiagonal = new System.Windows.Forms.CheckBox();
            this.txt_Row = new System.Windows.Forms.TextBox();
            this.txt_Collumn = new System.Windows.Forms.TextBox();
            this.btn_SizeFiled = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblPathCaption = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.grpAction.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(29, 18);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1118, 626);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            // 
            // lblPath
            // 
            this.lblPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPath.Location = new System.Drawing.Point(6, 9);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(172, 33);
            this.lblPath.TabIndex = 3;
            // 
            // grpAction
            // 
            this.grpAction.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.grpAction.Controls.Add(this.button2);
            this.grpAction.Controls.Add(this.bFind);
            this.grpAction.Controls.Add(this.bDraw);
            this.grpAction.Location = new System.Drawing.Point(1171, 18);
            this.grpAction.Name = "grpAction";
            this.grpAction.Size = new System.Drawing.Size(187, 120);
            this.grpAction.TabIndex = 4;
            this.grpAction.TabStop = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button2.Location = new System.Drawing.Point(4, 79);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(177, 35);
            this.button2.TabIndex = 13;
            this.button2.Text = "Генерація лабіринту";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // bFind
            // 
            this.bFind.Location = new System.Drawing.Point(6, 45);
            this.bFind.Name = "bFind";
            this.bFind.Size = new System.Drawing.Size(124, 28);
            this.bFind.TabIndex = 1;
            this.bFind.Text = "Знайти шлях";
            this.bFind.CheckedChanged += new System.EventHandler(this.bFind_CheckedChanged);
            // 
            // bDraw
            // 
            this.bDraw.Checked = true;
            this.bDraw.Location = new System.Drawing.Point(6, 12);
            this.bDraw.Name = "bDraw";
            this.bDraw.Size = new System.Drawing.Size(173, 27);
            this.bDraw.TabIndex = 0;
            this.bDraw.TabStop = true;
            this.bDraw.Text = "Намалювати стіни";
            this.bDraw.CheckedChanged += new System.EventHandler(this.bDraw_CheckedChanged);
            // 
            // cmdReset
            // 
            this.cmdReset.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.cmdReset.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.cmdReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmdReset.Location = new System.Drawing.Point(1216, 158);
            this.cmdReset.Name = "cmdReset";
            this.cmdReset.Size = new System.Drawing.Size(85, 67);
            this.cmdReset.TabIndex = 6;
            this.cmdReset.Text = "Видалити";
            this.cmdReset.UseVisualStyleBackColor = false;
            this.cmdReset.Click += new System.EventHandler(this.cmdReset_Click);
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.button1.Location = new System.Drawing.Point(1204, 572);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(122, 28);
            this.button1.TabIndex = 7;
            this.button1.Text = "Про програму";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.cmdExit.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.cmdExit.Location = new System.Drawing.Point(1204, 616);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(122, 28);
            this.cmdExit.TabIndex = 8;
            this.cmdExit.Text = "Вихід";
            this.cmdExit.UseVisualStyleBackColor = false;
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // chkDiagonal
            // 
            this.chkDiagonal.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.chkDiagonal.Location = new System.Drawing.Point(1171, 241);
            this.chkDiagonal.Name = "chkDiagonal";
            this.chkDiagonal.Size = new System.Drawing.Size(187, 28);
            this.chkDiagonal.TabIndex = 9;
            this.chkDiagonal.Text = "Дозволити діагональ";
            this.chkDiagonal.CheckedChanged += new System.EventHandler(this.chkDiagonal_CheckedChanged);
            // 
            // txt_Row
            // 
            this.txt_Row.Location = new System.Drawing.Point(138, 43);
            this.txt_Row.Name = "txt_Row";
            this.txt_Row.Size = new System.Drawing.Size(40, 22);
            this.txt_Row.TabIndex = 10;
            // 
            // txt_Collumn
            // 
            this.txt_Collumn.Location = new System.Drawing.Point(136, 100);
            this.txt_Collumn.Name = "txt_Collumn";
            this.txt_Collumn.Size = new System.Drawing.Size(43, 22);
            this.txt_Collumn.TabIndex = 10;
            // 
            // btn_SizeFiled
            // 
            this.btn_SizeFiled.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn_SizeFiled.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_SizeFiled.Location = new System.Drawing.Point(33, 130);
            this.btn_SizeFiled.Name = "btn_SizeFiled";
            this.btn_SizeFiled.Size = new System.Drawing.Size(122, 46);
            this.btn_SizeFiled.TabIndex = 11;
            this.btn_SizeFiled.Text = "Підтвердити";
            this.btn_SizeFiled.UseVisualStyleBackColor = false;
            this.btn_SizeFiled.Click += new System.EventHandler(this.btn_SizeFiled_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txt_Row);
            this.groupBox1.Controls.Add(this.btn_SizeFiled);
            this.groupBox1.Controls.Add(this.txt_Collumn);
            this.groupBox1.Location = new System.Drawing.Point(1171, 373);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(187, 182);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Розмір поля";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 17);
            this.label4.TabIndex = 14;
            this.label4.Text = "(від 4 до 50)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 17);
            this.label3.TabIndex = 13;
            this.label3.Text = "(від 4 до 30)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 17);
            this.label2.TabIndex = 12;
            this.label2.Text = "Вкажіть колонку:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 17);
            this.label1.TabIndex = 12;
            this.label1.Text = "Вкажіть рядок:";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.groupBox2.Controls.Add(this.lblPath);
            this.groupBox2.Location = new System.Drawing.Point(1171, 303);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(187, 52);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            // 
            // lblPathCaption
            // 
            this.lblPathCaption.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblPathCaption.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblPathCaption.Location = new System.Drawing.Point(1167, 283);
            this.lblPathCaption.Name = "lblPathCaption";
            this.lblPathCaption.Size = new System.Drawing.Size(195, 17);
            this.lblPathCaption.TabIndex = 5;
            this.lblPathCaption.Text = "Поточний шлях";
            // 
            // frmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.BackColor = System.Drawing.Color.Snow;
            this.ClientSize = new System.Drawing.Size(1382, 673);
            this.Controls.Add(this.lblPathCaption);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.chkDiagonal);
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cmdReset);
            this.Controls.Add(this.grpAction);
            this.Controls.Add(this.pictureBox1);
            this.Name = "frmMain";
            this.Text = "Лабіринт";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.grpAction.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        /// <summary>
        /// Основна точка входу для програми
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.Run(new frmMain());
                
            }
            catch (Exception exp)
            {
                
                System.Windows.Forms.MessageBox.Show("Виникла помилка " + exp.Message);
            }
        }

        /// <summary>
        /// оновлення форми
        /// </summary>
        private void Form1_Load(object sender, System.EventArgs e) //форма лабіринту
        {
            m_Maze = new Mehroz.MazeSolver(m_iRowDimensions, m_iColDimensions);
            this.pictureBox1.Size = new System.Drawing.Size(m_iColDimensions * m_iSize + 3, m_iRowDimensions * m_iSize + 3);//ромір матриці і рамки
            m_iMaze = m_Maze.GetMaze;
            this.lblPath.Visible = false;
            this.lblPathCaption.Visible = false;
            this.groupBox2.Visible = false;

        }

        private void pictureBox1_Paint(object sender, System.Windows.Forms.PaintEventArgs e) //малювання сітки, стін, шляху, кулі
        {
            Graphics myGraphics = e.Graphics;
            for (int i = 0; i < m_iRowDimensions; i++)
                for (int j = 0; j < m_iColDimensions; j++)
                {
                    // малювання сітки
                    myGraphics.DrawRectangle(new Pen(Color.Black), j * m_iSize, i * m_iSize, m_iSize, m_iSize);
                    // малювання стін
                    if (m_iMaze[i, j] == 1)
                        myGraphics.FillRectangle(new SolidBrush(Color.DarkGray), j * m_iSize + 1, i * m_iSize + 1, m_iSize - 1, m_iSize - 1);
                    //малювання шляху
                    if (m_iMaze[i, j] == 100)
                        myGraphics.FillRectangle(new SolidBrush(Color.Cyan), j * m_iSize + 1, i * m_iSize + 1, m_iSize - 1, m_iSize - 1);
                }
            //малювання кулі
            myGraphics.FillEllipse(new SolidBrush(Color.DarkCyan), this.iSelectedX * m_iSize + 5, this.iSelectedY * m_iSize + 5, m_iSize - 10, m_iSize - 10);

        }

        public static void ResetMaze() //видалити поле
        {
            for (int i = 0; i < MazeSolverDemo.frmMain.m_iRowDimensions; i++)
            {
                for (int j = 0; j < MazeSolverDemo.frmMain.m_iColDimensions; j++)
                {
                    MazeSolverDemo.frmMain.m_iMaze[i, j] = 0;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e) //заповнення поля рандомним лабіринтом
        {
            ResetMaze();
            Random rand = new Random();
            // R - рядок, C - колонка
            // генерація рандомного R
            int r = rand.Next(m_iRowDimensions);
            while (r % 2 == 0)
            {
                r = rand.Next(m_iRowDimensions);
            }
            // генерація рандомного С
            int c = rand.Next(m_iColDimensions);
            while (c % 2 == 0)
            {
                c = rand.Next(m_iColDimensions);
            }
            MazeSolver.GenerationMaze.recursion(r, c);
            this.Refresh();
        }

        private void pictureBox1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) //пошук шляху
        {
            int iX = e.X / m_iSize;
            int iY = e.Y / m_iSize;
            if (iX < m_iColDimensions && iX >= 0 && iY < m_iRowDimensions && iY >= 0)
            {
                if (this.bDraw.Checked == true)
                {
                    m_iMaze = m_Maze.GetMaze;
                    if (m_iMaze[iY, iX] == 0)
                        m_iMaze[iY, iX] = 1;
                    else
                        m_iMaze[iY, iX] = 0;
                    this.Refresh();
                }
                else
                {
                    if (m_iMaze[iY, iX] == 100) //якщо шлях існує то
                    {

                        //рухатися крок за кроком до досягнення необхідної позиції
                        while (this.iSelectedX != iX || this.iSelectedY != iY)
                        {
                            Thread.Sleep(5);
                            m_iMaze[this.iSelectedY, this.iSelectedX] = 0;

                            if (this.iSelectedX - 1 >= 0 && this.iSelectedX - 1 < m_iColDimensions && m_iMaze[this.iSelectedY, this.iSelectedX - 1] == 100)
                                this.iSelectedX--;
                            else if (this.iSelectedX + 1 >= 0 && this.iSelectedX + 1 < m_iColDimensions && m_iMaze[this.iSelectedY, this.iSelectedX + 1] == 100)
                                this.iSelectedX++;
                            else if (this.iSelectedY - 1 >= 0 && this.iSelectedY - 1 < m_iRowDimensions && m_iMaze[this.iSelectedY - 1, this.iSelectedX] == 100)
                                this.iSelectedY--;
                            else if (this.iSelectedY + 1 >= 0 && this.iSelectedY + 1 < m_iRowDimensions && m_iMaze[this.iSelectedY + 1, this.iSelectedX] == 100)
                                this.iSelectedY++;

                            // рухатись по діагоналі
                            else if (this.iSelectedY + 1 >= 0 && this.iSelectedY + 1 < m_iRowDimensions && this.iSelectedX + 1 >= 0 && this.iSelectedX + 1 < m_iColDimensions && m_iMaze[this.iSelectedY + 1, this.iSelectedX + 1] == 100)
                            { this.iSelectedX++; this.iSelectedY++; }
                            else if (this.iSelectedY - 1 >= 0 && this.iSelectedY - 1 < m_iRowDimensions && this.iSelectedX + 1 >= 0 && this.iSelectedX + 1 < m_iColDimensions && m_iMaze[this.iSelectedY - 1, this.iSelectedX + 1] == 100)
                            { this.iSelectedX++; this.iSelectedY--; }
                            else if (this.iSelectedY + 1 >= 0 && this.iSelectedY + 1 < m_iRowDimensions && this.iSelectedX - 1 >= 0 && this.iSelectedX - 1 < m_iColDimensions && m_iMaze[this.iSelectedY + 1, this.iSelectedX - 1] == 100)
                            { this.iSelectedX--; this.iSelectedY++; }
                            else if (this.iSelectedY - 1 >= 0 && this.iSelectedY - 1 < m_iRowDimensions && this.iSelectedX - 1 >= 0 && this.iSelectedX - 1 < m_iColDimensions && m_iMaze[this.iSelectedY - 1, this.iSelectedX - 1] == 100)
                            { this.iSelectedX--; this.iSelectedY--; }

                            this.Refresh();
                        }
                    }
                }
            }
        }

        private void pictureBox1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e) //поточний шлях
        {
            int iY = e.Y / m_iSize;
            int iX = e.X / m_iSize;
            if (iX < m_iColDimensions && iX >= 0 && iY < m_iRowDimensions && iY >= 0)
            {
                m_iMaze = m_Maze.GetMaze;
                if (this.bDraw.Checked == false)
                {
                    int[,] iSolvedMaze = m_Maze.FindPath(iSelectedY, iSelectedX, iY, iX);
                    if (iSolvedMaze != null)
                    {
                        m_iMaze = iSolvedMaze;
                        this.lblPath.Text = "" + iSelectedY + "," + iSelectedX + " до " + iY + "," + iX;

                    }
                    else
                        this.lblPath.Text = "Шлях не знайдений";
                    this.Refresh();
                }
            }
        }

        private void bDraw_CheckedChanged(object sender, System.EventArgs e) //намалювати стіни
        {
            m_iMaze = m_Maze.GetMaze;
            this.lblPath.Visible = false;
            this.lblPathCaption.Visible = false;
            this.groupBox2.Visible = false;
            this.Refresh();
        }

        private void bFind_CheckedChanged(object sender, System.EventArgs e) //знайти шлях
        {
            this.m_Maze.AllowDiagonals = this.chkDiagonal.Checked;
            this.lblPath.Visible = true;
            this.lblPathCaption.Visible = true;
            this.groupBox2.Visible = true;
        }

        private void cmdReset_Click(object sender, System.EventArgs e) //очистити лабіринт
        {
            m_Maze = new Mehroz.MazeSolver(m_iRowDimensions, m_iColDimensions);
            m_Maze.AllowDiagonals = this.chkDiagonal.Checked;
            m_iMaze = m_Maze.GetMaze;
            this.Refresh();
        }

        private void button1_Click(object sender, System.EventArgs e) //Про програму
        {

            MessageBox.Show(
              "Програма 'Лабіринт'" +
            "\n 1. Намалюй стіни або згенеруй лабіринт. " +
            "\n 2. Знайди найкоротший шлях. ");
        }

        private void cmdExit_Click(object sender, System.EventArgs e) //Вихід
        {
            this.Close();
        }

        private void btn_SizeFiled_Click(object sender, EventArgs e) //надання розміра поля
        {
            string row = txt_Row.Text;
            string collumn = txt_Collumn.Text;
            int rowNum = 0;
            int collNum = 0;
            try
            {
                if (row != null && collumn != null)
                {
                    if (Int32.TryParse(row, out rowNum) && Int32.TryParse(collumn, out collNum))
                    {
                        if (rowNum > 30 || collNum > 50 || rowNum < 4 || collNum < 4)
                        {
                            throw new IndexOutOfRangeException("Розміри не припустимі!");
                        }
                        m_iRowDimensions = rowNum;
                        m_iColDimensions = collNum;
                        m_Maze = new Mehroz.MazeSolver(m_iRowDimensions, m_iColDimensions);
                        this.pictureBox1.Size = new System.Drawing.Size(m_iColDimensions * m_iSize + 3, m_iRowDimensions * m_iSize + 3);
                        m_iMaze = m_Maze.GetMaze;
                        this.lblPath.Visible = false;
                        this.lblPathCaption.Visible = false;
                        this.iSelectedX = 0;
                        this.iSelectedY = 0;
                    }
                    else
                    {
                        throw new Exception("Некоректне введення даних");
                    }

                }
            }
            catch (Exception r)
            {
                MessageBox.Show(r.Message);
            }

        }

        private void chkDiagonal_CheckedChanged(object sender, System.EventArgs e) //дозволити діагональ
        {
            m_Maze.AllowDiagonals = chkDiagonal.Checked;
        }


    }
}
