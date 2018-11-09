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
        double S, H, V, K1, AO, BHO, OB, IC, Kvp, Vf, M, R, L, SP, ST, AP, Kkat, USH, Kov = 0.5;
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

        static double[] kat = new double[]          //   массив с коэффицентами для категорий земель
        {2, 1.9, 1.9, 1.8, 1.6, 1.5, 1.3, 1};

        private void button2_Click_1(object sender, EventArgs e)            //   кнопка "Назад" на панели
        {
            panel1.Visible = false;
        }

        static double GetV(double S, double H, string selection1 = "")          //   метод для нахождения объёма
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

        static double GetSP(double L, double S, double R, double AP, double ST, double H, string selection1 = "")         //   находим площадь поверхности фигур
        {
            switch (selection1)         //   свич, выбирающий форму
            {
                case "конус":
                   return (S +(3.14 * R * L));         
                  
                    
                //break;

                case "пирамида":
                    return (S + 4 * (AP * (ST/2)));
                // break;

                case "параллелепипед":
                    return (2 * S +(4 * (Math.Sqrt(S)* H)));
                // break;

                default:
                    return 0;
                    //  break;
            }
        } 

        static double GetRad(double S)          //   получение радиуса конуса из площади
        {
            return (Math.Sqrt(S / 3.14));
        }

        static double GetL(double R, double H)          //   получение образующей конуса
        {
            return (Math.Sqrt(H*H + R*R));
        }

        static double GetST(double S)          //   получение длины стороны пирамиды из площади
        {
            return (Math.Sqrt(S));
        }

        static double GetAP(double H, double ST)          //   получение длины апофемы пирамиды
        {
            return (Math.Sqrt(((ST / 2) * (ST/2)) + (H*H)));
        }

        private void comboBox1_MouseHover(object sender, EventArgs e)            //   разворачивается список при наведении
        {
            var box = sender as ComboBox;
            box.DroppedDown = true;
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
                           
        private void textBox2_TextChanged(object sender, EventArgs e)           //   получение переменной высоты
        {
            H = double.Parse(textBox2.Text);
        }
           
                public Form1()          //  здраствуй, форма
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)          //   кнопка 
        {
            selection1 = comboBox1.Text.ToString();         //   так, похоже, можно с самого начала было делать
            V = GetV(S, H, selection1);         //   получаем объём свалки
            R = GetRad(S);          //   радиус основания конуса
            L = GetL(R, H);         //   образующая конуса
            ST = GetST(S);          //   сторона основания пирамиды или параллелепипеда
            AP = GetAP(H, ST);          //   апофема пирамиды
            SP = GetSP(L, S, R, AP, ST, H, selection1);         //   площадь поверхности фигуры

            BHO = (0.15 * V);           //   влага, расходуемая на насыщение отходов
            
            h1 = Convert.ToInt32(numericUpDown1.Text);          //   получаем значение из нумерика
            IndexY = GetIndex(h1);          //   получаем индекс коэффицента осадков
            K1 = osad[IndexY][1];           //   находим коэффицент
            AO = 0.001 * S * h1 * K1;           //   атмосферные осадки
            Kvp = vid[comboBox3.Items.IndexOf(comboBox3.Text)];
            Kkat = kat[comboBox2.Items.IndexOf(comboBox2.Text)];            //   получаем значение коэффицента чего-то, в зависимости от категории земель
            IC = 0.01 * SP * 54 * 1.113 * Kvp;          //   испарение с поверхности полигона
            OB = Kov * (AO - IC);           //   отжимная влага
            Vf = Math.Abs ((AO + OB) - (IC + BHO));            //   самая большая и самая страшная формула, объём фильтрата
            M = V * 0.45;           //   масса отходов


            USH = M * 5 * Kkat;          //   размер вреда

            label10.Visible = true;
            label10.Text = Convert.ToString(SP);
            label12.Text = Convert.ToString(S);         
            label14.Text = Convert.ToString(V);
            label16.Text = Convert.ToString(R);
            panel1.Visible = true;

        }
    }
}
