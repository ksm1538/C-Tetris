using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    public partial class Form1 : Form
    {
        ArrayList AA = new ArrayList();
        int[,] Dead1 = new int[20, 10];
        int NowNum;
        int SelNum;
        int image;
        int stage = 0;
        public static int NScore = 0;

        public Form1()
        {

            for (int i = 0; i < 20; i++)
                for (int j = 0; j < 10; j++)
                {
                    Dead1[i, j] = 0;
                }
                    
            InitializeComponent();
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);            
        }

        private void stageChange()
        {
            if(NScore>=2000)
                stage = 1;
            if (NScore >= 4000)
                stage = 2;
            if (NScore >= 6000)
                stage = 3;
            if (NScore >= 10000)
                stage = 4;
            switch (stage)
            {
                case 1: timer1.Interval = 400; break;
                case 2: timer1.Interval = 300; break;
                case 3: timer1.Interval = 200; break;
                case 4: timer1.Interval = 100; break;
            }
        }

        
        private void Form1_Load(object sender, EventArgs e)
        {
            firstblock();
            nextblock();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;  
            SolidBrush brushBack = new SolidBrush(Color.White); 
            Pen penLine = new Pen(Color.Black); 
            int nWidth = 25; 
            int nHeight = 25; 
            for (int nCntY = 0; nCntY * nHeight <= panel1.Height; nCntY++)
            {
                for (int nCntX = 0; nCntX * nHeight <= panel1.Height; nCntX++)
                {
                    g.FillRectangle(brushBack, nCntX * nWidth, nCntY * nHeight, nWidth, nHeight);
                    g.DrawRectangle(penLine, nCntX * nWidth, nCntY * nHeight, nWidth, nHeight);
                }
            }

            for (int i = 0; i < AA.Count; i++)      
            {
                g.FillRectangle(Brushes.BlueViolet, (Rectangle)AA[i]);
            }

            for(int i=0;i<20;i++)
                for(int j=0;j<10;j++)
                {
                    if(Dead1[i,j]==1)
                    {
                        Rectangle box=new Rectangle(new Point(j*25+1, i*25+1), new Size(25, 25));
                        g.FillRectangle(Brushes.Black, (Rectangle)box);
                    }
                }
        }
     
        private void GameOver()
        {
            for (int i = 0; i < 20; i++)       
                for (int j = 0; j < 10; j++)
                {
                    if (Dead1[i, j] == 1)
                    {
                        Rectangle boxbox = new Rectangle(new Point(j * 25 + 1, i * 25 + 1), new Size(25, 25));

                        for (int zz = 0; zz < AA.Count; zz++)
                        {
                            Rectangle Ex = (Rectangle)AA[zz];
                            if (Ex == boxbox)
                            {
                                timer1.Stop();
                                MessageBox.Show("게임 오버");
                                new Form3().ShowDialog();
                                return;
                            }
                        }
                    }
                }
        }
       
        private void firstblock()
        {
            NowNum = 0;
            Random number = new Random();
            SelNum = number.Next(1, 8);
            if (SelNum == 1)        // 정사각형 모양
            {
                AA.Add(new Rectangle(new Point(101, 1), new Size(25, 25)));
                AA.Add(new Rectangle(new Point(126, 1), new Size(25, 25)));
                AA.Add(new Rectangle(new Point(101, 26), new Size(25, 25)));
                AA.Add(new Rectangle(new Point(126, 26), new Size(25, 25)));
                GameOver();
            }
            else if (SelNum == 2)       //긴 모양
            {
                AA.Add(new Rectangle(new Point(101, 1), new Size(25, 25)));
                AA.Add(new Rectangle(new Point(101, 26), new Size(25, 25)));
                AA.Add(new Rectangle(new Point(101, 51), new Size(25, 25)));
                AA.Add(new Rectangle(new Point(101, 76), new Size(25, 25)));
                GameOver();
            }
            else if (SelNum == 3)       // L자 모양
            {
                AA.Add(new Rectangle(new Point(126, 51), new Size(25, 25)));
                AA.Add(new Rectangle(new Point(101, 1), new Size(25, 25)));
                AA.Add(new Rectangle(new Point(101, 26), new Size(25, 25)));
                AA.Add(new Rectangle(new Point(101, 51), new Size(25, 25)));
                GameOver();
            }
            else if (SelNum == 4)       // J자 모양
            {
                AA.Add(new Rectangle(new Point(101, 51), new Size(25, 25)));
                AA.Add(new Rectangle(new Point(126, 1), new Size(25, 25)));
                AA.Add(new Rectangle(new Point(126, 26), new Size(25, 25)));
                AA.Add(new Rectangle(new Point(126, 51), new Size(25, 25)));
                GameOver();
            }
            else if (SelNum == 5)
            {
                AA.Add(new Rectangle(new Point(126, 1), new Size(25, 25)));
                AA.Add(new Rectangle(new Point(101, 26), new Size(25, 25)));
                AA.Add(new Rectangle(new Point(126, 26), new Size(25, 25)));
                AA.Add(new Rectangle(new Point(151, 26), new Size(25, 25)));
                GameOver();
            }
            else if (SelNum == 6)
            {
                AA.Add(new Rectangle(new Point(101, 1), new Size(25, 25)));
                AA.Add(new Rectangle(new Point(101, 26), new Size(25, 25)));
                AA.Add(new Rectangle(new Point(126, 26), new Size(25, 25)));
                AA.Add(new Rectangle(new Point(126, 51), new Size(25, 25)));
                GameOver();
            }
            else if (SelNum == 7)
            {
                AA.Add(new Rectangle(new Point(126, 1), new Size(25, 25)));
                AA.Add(new Rectangle(new Point(126, 26), new Size(25, 25)));
                AA.Add(new Rectangle(new Point(101, 26), new Size(25, 25)));
                AA.Add(new Rectangle(new Point(101, 51), new Size(25, 25)));
                GameOver();
            }
            nextblock();
        }
        private void nextblock()
        {
            Random number = new Random();
            image = number.Next(1, 8);

           
            if (image == 1)
                pictureBox1.Load(@"./Resources/1.png");
            else if(image == 2)
                pictureBox1.Load(@"./Resources/2.png");
            else if(image == 3)
                pictureBox1.Load(@"./Resources/3.png");
            else if(image == 4)
                pictureBox1.Load(@"./Resources/4.png");
            else if(image == 5)
                pictureBox1.Load(@"./Resources/5.png");
            else if(image == 6)
                pictureBox1.Load(@"./Resources/6.png");
            else if(image == 7)
                pictureBox1.Load(@"./Resources/7.png");
        }
        private void BlockCreate()
        {
            NowNum = 0;
            SelNum = image;

            if(SelNum == 1)        // 정사각형 모양
            {
                AA.Add(new Rectangle(new Point(101, 1), new Size(25, 25)));
                AA.Add(new Rectangle(new Point(126, 1), new Size(25, 25)));
                AA.Add(new Rectangle(new Point(101, 26), new Size(25, 25)));
                AA.Add(new Rectangle(new Point(126, 26), new Size(25, 25)));
                GameOver();
            }
            else if(SelNum == 2)       //긴 모양
            {
                AA.Add(new Rectangle(new Point(101, 1), new Size(25, 25)));
                AA.Add(new Rectangle(new Point(101, 26), new Size(25, 25)));
                AA.Add(new Rectangle(new Point(101, 51), new Size(25, 25)));
                AA.Add(new Rectangle(new Point(101, 76), new Size(25, 25)));
                GameOver();
            }
            else if(SelNum == 3)       // L자 모양
            {
                AA.Add(new Rectangle(new Point(126, 51), new Size(25, 25)));
                AA.Add(new Rectangle(new Point(101, 1), new Size(25, 25)));
                AA.Add(new Rectangle(new Point(101, 26), new Size(25, 25)));
                AA.Add(new Rectangle(new Point(101, 51), new Size(25, 25)));
                GameOver();
            }
            else if(SelNum == 4)       // J자 모양
            {
                AA.Add(new Rectangle(new Point(101, 51), new Size(25, 25)));
                AA.Add(new Rectangle(new Point(126, 1), new Size(25, 25)));
                AA.Add(new Rectangle(new Point(126, 26), new Size(25, 25)));
                AA.Add(new Rectangle(new Point(126, 51), new Size(25, 25)));
                GameOver();
            }
            else if(SelNum == 5)
            {
                AA.Add(new Rectangle(new Point(126, 1), new Size(25, 25)));
                AA.Add(new Rectangle(new Point(101, 26), new Size(25, 25)));
                AA.Add(new Rectangle(new Point(126, 26), new Size(25, 25)));
                AA.Add(new Rectangle(new Point(151, 26), new Size(25, 25)));
                GameOver();
            }
            else if (SelNum == 6)
            {
                AA.Add(new Rectangle(new Point(101, 1), new Size(25, 25)));
                AA.Add(new Rectangle(new Point(101, 26), new Size(25, 25)));
                AA.Add(new Rectangle(new Point(126, 26), new Size(25, 25)));
                AA.Add(new Rectangle(new Point(126, 51), new Size(25, 25)));
                GameOver();
            }
            else if (SelNum == 7)
            {
                AA.Add(new Rectangle(new Point(126, 1), new Size(25, 25)));
                AA.Add(new Rectangle(new Point(126, 26), new Size(25, 25)));
                AA.Add(new Rectangle(new Point(101, 26), new Size(25, 25)));
                AA.Add(new Rectangle(new Point(101, 51), new Size(25, 25)));
                GameOver();
            }
            nextblock();
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            for (int i = 0; i < AA.Count; i++)
            {
                Rectangle box = (Rectangle)AA[i];
                box.Y += 25;
                AA[i] = box;
            }
            Rectangle aas = (Rectangle)AA[0];
            Out();
            Clear();
            stageChange();
            panel1.Invalidate();
            label4.Text = stage.ToString();
            label5.Text = NScore.ToString();
            label6.Text = timer1.Interval.ToString()+" msec";
        }

        private void Clear()            
        {
            
            for (int i = 0; i < 20; i++)
            {
                int sum = 0;
                for (int j = 0; j < 10; j++)
                {
                    sum += Dead1[i, j];
                }
                if (sum == 10)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        Dead1[i, j] = 0;
                    }
                    NScore += 1000;
                    Down(i);
                }
            }
        }
        private void Down(int a)        
        {
           for(int i=20;i!=0;i--)
            {
                if (i > a)
                    continue;
                for (int j=0;j<10;j++)
                {     
                    Dead1[i,j] = Dead1[i-1, j];
                }
            }
            Clear();
        }
        private void Out()
        {
            
            Rectangle box = (Rectangle)AA[0];               //블록의 가장 낮은 위치를 파악 하는 곳
            int max = box.Y;
            for (int i = 0; i < AA.Count - 1; i++)
            {
                Rectangle next = (Rectangle)AA[i + 1];

                if (next.Y > max)
                {
                        max = next.Y;
                }

            }
            if (max >= 475)                                     // 바닥에 닿았을 경우
            {
                Rectangle box1;
                int a, b;
                for (int i = 0; i < AA.Count; i++)
                {
                    box1 = (Rectangle)AA[i];
                    a = (box1.X / 25);
                    b = (box1.Y / 25);
                    
                    Dead1[b, a] = 1;   
                }
                AA.Clear();
                BlockCreate();
                return;
            }

            for (int i = 0; i < 20; i++)                                                                                                        // 블록에 닿았을 경우
                for (int j = 0; j < 10; j++)
                {
                    if (Dead1[i, j] == 1)
                    {
                        Rectangle boxbox = new Rectangle(new Point(j * 25 + 1, i * 25 + 1), new Size(25, 25));

                        for (int zz = 0; zz < AA.Count; zz++)
                        {
                            Rectangle Ex = (Rectangle)AA[zz];
                            Ex.Y = Ex.Y + 25;
                            if (Ex == boxbox)
                            {
                                int a, b;
                                for (int t = 0; t < AA.Count; t++)
                                {
                                    Rectangle box3 = (Rectangle)AA[t];
                                    a = (box3.X / 25);
                                    b = (box3.Y / 25);

                                    Dead1[b, a] = 1;
                                }
                                AA.Clear();
                                BlockCreate();
                                return;
                            }
                        }
                    }
                }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {

        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)          
        {
            switch (e.KeyCode)
            {
                case Keys.Space:

                    if (SelNum == 2)        // 일자모양 변환
                    {
                        label3.Text = NowNum.ToString();
                        if (NowNum == 0)
                        {
                            Rectangle ABC = (Rectangle)AA[0];
                            if (ABC.X - 75 < 10)
                                break;
                            for (int i = 0; i < 20; i++)
                                for (int j = 0; j < 10; j++)
                                {
                                    if (Dead1[i, j] == 1)
                                    {
                                        Rectangle boxbox = new Rectangle(new Point(j * 25 + 1, i * 25 + 1), new Size(25, 25));
                                        Rectangle Ex = (Rectangle)AA[0];

                                        Ex.X = Ex.X - 25;
                                        if (Ex == boxbox)
                                        {
                                            return;
                                        }

                                        Ex.X = Ex.X - 25;
                                        if (Ex == boxbox)
                                        {
                                            return;
                                        }

                                        Ex.X = Ex.X - 25;
                                        if (Ex == boxbox)
                                        {
                                            return;
                                        }
                                    }
                                }

                            for (int i = 1; i < AA.Count; i++)
                            {
                                Rectangle AAA = (Rectangle)AA[i];
                                AAA.X = ABC.X - (i * 25);
                                AAA.Y = ABC.Y;
                                AA[i] = AAA;
                            }
                            NowNum = 1;
                            break;
                        }

                        if (NowNum == 1)
                        {
                            Rectangle ABC = (Rectangle)AA[0];

                            if (ABC.Y + 75 > 475)
                                break;
                            for (int i = 0; i < 20; i++)
                                for (int j = 0; j < 10; j++)
                                {
                                    if (Dead1[i, j] == 1)
                                    {
                                        Rectangle boxbox = new Rectangle(new Point(j * 25 + 1, i * 25 + 1), new Size(25, 25));
                                        Rectangle Ex = (Rectangle)AA[0];

                                        Ex.Y = Ex.Y + 25;
                                        if (Ex == boxbox)
                                        {
                                            return;
                                        }

                                        Ex.Y = Ex.Y + 25;
                                        if (Ex == boxbox)
                                        {
                                            return;
                                        }

                                        Ex.Y = Ex.Y + 25;
                                        if (Ex == boxbox)
                                        {
                                            return;
                                        }
                                    }
                                }
                            for (int i = 1; i < AA.Count; i++)
                            {
                                Rectangle AAA = (Rectangle)AA[i];
                                AAA.Y = ABC.Y + (i * 25);
                                AAA.X = ABC.X;
                                AA[i] = AAA;
                            }
                            NowNum = 0;
                            break;
                        }
                    }

                    if (SelNum == 3)       //L자 모양 변환
                    {
                        if (NowNum == 0)  
                        {
                            Rectangle check = (Rectangle)AA[0];
                            check.X += 25;
                            if (check.X > 205)
                                return;
                            for (int i = 0; i < 20; i++)
                            {
                                for (int j = 0; j < 10; j++)
                                {
                                    if (Dead1[i, j] == 1)
                                    {
                                        Rectangle boxbox = new Rectangle(new Point(j * 25 + 1, i * 25 + 1), new Size(25, 25));
                                        Rectangle Ex = (Rectangle)AA[0];
                                        Ex.Y -= 25;
                                        if (Ex == boxbox)
                                            return;
                                        Ex.X += 25;
                                        if (Ex == boxbox)
                                            return;
                                        Ex.X += 25;
                                        if (Ex == boxbox)
                                            return;
                                    }
                                }
                            }
                            Rectangle M = (Rectangle)AA[1];

                            AA[0] = M;

                            M.Y -= 25;
                            AA[1] = M;

                            M.X += 25;
                            AA[2] = M;

                            M.X += 25;
                            AA[3] = M;


                            NowNum = 1;
                            break;
                        }

                        if (NowNum == 1)
                        {
                            Rectangle check = (Rectangle)AA[0];
                            check.Y += 25;
                            if (check.Y >= 475)
                                return;
                            for (int i = 0; i < 20; i++)
                            {
                                for (int j = 0; j < 10; j++)
                                {
                                    if (Dead1[i, j] == 1)
                                    {
                                        Rectangle boxbox = new Rectangle(new Point(j * 25 + 1, i * 25 + 1), new Size(25, 25));
                                        Rectangle Ex = (Rectangle)AA[0];
                                        Ex.X += 25;
                                        if (Ex == boxbox)
                                            return;
                                        Ex.Y += 25;
                                        if (Ex == boxbox)
                                            return;
                                    }
                                }
                            }

                            Rectangle M = (Rectangle)AA[1];

                            AA[0] = M;

                            M.X += 25;
                            AA[1] = M;

                            M.Y += 25;
                            AA[2] = M;

                            M.Y += 25;
                            AA[3] = M;

                            NowNum = 2;
                            break;
                        }
                        if (NowNum == 2)
                        {
                            Rectangle check = (Rectangle)AA[0];
                            check.X -= 25;
                            if (check.X <= 0)
                                return;
                            for (int i = 0; i < 20; i++)
                            {
                                for (int j = 0; j < 10; j++)
                                {
                                    if (Dead1[i, j] == 1)
                                    {
                                        Rectangle boxbox = new Rectangle(new Point(j * 25 + 1, i * 25 + 1), new Size(25, 25));
                                        Rectangle Ex = (Rectangle)AA[2];
                                        Ex.X -= 25;
                                        if (Ex == boxbox)
                                            return;
                                        Ex.X -= 25;
                                        if (Ex == boxbox)
                                            return;
                                    }
                                }
                            }
                            Rectangle M = (Rectangle)AA[1];
                            AA[0] = M;

                            M.Y += 25;
                            AA[1] = M;

                            M.X -= 25;
                            AA[2] = M;

                            M.X -= 25;
                            AA[3] = M;

                            NowNum = 3;
                            break;
                        }
                        if (NowNum == 3)
                        {
                            for (int i = 0; i < 20; i++)
                            {
                                for (int j = 0; j < 10; j++)
                                {
                                    if (Dead1[i, j] == 1)
                                    {
                                        Rectangle boxbox = new Rectangle(new Point(j * 25 + 1, i * 25 + 1), new Size(25, 25));
                                        Rectangle Ex = (Rectangle)AA[1];
                                        Ex.X -= 25;
                                        if (Ex == boxbox)
                                            return;
                                        Ex.Y -= 25;
                                        if (Ex == boxbox)
                                            return;
                                        Ex.Y -= 25;
                                        if (Ex == boxbox)
                                            return;
                                    }
                                }
                            }

                            Rectangle M = (Rectangle)AA[1];
                            AA[0] = M;

                            M.X -= 25;
                            AA[1] = M;

                            M.Y -= 25;
                            AA[2] = M;

                            M.Y -= 25;
                            AA[3] = M;

                            NowNum = 0;
                            break;
                        }
                    }
                    if (SelNum == 4)
                    {
                        if (NowNum == 0)  //
                        {
                            Rectangle check = (Rectangle)AA[1];
                            check.X += 25;
                            if (check.X > 205)
                                return;

                            for (int i = 0; i < 20; i++)
                            {
                                for (int j = 0; j < 10; j++)
                                {
                                    if (Dead1[i, j] == 1)
                                    {
                                        Rectangle boxbox = new Rectangle(new Point(j * 25 + 1, i * 25 + 1), new Size(25, 25));
                                        Rectangle Ex = (Rectangle)AA[1];
                                        Ex.X += 25;
                                        if (Ex == boxbox)
                                            return;
                                        Ex.X += 25;
                                        if (Ex == boxbox)
                                            return;
                                    }
                                }
                            }

                            Rectangle M = (Rectangle)AA[0];
                            AA[1] = AA[0];
                            M.Y -= 25;
                            AA[0] = M;
                            M = (Rectangle)AA[1];

                            M.X += 25;
                            AA[2] = M;

                            M.X += 25;
                            AA[3] = M;

                            NowNum = 1;
                            break;
                        }

                        if (NowNum == 1)
                        {
                            Rectangle check = (Rectangle)AA[1];
                            check.Y += 25;
                            if (check.Y >= 475)
                                return;
                            for (int i = 0; i < 20; i++)
                            {
                                for (int j = 0; j < 10; j++)
                                {
                                    if (Dead1[i, j] == 1)
                                    {
                                        Rectangle boxbox = new Rectangle(new Point(j * 25 + 1, i * 25 + 1), new Size(25, 25));
                                        Rectangle Ex = (Rectangle)AA[0];
                                        Ex.X += 25;
                                        if (Ex == boxbox)
                                            return;
                                        Ex = (Rectangle)AA[1];
                                        Ex.Y += 25;
                                        if (Ex == boxbox)
                                            return;
                                    }
                                }
                            }

                            Rectangle M = (Rectangle)AA[0];
                            AA[1] = AA[0];

                            M.X += 25;
                            AA[0] = M;

                            M = (Rectangle)AA[1];
                            M.Y += 25;
                            AA[2] = M;

                            M.Y += 25;
                            AA[3] = M;

                            NowNum = 2;
                            break;
                        }
                        if (NowNum == 2)        
                        {
                            Rectangle check = (Rectangle)AA[1];
                            check.X -= 25;
                            if (check.X <= 0)
                                return;
                            for (int i = 0; i < 20; i++)
                            {
                                for (int j = 0; j < 10; j++)
                                {
                                    if (Dead1[i, j] == 1)
                                    {
                                        Rectangle boxbox = new Rectangle(new Point(j * 25 + 1, i * 25 + 1), new Size(25, 25));
                                        Rectangle Ex = (Rectangle)AA[0];
                                        Ex.Y += 25;
                                        if (Ex == boxbox)
                                            return;
                                        Ex = (Rectangle)AA[1];
                                        Ex.X -= 25;
                                        if (Ex == boxbox)
                                            return;
                                    }
                                }
                            }
                            Rectangle M = (Rectangle)AA[0];
                            AA[1] = AA[0];

                            M.Y += 25;
                            AA[0] = M;

                            M = (Rectangle)AA[1];
                            M.X -= 25;
                            AA[2] = M;

                            M.X -= 25;
                            AA[3] = M;

                            NowNum = 3;
                            break;
                        }
                        if (NowNum == 3)
                        {
                            for (int i = 0; i < 20; i++)
                            {
                                for (int j = 0; j < 10; j++)
                                {
                                    if (Dead1[i, j] == 1)
                                    {
                                        Rectangle boxbox = new Rectangle(new Point(j * 25 + 1, i * 25 + 1), new Size(25, 25));
                                        Rectangle Ex = (Rectangle)AA[0];
                                        Ex.X -= 25;
                                        if (Ex == boxbox)
                                            return;
                                        Ex = (Rectangle)AA[1];
                                        Ex.Y -= 25;
                                        if (Ex == boxbox)
                                            return;
                                    }
                                }
                            }

                            Rectangle M = (Rectangle)AA[0];
                            AA[1] = AA[0];
                            M.X -= 25;
                            AA[0] = M;

                            M = (Rectangle)AA[1];
                            M.Y -= 25;
                            AA[2] = M;

                            M.Y -= 25;
                            AA[3] = M;

                            NowNum = 0;
                            break;
                        }
                    }
                    
                    if(SelNum==5)
                    {
                        if (NowNum == 0)  
                        {
                            Rectangle check = (Rectangle)AA[2];
                            check.Y += 25;
                            if (check.X > 205)
                                return;

                            for (int i = 0; i < 20; i++)
                            {
                                for (int j = 0; j < 10; j++)
                                {
                                    if (Dead1[i, j] == 1)
                                    {
                                        Rectangle boxbox = new Rectangle(new Point(j * 25 + 1, i * 25 + 1), new Size(25, 25));
                                        Rectangle Ex = (Rectangle)AA[2];
                                        Ex.Y += 25;
                                        if (Ex == boxbox)
                                            return;
                                    }
                                }
                            }

                            Rectangle M = (Rectangle)AA[0];
                            M.X -= 25;
                            M.Y -= 25;
                            AA[1] = M;

                            M.Y += 25;
                            AA[2] = M;

                            M.Y += 25;
                            AA[3] = M;

                            NowNum = 1;
                            break;
                        }

                        if (NowNum == 1)
                        {
                            Rectangle check = (Rectangle)AA[2];
                            check.X += 25;
                            if (check.X >= 205)
                                return;
                            for (int i = 0; i < 20; i++)
                            {
                                for (int j = 0; j < 10; j++)
                                {
                                    if (Dead1[i, j] == 1)
                                    {
                                        Rectangle boxbox = new Rectangle(new Point(j * 25 + 1, i * 25 + 1), new Size(25, 25));
                                        Rectangle Ex = (Rectangle)AA[2];
                                        Ex.X -= 25;
                                        if (Ex == boxbox)
                                            return;
                                    }
                                }
                            }

                            Rectangle M = (Rectangle)AA[0];
                            M.X += 25;
                            M.Y -= 25;

                            AA[1] = M;

                            M.X -= 25;
                            AA[2]=M;

                            M.X -= 25;
                            AA[3] = M;

                            NowNum = 2;
                            break;
                        }
                        if (NowNum == 2)        
                        {
                            Rectangle check = (Rectangle)AA[1];
                            check.Y -= 50;
                            if (check.Y >= 475)
                                return;
                            for (int i = 0; i < 20; i++)
                            {
                                for (int j = 0; j < 10; j++)
                                {
                                    if (Dead1[i, j] == 1)
                                    {
                                        Rectangle boxbox = new Rectangle(new Point(j * 25 + 1, i * 25 + 1), new Size(25, 25));
                                        Rectangle Ex = (Rectangle)AA[1];
                                        Ex.Y -= 25;
                                        if (Ex == boxbox)
                                            return;
                                        Ex.Y += 50;
                                        if (Ex == boxbox)
                                            return;
                                    }
                                }
                            }

                            Rectangle M = (Rectangle)AA[0];
                            M.X += 25;
                            M.Y += 25;
                            AA[1] = M;

                            M.Y -= 25;
                            AA[2] = M;

                            M.Y -= 25;
                            AA[3] = M;

                            NowNum = 3;
                            break;
                        }
                        if (NowNum == 3)        
                        {
                            Rectangle check = (Rectangle)AA[0];
                            check.X -= 25;
                            if (check.X <= 0)
                                return;
                            for (int i = 0; i < 20; i++)
                            {
                                for (int j = 0; j < 10; j++)
                                {
                                    if (Dead1[i, j] == 1)
                                    {
                                        Rectangle boxbox = new Rectangle(new Point(j * 25 + 1, i * 25 + 1), new Size(25, 25));
                                        Rectangle Ex = (Rectangle)AA[0];
                                        Ex.Y += 25;
                                        if (Ex == boxbox)
                                            return;
                                        Ex.X -= 25;
                                        if (Ex == boxbox)
                                            return;
                                    }
                                }
                            }

                            Rectangle M = (Rectangle)AA[0];
                            M.X -= 25;
                            M.Y += 25;
                            AA[1] = M;

                            M.X += 25;
                            AA[2] = M;

                            M.X += 25;
                            AA[3] = M;
                            
                            NowNum = 0;
                            break;
                        }
                    }
                    if (SelNum == 6)
                    {
                        if (NowNum == 0)  
                        {
                            Rectangle check = (Rectangle)AA[2];
                            check.X += 25;
                            if (check.X > 205)
                                return;

                            for (int i = 0; i < 20; i++)
                            {
                                for (int j = 0; j < 10; j++)
                                {
                                    if (Dead1[i, j] == 1)
                                    {
                                        Rectangle boxbox = new Rectangle(new Point(j * 25 + 1, i * 25 + 1), new Size(25, 25));
                                        Rectangle Ex = (Rectangle)AA[3];
                                        Ex.X -= 25;
                                        if (Ex == boxbox)
                                            return;
                                        Ex.X += 50;
                                        Ex.Y -= 25;
                                        if (Ex == boxbox)
                                            return;
                                    }
                                }
                            }

                            Rectangle M = (Rectangle)AA[3];
                            AA[1] = M;
                            M.X -= 25;
                            AA[0] = M;
                            M = (Rectangle)AA[2];
                            M.X += 25;
                            AA[3] = M;
                            
                            NowNum = 1;
                            break;
                        }

                        if (NowNum == 1)
                        {
                            for (int i = 0; i < 20; i++)
                            {
                                for (int j = 0; j < 10; j++)
                                {
                                    if (Dead1[i, j] == 1)
                                    {
                                        Rectangle boxbox = new Rectangle(new Point(j * 25 + 1, i * 25 + 1), new Size(25, 25));
                                        Rectangle Ex = (Rectangle)AA[2];
                                        Ex.X -= 25;
                                        if (Ex == boxbox)
                                            return;
                                        Ex.Y -= 25;
                                        if (Ex == boxbox)
                                            return;
                                    }
                                }
                            }
                            
                            Rectangle M = (Rectangle)AA[2];
                            M.X -= 25;
                            AA[1] = M;
                            M.Y -= 25;
                            AA[0] = M;
                            M = (Rectangle)AA[2];
                            M.Y += 25;
                            AA[3] = M;
                            
                            NowNum = 0;
                            break;
                        }
                    }

                    if (SelNum == 7)
                    {
                        if (NowNum == 0) 
                        {
                            Rectangle check = (Rectangle)AA[2];
                            check.X -= 25;
                            if (check.X <= 0)
                                return;

                            for (int i = 0; i < 20; i++)
                            {
                                for (int j = 0; j < 10; j++)
                                {
                                    if (Dead1[i, j] == 1)
                                    {
                                        Rectangle boxbox = new Rectangle(new Point(j * 25 + 1, i * 25 + 1), new Size(25, 25));
                                        Rectangle Ex = (Rectangle)AA[2];
                                        Ex.X -= 25;
                                        if (Ex == boxbox)
                                            return;
                                        Ex.X += 50;
                                        Ex.Y -= 25;
                                        if (Ex == boxbox)
                                            return;
                                    }
                                }
                            }

                            Rectangle M = (Rectangle)AA[2];
                            AA[1] = M;
                            M.X += 25;
                            AA[0] = M;
                            M = (Rectangle)AA[2];
                            M.Y -= 25;
                            AA[2] = M;
                            M.X -= 25;
                            AA[3] = M;

                            NowNum = 1;
                            break;
                        }

                        if (NowNum == 1)
                        {
                            Rectangle check = (Rectangle)AA[1];
                            check.Y += 25;
                            if (check.Y >= 475)
                                return;
                            for (int i = 0; i < 20; i++)
                            {
                                for (int j = 0; j < 10; j++)
                                {
                                    if (Dead1[i, j] == 1)
                                    {
                                        Rectangle boxbox = new Rectangle(new Point(j * 25 + 1, i * 25 + 1), new Size(25, 25));
                                        Rectangle Ex = (Rectangle)AA[1];
                                        Ex.Y += 25;
                                        if (Ex == boxbox)
                                            return;
                                        Ex = (Rectangle)AA[0];
                                        Ex.Y -= 25;
                                        if (Ex == boxbox)
                                            return;
                                    }
                                }
                            }

                            Rectangle M = (Rectangle)AA[0];
                            AA[1] = AA[0];
                            M.Y -= 25;
                            AA[0] = M;

                            M = (Rectangle)AA[1];
                            M.X -= 25;
                            AA[2] = M;

                            M.Y += 25;
                            AA[3] = M;

                            NowNum = 0;
                            break;
                        }
                    }
                    break;
                case Keys.Down:
                    while (true)
                    {
                        Rectangle box3 = (Rectangle)AA[0];                                          //블록의 가장 낮은 위치를 파악 하는 곳
                        int maxx = box3.Y;
                        for (int i = 0; i < AA.Count - 1; i++)
                        {
                            Rectangle next = (Rectangle)AA[i + 1];

                            if (next.Y > maxx)
                            {
                                maxx = next.Y;
                            }
                        }

                        maxx += 25;
                        if (maxx >= 475)
                        {
                            return;
                        }

                        for (int i = 0; i < 20; i++)
                        {
                            for (int j = 0; j < 10; j++)
                            {
                                if (Dead1[i, j] == 1)
                                {
                                    Rectangle boxbox = new Rectangle(new Point(j * 25 + 1, i * 25 + 1), new Size(25, 25));
                                    for (int a = 0; a < 4; a++)
                                    {
                                        Rectangle Ex = (Rectangle)AA[a];
                                        Ex.Y += 50;
                                        if (Ex == boxbox)
                                            return;
                                    }
                                }
                            }
                        }
                        for(int a=0;a<4;a++)
                        {
                            Rectangle Ex = (Rectangle)AA[a];
                            Ex.Y += 25;
                            AA[a] = Ex;
                        }
                        
                    } 

                case Keys.Left:
                    int min = 205;
                    for (int i = 0; i < AA.Count; i++)
                    {
                        Rectangle box = (Rectangle)AA[i];
                        if (box.X <= min)
                            min = box.X;
                    }
                    if (min < 10)
                        break;
                    for (int i = 0; i < 20; i++)      
                        for (int j = 0; j < 10; j++)
                        {
                            if (Dead1[i, j] == 1)
                            {
                                Rectangle boxbox = new Rectangle(new Point(j * 25 + 1, i * 25 + 1), new Size(25, 25));

                                for (int zz = 0; zz < AA.Count; zz++)
                                {
                                    Rectangle Ex = (Rectangle)AA[zz];
                                    Ex.X = Ex.X - 25;
                                    if (Ex == boxbox)
                                    {
                                        return;
                                    }
                                    Ex.Y = Ex.Y + 25;
                                    if (Ex == boxbox)
                                    {   
                                        return;
                                    }
                                }
                            }
                        }
                        for (int i = 0; i < AA.Count; i++)
                    {
                        Rectangle box = (Rectangle)AA[i];
                        box.X -= 25;
                        AA[i] = box;
                    }
                    break;
                case Keys.Right:
                    int max = 0;
                    for (int i = 0; i < AA.Count; i++)
                    {
                        Rectangle box = (Rectangle)AA[i];
                        if (box.X >= max)
                            max = box.X;
                    }
                    if (max > 205)
                        break;
                    for (int i = 0; i < 20; i++)       
                        for (int j = 0; j < 10; j++)
                        {
                            if (Dead1[i, j] == 1)
                            {
                                Rectangle boxbox = new Rectangle(new Point(j * 25 + 1, i * 25 + 1), new Size(25, 25));

                                for (int zz = 0; zz < AA.Count; zz++)
                                {
                                    Rectangle Ex = (Rectangle)AA[zz];
                                    Ex.X = Ex.X + 25;
                                    if (Ex == boxbox)
                                    {
                                        return;
                                    }
                                    Ex.Y = Ex.Y + 25;
                                    if (Ex == boxbox)
                                    {
                                        return;
                                    }
                                }
                            }
                        }

                    for (int i = 0; i < AA.Count; i++)
                    {
                        Rectangle box = (Rectangle)AA[i];
                        box.X += 25;
                        AA[i] = box;
                    }
                    break;

                default:                                                    
                    break;
            }
        }

        private void panel1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            Graphics a = e.Graphics;  // 패널의 그래픽스 객체
            SolidBrush brushBack = new SolidBrush(Color.White); // 사각형의 배경색
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
