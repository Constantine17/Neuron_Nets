using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NeuronNets
{
    public partial class Form1 : Form
    {
        //объявление общих элементов
        TextBox[,] tb = new TextBox[30,30]; //объявление текстбоксов
        Bitmap b;
        Graphics graf;
        bool prov = false;
        Color fist;
        int[,] vX=new int[30,30];
        double[,] W = new double[30, 30];
        int Y = 0;
        
        double T = 0; // задаем Т
        double alpha = 0.5; //задаем скорость обучения

        double S = 0;

        

        public Form1()
        {
            InitializeComponent();

            for (int j = 0; j < 30; j++)
            {
                for (int i = 0; i < 30; i++)
                {
                    W[i,j] = 0.4; // первое значение весовой функции
                    
                }
            }


            int x = 7, y =20;
            //TextBox[] tb = new TextBox[20];
            for (int j=0; j<30;j++ )
            {
                for (int i = 0; i < 30; i++)
                {
                    tb[i, j] = new TextBox() //свойства текстбоксов
                    {
                        Location = new Point(x, y),
                        Text = "0.5",
                        Size = new Size(24, 20)
                    };
                    //бла-бла-бла
                    this.groupBox1.Controls.Add(tb[i, j]);
                    x += 25;
                }
                x = 7; y += 20;
            }
            Bitmap savedBit = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.DrawToBitmap(savedBit, pictureBox1.ClientRectangle);
            fist = savedBit.GetPixel(1,1);//цвет фона изображения
            
        }
        

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //int u=1;
            for (int i = 0; i < 4; i++)
            {

                //Graphics g = Graphics.FromHwnd(panel1.Handle);
            }
           // Graphics graf = pictureBox1.CreateGraphics();

            //pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
           // Image bmp = pictureBox1.Image;
            //graf = Graphics.FromImage(bmp);
           // g.DrawLine(new Pen(Brushes.Black, 2f), 0, 20, 100, 20);
           // pictureBox1.Image.Save("image.png", System.Drawing.Imaging.ImageFormat.Png);


            Bitmap savedBit = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.DrawToBitmap(savedBit, pictureBox1.ClientRectangle);

           
            //savedBit = (Bitmap)pictureBox1.Image;

          //  Graphics graf = Graphics.FromImage(savedBit);
            
            SaveFileDialog k = new SaveFileDialog();
            if (k.ShowDialog() == DialogResult.OK)

                savedBit.Save(k.FileName);

        }
        
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }


        bool DoDraw = false;

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            DoDraw = true;
        }





        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            DoDraw = true; 
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            DoDraw = false;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            //if (DoDraw)//рисование
            {

                if (DoDraw)
                {
                    prov = false; b = new Bitmap(pictureBox1.Width, pictureBox1.Height); //преобразования рисунка в изображение
                    graf = Graphics.FromImage(b);
                    Graphics grafpro = Graphics.FromImage(b);
                    SolidBrush redBrush = new SolidBrush(Color.Black);//кисть рисования
                    graf.FillEllipse(redBrush, e.X, e.Y, 10, 10);

                    pictureBox1.Image = b;
                }

            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Clear_Click(object sender, EventArgs e)
        {
            
            pictureBox1.Invalidate();
            if(!prov) graf.Clear(Color.White); //очистка изображения
            prov = true;
            yotv.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap savedBit = new Bitmap(pictureBox1.Width, pictureBox1.Height);//преобразования рисунка в изображение
            pictureBox1.DrawToBitmap(savedBit, pictureBox1.ClientRectangle);
            
           // Color pixelColor = savedBit.GetPixel(50, 50);
           // pictureBox1.Invalidate();
            //graf.Clear(pixelColor);

            for (int y=0, j=2; y < 30; y++)
            {
                for (int x =0, i=2; x < 30; x++)
                {

                    if (savedBit.GetPixel(i, j) == fist) vX[x, y] = 0;else vX[x, y] = 1;//ввод данных
                    if (checkBox1.Checked == true)//проверка чекбокса переобразование 
                    { tb[x, y].Text = Convert.ToString(vX[x, y]); tb[x, y].BackColor = Color.White; }//обнуление цвета
                    i = i + 5;
                }
                j = j + 5;
            }

            for (int j = 0; j < 30; j++)
            {
                for (int i = 0; i < 30; i++)
                {
                    S = S + (vX[i, j] * W[i, j] - T);
                }
            }
            sum.Text = Convert.ToString(S);
            S = 0;
            yotv.Text = "";//обнуление вывода
             
        }


        private void plus_Click(object sender, EventArgs e)
        {
            S = 0;

            for (int j = 0; j < 30; j++)//суммирование
            {
                for (int i = 0; i < 30; i++)
                {
                    S = S + (vX[i, j] * W[i, j] - T);
                }
            }

            if (S >= 0) Y = 1; else Y = -1;//вывод сети

            for (int j = 0; j < 30; j++)
            {
                for (int i = 0; i < 30; i++)
                {
                    W[i, j] = W[i, j] - alpha * vX[i, j] * (Y - 1);//обучение сети
                }
            }
            T = T + alpha * (Y - 1);

            for (int j = 0; j < 30; j++)// вывод значения персептронов
            {
                for (int i = 0; i < 30; i++)
                {
                    tb[i, j].Text = Convert.ToString(W[i, j]);
                    if (checkBox2.Checked == true) //проверка чек бокса на цвет
                        if (W[i, j] > 0.5)
                            tb[i, j].BackColor = Color.Green;
                        else if (W[i, j] < 0.3) tb[i, j].BackColor = Color.Red; else tb[i, j].BackColor = Color.White; ;
                }
            }

            if (Y == 1) yotv.Text = "A"; else yotv.Text = "Б";//вывод значение сети в текстбокс
            S = 0;
        }

        private void minus_Click(object sender, EventArgs e)
        {
            S = 0;
            for (int j = 0; j < 30; j++)//суммирование
            {
                for (int i = 0; i < 30; i++)
                {
                    S = S + (vX[i, j] * W[i, j] - T);
                }
            }

            if (S >= 0) Y = 1; else Y = -1;//вывод сети

            for (int j = 0; j < 30; j++)
            {
                for (int i = 0; i < 30; i++)
                {
                    W[i, j] = W[i, j] - alpha * vX[i, j] * (Y + 1);//обучение сети
                }
            }
            T = T + alpha * (Y + 1);

            for (int j = 0; j < 30; j++)// вывод значения персептронов
            {
                for (int i = 0; i < 30; i++)
                { 
                    tb[i, j].Text = Convert.ToString(W[i, j]);
                    if (checkBox2.Checked == true)//проверка чек бокса на цвет
                        if (W[i, j] > 0.5)
                            tb[i, j].BackColor = Color.Green;
                        else if (W[i, j] < 0.3) tb[i, j].BackColor = Color.Red; else tb[i, j].BackColor = Color.White; ; 
                }
            }
            if (Y == 1) yotv.Text = "A"; else yotv.Text = "Б";//вывод значение сети в текстбокс
            S = 0;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            for (int j = 0; j < 30; j++)//суммирование
            {
                for (int i = 0; i < 30; i++)
                {
                    S = S + (vX[i, j] * W[i, j] - T);
                }
            }

            if (S >= 0) Y = 1; else Y = -1;//вывод сети
            if (Y == 1) yotv.Text = "A"; else yotv.Text = "Б";//вывод значение сети в текстбокс
            S = 0;
        }

        private void Clear_KeyDown(object sender, KeyEventArgs e)
        {

        }
        

    }
}
