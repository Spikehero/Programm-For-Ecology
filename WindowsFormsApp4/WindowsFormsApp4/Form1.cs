using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4
{

    public partial class Form1 : Form
    {
        int h1 = 440,IndexY = 0 ;
        double S, H, V, K1, AO, BHO, OB, Kov = 0.5;
        string selection1;

        static double[][] osad = new double[][] {           //   Массив с коэффицентами для осадков
            new double[] { 440, 1.37 },
            new double[] { 450, 1.38 },
            new double[] { 460, 1.39 },
            new double[] { 470, 1.40 },
            new double[] { 480, 1.40 },
            new double[] { 490, 1.42 },
            new double[] { 500, 1.42 },
            new double[] { 510, 1.43 },
            new double[] { 520, 1.44 },
            new double[] { 530, 1.44 },
            new double[] { 540, 1.45 },
            new double[] { 550, 1.46 },
            new double[] { 560, 1.47 },
            new double[] { 570, 1.47 },
            new double[] { 580, 1.48 },
            new double[] { 590, 1.48 },
            new double[] { 600, 1.49 },
            new double[] { 610, 1.49 },
            new double[] { 620, 1.50 },
            new double[] { 630, 1.51 },
            new double[] { 640, 1.51 },
            new double[] { 650, 1.51 },
        };

        static double[] vid = new double[]          //   массив с коэффицентами для видов поверхностей
        {0.20, 0.30, 0.40, 0.50, 0.50, 0.56, 0.58, 0.60, 0.85, 0.90, 0.90};

        static double GetV(double S, double H = 0, string selection1 = "")          //   метод для нахождения объёма
        {
            switch (selection1)         //   свич, выбирающий форму
            {
                case "конус":
                    return (S * H) / 3;
                    //break;

                case "пирамида":
                    return (S * H) / 3;
                   // break;

                case "параллелепипед":
                    return (S * H);
                   // break;

                default:
                    return 0;
                  //  break;
            }
        }

        private void comboBox3_MouseHover(object sender, EventArgs e)           //   разворачивается список при наведении мыши
        {
            var box = sender as ComboBox;
            box.DroppedDown = true;
        }

    

        static int GetIndex(int h1)         //   получения индекса для коэффицента осадков
        {
            for (int i=0; i<22; i++)
            {
                if (osad [i][0] == h1)
                    {
                    return i;
                    }
               // IndexY = GetIndex(h1);
            }

            return 0;    
        }

        private void textBox1_TextChanged(object sender, EventArgs e)           //   получения переменной площади
        {
            S = double.Parse(textBox1.Text);
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)          //   получение данных о форме при клавиатурном вводе
        {
            selection1 = comboBox1.Text.ToString();
        }
            
        private void textBox2_TextChanged(object sender, EventArgs e)           //   получение переменной высоты
        {
            H = double.Parse(textBox2.Text);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)         //  получение данных о форме при выборе из списка
        {
            selection1 = comboBox1.SelectedItem.ToString();
           // MessageBox.Show(selection1);
        }
              
                public Form1()          //  здраствуй, форма
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)          //   кнопка 
        {

            V = GetV(S, H, selection1);         //   получаем объём свалки
            BHO = (0.15 * V);
            
            
            h1 = Convert.ToInt32(numericUpDown1.Text);          //   получаем значение из нумерика
            IndexY = GetIndex(h1);          //   получаем индекс коэффицента осадков
            K1 = osad[IndexY][1];           //   находим коэффицент
            // h1 = numericUpDown1.Value;
            AO = 0.001 * S * h1 * K1;
           // OB = Kov * (AO - IC);
            label10.Text = Convert.ToString(BHO);
            label10.Visible = true;
            button2.Visible = true;
           // label9.Text = Convert.ToString(vid [0]);
            label9.Text = Convert.ToString(comboBox3.Items.IndexOf(comboBox3.Text));
        }
    }
}
