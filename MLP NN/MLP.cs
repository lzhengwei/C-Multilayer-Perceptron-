using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MLP_NN
{
    class MLP
    {
        int[] shape;
        List<double[,]> weights = new List<double[,]>(100), dw = new List<double[,]>(100);
        double[][] layers;
        double[] Target;
        public MLP(int[] Layer)
        {
            shape = Layer;
            layers = new double[Layer.Length][];
           
            
            for (int i = 0; i < Layer.Length; i++)
            {
                int len;
                if(i==0)
                    len=Layer[i]+1;
                else
                    len=Layer[i];
                layers[i] = new double[len];
                
                for (int j = 0 ; j < len ; j++)
                {
                    layers[i][j] = 1;
                    
                }
            }
            
            for (int i = 0; i < Layer.Length-1; i++)
            {
                double[,] np = new double[layers[i].Length, layers[i + 1].Length];
                weights.Add(np);
                double[,] nnp = new double[layers[i].Length, layers[i + 1].Length];
                dw.Add(nnp);
            }
            
            //MessageBox.Show(weights[0].GetLength(0) + "");
            reset();
        }
        public void reset()
        {
            Random rand=new Random();
            for (int i = 0; i < weights.Count; i++)
            {
                for (int j = 0; j < weights[i].GetLength(0); j++)
                {
                    for (int t = 0; t < weights[i].GetLength(1); t++)
                    {
                        weights[i][j, t] = (2.0*rand.NextDouble()-1)*2*0.25 ;
                    }
                }
            }
        }
        private double propagate_forward(double[] data)
        {
            for (int i = 0; i < data.Length; i++)          
                layers[0][i] = data[i];
            
            for (int i = 1; i < shape.Length; i++)
            {
                double[] dotmatrix = dot(layers[i - 1], weights[i - 1]);
                layers[i] = sigmod(dotmatrix);
            }
            return layers[shape.Length - 1][0];
            
        }
        private double propagate_backward(double target,double lrate=0.1, double momentum=0.1)
        {
            double[] error = new double[shape[shape.Length - 1]];
            for (int i = 0; i < shape[shape.Length - 1]; i++)
                error[i] = target - layers[layers.GetLength(0) - 1][i];
            double[] dsigmod_layers = dsigmod(layers[layers.GetLength(0) - 1]);
            double[][] deltas = new double[shape.Length - 1][];

            deltas[1] = new double[shape[2]];
            deltas[0] = new double[shape[1]];
            for (int i = 0; i < shape[2]; i++)
            {
               deltas[1][i] = error[i] * dsigmod_layers[i];
            }

            double[,] layer_dot_weight = new double[weights[1].GetLength(1), weights[1].GetLength(0)];
            dsigmod_layers = dsigmod(layers[1]);
            for (int di = 0; di < weights[1].GetLength(1); di++)
            {
                for (int i = 0; i < weights[1].GetLength(0); i++)
                {
                    layer_dot_weight[di, i] = dsigmod_layers[i] * weights[1][i, di];
                }
            }
            deltas[0] = dot(deltas[1],layer_dot_weight);
            double[][] dw_compute ;
            if (tryindex % 10 == 0)
            {
                int x = 20;
            }
            for (int i = 0; i < weights.Count; i++)
            {
                dw_compute = new double[layers[i].Length][];
                for (int d = 0; d < layers[i].Length; d++)
                {
                    dw_compute[d] = new double[layers[i+1].Length];
                }

                for (int di = 0; di < layers[i+1].Length; di++)
                {
                    for (int d = 0; d < layers[i].Length; d++)
                    {
                        dw_compute[d][di] = layers[i][d] * deltas[i][di];
                    }
                }
                double dii=0;
                for (int di = 0; di < layers[i+1].Length; di++)
                {
                    for (int d = 0; d < layers[i].Length; d++)
                    {
                        weights[i][d, di] += dw_compute[d][di] * lrate + momentum*dw[i][d,di];
                        dw[i][d, di] = dw_compute[d][di];
                    }
                }
            }
            double re_ans=0;
            for (int i = 0; i < layers[layers.GetLength(0) - 1].Length; i++)
            {
                re_ans+=error[i]*error[i];
            }
            return re_ans;
        }
        private double[] sigmod(double[] num)
        {
            double[] ans = new double[num.Length];
            for(int i=0;i<num.Length;i++)
            {
                ans[i]=Math.Tanh(num[i]);
            }
            return ans;
        }
        private double[] dsigmod(double[] num)
        {
            double[] ans = new double[num.Length];
            for (int i = 0; i < num.Length; i++)
            {
                ans[i] = 1.0 - Math.Pow(num[i], 2.0);
            }
            return ans;
        }
        public double[] dot(double[] matrixA,double[,] matrixB)
        {
            
            int a = 1; //矩阵的行数       
            int b = 1;//矩阵的列数
            int c = matrixB.GetLength(0);//另一矩阵的行数
            int d = matrixB.GetLength(1);//另一矩阵的列数
            double[] arr=new double[d];
            

                for (int j = 0; j < d; j++)
                {
                   arr[j] = 0;
                }
            

            for (int j = 0; j < d; j++)
            {
                double temp= 0;
                for (int k = 0; k < c; k++)
                {
                    temp += matrixA[k] * matrixB[k,j];
                }
                arr[j] = temp;
            }
            
            return arr;
        }
        int tryindex = 0;
        public void learn(double[][] samples,int epochs=2500,double lrate=0.1,double momentum=0.1)
        {
            Random rand=new Random();
            Target = new double[samples.GetLength(0)];
            double[][] train_samples = new double[samples.GetLength(0)][];
            for (int i = 0; i < samples.GetLength(0); i++)
            {
                Target[i] = samples[i][samples[0].Length - 1];
                train_samples[i] = new double[36];
                for (int ii = 0; ii < 36; ii++)
                {
                    train_samples[i][ii] = samples[i][ii];
                }
            }

                //Train
                for (int i = 0; i < epochs; i++)
                {
                    tryindex++;
                    int n = rand.Next(samples.GetLength(0));
                    propagate_forward(train_samples[n]);
                    propagate_backward(Target[n], lrate, momentum);
                }
            //Test
            for (int i = 0; i < samples.GetLength(0); i++)
            {
                double d = propagate_forward(train_samples[i]);
                MessageBox.Show(d + "");
            }
        }
        public int Test(double[][] samples)
        {
            int ans = 0;
            double d = propagate_forward(samples[0]);
            double x=Math.Round(d, 1, MidpointRounding.AwayFromZero);
            
                if(x== 0.0)
                    return 0;
                    
                if(x== 0.1)
                    return 1;
                if(x== 0.2)
                    return 2;
                if(x== 0.3)
                    return 3;
                if(x== 0.4)
                    return 4;
                if(x== 0.5)
                    return 5;
                if(x== 0.6)
                    return 6;
                if(x== 0.7)
                    return 7;
                if(x== 0.8)
                    return 8;
                if (x == 0.9)
                    return 9;

            
            return 0;
        }
    }
}
