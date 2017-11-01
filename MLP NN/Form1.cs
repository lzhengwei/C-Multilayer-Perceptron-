using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MLP_NN
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Learn();
        }
        TextBox[][] TextBox_num = new TextBox[9][];
        private void LoadTextBox()
        {
            for (int i = 0; i < 9; i++)
            {
                TextBox_num[i] = new TextBox[4];
            }

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    TextBox_num[i][j] = new TextBox();
                    TextBox_num[i][j].Name = "TextBox" + i;
                    TextBox_num[i][j].Text ="0";
                    TextBox_num[i][j].Height = 50;
                    TextBox_num[i][j].Width = 50;
                    TextBox_num[i][j].Location = new Point(60 * j, i*40);
                    Panel_textbox.Controls.Add(TextBox_num[i][j]);
                }                    
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            double[][] testdata = { new double[36] };
            int index = 0;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    testdata[0][index] = double.Parse(TextBox_num[i][j].Text);
                    index++;
                }
            }
           // testdata[0][36] = double.Parse(textBox1.Text);
            double x = mm.Test(testdata);
            MessageBox.Show( x+ "");
            //mm.dot(a, b);
            
        }
        MLP mm;
        private void Learn()
        {
            int[] layer = new int[] { 36,10,3};
            mm = new MLP(layer);
        /*    double[][] samples ={new double[10]{1,1,1,
                                                1,0,1,
                                                1,1,1,0.0},
                                 new double[10]{0,0,1,
                                                0,0,1,
                                                0,0,1,0.5},
                                 new double[10]{1,1,1,
                                                0,0,1,
                                                0,0,1,0.8},
                                                };*/
            double[][] samples ={new double[37]{1,1,1,1,
                                                1,0,0,1,
                                                1,0,0,1,
                                                1,0,0,1,
                                                1,0,0,1,
                                                1,0,0,1,
                                                1,0,0,1,
                                                1,0,0,1,
                                                1,1,1,1,0.0},
                                 new double[37]{0,0,0,1,
                                                0,0,0,1,
                                                0,0,0,1,
                                                0,0,0,1,
                                                0,0,0,1,
                                                0,0,0,1,
                                                0,0,0,1,
                                                0,0,0,1,
                                                0,0,0,1,0.1},
                                 new double[37]{1,1,1,1,
                                                0,0,0,1,
                                                0,0,0,1,
                                                0,0,0,1,
                                                1,1,1,1,
                                                1,0,0,0,
                                                1,0,0,0,
                                                1,0,0,0,
                                                1,1,1,1,0.2},
                                new double[37]{ 1,1,1,1,
                                                0,0,0,1,
                                                0,0,0,1,
                                                0,0,0,1,
                                                1,1,1,1,
                                                0,0,0,1,
                                                0,0,0,1,
                                                0,0,0,1,
                                                1,1,1,1,0.3},
                                new double[37]{ 1,0,0,1,
                                                1,0,0,1,
                                                1,0,0,1,
                                                1,0,0,1,
                                                1,1,1,1,
                                                0,0,0,1,
                                                0,0,0,1,
                                                0,0,0,1,
                                                0,0,0,1,0.4},
                                new double[37]{ 1,1,1,1,
                                                1,0,0,0,
                                                1,0,0,0,
                                                1,0,0,0,
                                                1,1,1,1,
                                                0,0,0,1,
                                                0,0,0,1,
                                                0,0,0,1,
                                                1,1,1,1,0.5},
                                new double[37]{ 1,0,0,0,
                                                1,0,0,0,
                                                1,0,0,0,
                                                1,0,0,0,
                                                1,1,1,1,
                                                1,0,0,1,
                                                1,0,0,1,
                                                1,0,0,1,
                                                1,1,1,1,0.6},
                                 new double[37]{1,1,1,1,
                                                0,0,0,1,
                                                0,0,0,1,
                                                0,0,0,1,
                                                0,0,0,1,
                                                0,0,0,1,
                                                0,0,0,1,
                                                0,0,0,1,
                                                0,0,0,1,0.7},
                                new double[37]{ 1,1,1,1,
                                                1,0,0,1,
                                                1,0,0,1,
                                                1,0,0,1,
                                                1,1,1,1,
                                                1,0,0,1,
                                                1,0,0,1,
                                                1,0,0,1,
                                                1,1,1,1,0.8},
                                new double[37]{ 1,1,1,1,
                                                1,0,0,1,
                                                1,0,0,1,
                                                1,0,0,1,
                                                1,1,1,1,
                                                0,0,0,1,
                                                0,0,0,1,
                                                0,0,0,1,
                                                0,0,0,1,0.9},
                                };

            mm.learn(samples);

            double[] a = new double[] { 1, 2, 3 };
            double[,] b=new double[,]{{4,5,6},{5,6,7},{6,7,8}};

            double[] ans = mm.dot(a, b);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadTextBox();

        }
        
    }
}
