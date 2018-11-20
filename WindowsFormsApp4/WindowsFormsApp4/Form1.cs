using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace WindowsFormsApp4
{

    public partial class Form1 : Form
    {
        int h1 = 440, IndexY = 0, KeoM1, KeoM2, KeoM3, KeoM4, KeoM5, Keo1 = 0, Ksost, Main;
        double S, H, V, K1, AO, BHO, OB, IC, Kvp, Vf, M, R, L, SP, ST, AP, Kkat, USH, LK, T, Kov = 0.5;
        string selection1;

        private void comboBox4_MouseHover(object sender, EventArgs e)
        {
            var box = sender as ComboBox;
            box.DroppedDown = true;
        }

        private void comboBox2_MouseHover(object sender, EventArgs e)
        {
            var box = sender as ComboBox;
            box.DroppedDown = true;
        }

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

        static int[] sost = new int[]          //   массив с условными значениями для состава отходов
        {1, 2, 3, 4, 5};

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

                case "пирамида":
                    return (S * H) / 3;

                case "параллелепипед":
                    return (S * H);

                default:
                   
                    { MessageBox.Show("Неверно указана форма", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk); }
                    return 0;
            }
        }

        static double GetSP(double L, double S, double R, double AP, double ST, double H, string selection1 = "")         //   находим площадь поверхности фигур
        {
            switch (selection1)         //   свич, выбирающий форму
            {
                case "конус":
                    return (S + (3.14 * R * L));

                case "пирамида":
                    return (S + 4 * (AP * (ST / 2)));

                case "параллелепипед":
                    return (2 * S + (4 * (Math.Sqrt(S) * H)));

                default:
                    return 0;
            }
        }

        static double GetRad(double S)          //   получение радиуса конуса из площади
        {
            return (Math.Sqrt(S / 3.14));
        }

        static double GetL(double R, double H)          //   получение образующей конуса
        {
            return (Math.Sqrt(H * H + R * R));
        }

        static double GetST(double S)          //   получение длины стороны пирамиды из площади
        {
            return (Math.Sqrt(S));
        }

        static double GetAP(double H, double ST)          //   получение длины апофемы пирамиды
        {
            return (Math.Sqrt(((ST / 2) * (ST / 2)) + (H * H)));
        }

        static int GetKeoM1(int Keo1, int Ksost, double S, double LK, double T, double Vf, double USH)         //   проверка значений по первому классу опасности
        {

            if (S >= 20)
            {
                Keo1++;
            }

            if (Ksost == 1)
            {
                Keo1++;
            }

            if (LK <= 50)
            {
                Keo1++;
            }

            if (T >= 2)
            {
                Keo1++;
            }

            if (Vf >= 15)
            {
                Keo1++;
            }

            if (USH >= 10)
            {
                Keo1++;
            }



            return Keo1;
        }

        static int GetKeoM2(int Keo1, int Ksost, double S, double LK, double T, double Vf, double USH)          //   проверка значений по второму классу опасности
        {

            if (S < 20 && S >= 15)
            {
                Keo1++;
            }

            if (Ksost == 2)
            {
                Keo1++;
            }

            if (LK <= 50 && LK < 100)
            {
                Keo1++;
            }

            if (T >= 1.5 && T < 2)
            {
                Keo1++;
            }

            if (Vf >= 10 && Vf < 15)
            {
                Keo1++;
            }

            if (USH >= 5 && USH < 10)
            {
                Keo1++;
            }



            return Keo1;
        }

        static int GetKeoM3(int Keo1, int Ksost, double S, double LK, double T, double Vf, double USH)          //   проверка значений по третьему классу опасности
        {

            if (S < 15 && S >= 10)
            {
                Keo1++;
            }

            if (Ksost == 3)
            {
                Keo1++;
            }

            if (LK <= 100 && LK < 200)
            {
                Keo1++;
            }

            if (T >= 1.5 && T < 2)
            {
                Keo1++;
            }

            if (Vf >= 10 && Vf < 15)
            {
                Keo1++;
            }

            if (USH >= 2 && USH < 5)
            {
                Keo1++;
            }



            return Keo1;
        }

        static int GetKeoM4(int Keo1, int Ksost, double S, double LK, double T, double Vf, double USH)          //   проверка значений по четвёртому классу опасности
        {

            if (S < 10 && S >= 5)
            {
                Keo1++;
            }

            if (Ksost == 4)
            {
                Keo1++;
            }

            if (LK <= 200 && LK < 300)
            {
                Keo1++;
            }

            if (T >= 1 && T < 1.5)
            {
                Keo1++;
            }

            if (Vf >= 5 && Vf < 10)
            {
                Keo1++;
            }

            if (USH >= 1 && USH < 2)
            {
                Keo1++;
            }



            return Keo1;
        }

        static int GetKeoM5(int Keo1, int Ksost, double S, double LK, double T, double Vf, double USH)          //   проверка значений по пятому классу опасности
        {

            if (S > 5)
            {
                Keo1++;
            }

            if (Ksost == 5)
            {
                Keo1++;
            }

            if (LK > 300)
            {
                Keo1++;
            }

            if (T < 1)
            {
                Keo1++;
            }

            if (Vf < 5)
            {
                Keo1++;
            }

            if (USH < 1)
            {
                Keo1++;
            }



            return Keo1;
        }

        static int GetMain(int KeoM1, int KeoM2, int KeoM3, int KeoM4, int KeoM5)
        {
            if (KeoM1 >= 3)
            { return 1; }
            else if ((KeoM2 >= 3) || (KeoM2 == 2 && (KeoM1 < 3 && KeoM1 >= 1)))
            { return 2; }
            else if ((KeoM3 >= 3) || (KeoM3 == 2 && (KeoM2 < 3 && KeoM2 >= 1)))
            { return 3; }
            else if ((KeoM4 >= 3) || (KeoM4 == 2 && (KeoM3 < 3 && KeoM3 >= 1)))
            { return 4; }
            else if ((KeoM5 >= 3) || (KeoM5 == 2 && (KeoM4 < 3 && KeoM4 >= 1)))
            { return 5; }
            else
                return 0;
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
            for (int i = 0; i < 22; i++)
            {
                if (osad[i][0] == h1)
                {
                    return i;
                }
            }

            return 0;
        }


        public Form1()          //  здраствуй, форма
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)          //   кнопка 
        {
            selection1 = comboBox1.Text.ToString();         //   так, похоже, можно с самого начала было делать

            if ((textBox1.Text == ("")) || (textBox1.Text == "0"))
            {
                MessageBox.Show("Заполните поле", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            else
            {
                textBox1.Text = textBox1.Text.Replace(".", ",");
                try
                {
                    S = Convert.ToDouble(textBox1.Text);          //   забираем значение площади свалки
                }
                catch
                {
                    MessageBox.Show("Неверный формат данных!");
                    return;
                }
            }

                if ((textBox2.Text == ("")) || (textBox2.Text == "0"))
                {
                    MessageBox.Show("Заполните поле", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }
                else
                {
                    textBox2.Text = textBox2.Text.Replace(".", ",");
                try
                {
                    H = Convert.ToDouble(textBox2.Text);         //   забираем значение высоты свалки
                }
                catch
                {
                    MessageBox.Show("Неверный формат данных!");
                    return;
                }
                
                };

            if ((textBox3.Text == ("")) || (textBox3.Text == "0"))
                {
                    MessageBox.Show("Заполните поле", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }
                else
                {
                    textBox3.Text = textBox3.Text.Replace(".", ",");

                try
                {
                    LK = Convert.ToDouble(textBox3.Text);           //   забираем значение удалённости свалки
                }
                catch
                {
                    MessageBox.Show("Неверный формат данных!");
                    return;
                }
               
                };           

                if ((textBox4.Text == ("")) || (textBox1.Text == "0"))
                {
                    MessageBox.Show("Заполните поле", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }
                else
                   {
                    textBox4.Text = textBox4.Text.Replace(".", ",");

                     try
                          {
                              T = Convert.ToDouble(textBox4.Text);           //   забираем значение удалённости свалки
                          }
                         catch
                            {
                                MessageBox.Show("Неверный формат данных!");
                                return;
                            }

                };           

                V = GetV(S, H, selection1);         //   получаем объём свалки
            if (V == 0)
            { return; }

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
                Ksost = sost[comboBox4.Items.IndexOf(comboBox4.Text)];          //   получаем условные значения для состава отходов
                IC = 0.01 * SP * 54 * 1.113 * Kvp;          //   испарение с поверхности полигона
                OB = Kov * (AO - IC);           //   отжимная влага
                Vf = Math.Abs((AO + OB) - (IC + BHO));            //   самая большая и самая страшная формула, объём фильтрата
                M = V * 0.45;           //   масса отходов


                USH = M * 5 * Kkat;          //   размер вреда
                KeoM1 = GetKeoM1(Keo1, Ksost, S, LK, T, Vf, USH);           //   получаем колличество верных значений
                KeoM2 = GetKeoM2(Keo1, Ksost, S, LK, T, Vf, USH);
                KeoM3 = GetKeoM3(Keo1, Ksost, S, LK, T, Vf, USH);
                KeoM4 = GetKeoM4(Keo1, Ksost, S, LK, T, Vf, USH);
                KeoM5 = GetKeoM5(Keo1, Ksost, S, LK, T, Vf, USH);
                Main = GetMain(KeoM1, KeoM2, KeoM3, KeoM4, KeoM5);          //   ВОТ ОНО!!!!!!
                label16.Text = Convert.ToString(SP);
                label17.Text = Convert.ToString(V);
                label18.Text = Convert.ToString(Vf);
                label19.Text = Convert.ToString(M);
                label20.Text = Convert.ToString(USH);
                label21.Text = Convert.ToString(Main);
                panel1.Visible = true;

            }
        }
    }

