using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private List<Point> _points;
        public List<double> B;
        public double[,] G;
        public double[] a;
        public bool button = false;
        public Form1()
        {
            InitializeComponent();
            _points = new List<Point>();
            B = new List<double>();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(new Pen(new SolidBrush(Color.Black), 5), Width/2, 0, Width/2, Height);
            e.Graphics.DrawLine(new Pen(new SolidBrush(Color.Black), 5), 0, Height/2, Width, Height/2);

            foreach (var point in _points)
            {
                e.Graphics.FillEllipse(new SolidBrush(Color.Red), point.X + Width / 2, - point.Y + Height / 2, 5, 5);
            }

            if (button)
            {
                for (int i = -Width/2; i < Width/2; i++)
                {
                    e.Graphics.DrawLine(new Pen(new SolidBrush(Color.Blue), 5), i + Width / 2, -(float)Function(i) + Height / 2, i + 1 + Width / 2, -(float)Function(i + 1) + Height / 2);
                }
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            _points.Add(new Point(e.X-Width/2, -(e.Y-Height/2)));
            Refresh();
        }

        public double Function(double x){
            double sum = 0.0;
            for (int i = 0; i<_points.Count; i++){
                sum += a[i]*Math.Pow(x,i);
            }
            return sum;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < _points.Count; i++){
                double temp = 0;
                for (int j = 0; j < _points.Count; j++){
                    temp += _points[j].Y * (double)Math.Pow(_points[j].X, i);  
    
                }
                B.Add(temp);

            }
            G = new double[_points.Count, _points.Count];
            a = new double[_points.Count];
            for (int i = 0; i < _points.Count; i++)
            {
                for (int j = 0; j < _points.Count; j++)
                {
                    for (int k = 0; k < _points.Count; k++)
                    {
                        G[i, j] += (double)Math.Pow(_points[k].X, i) * (double)Math.Pow(_points[k].X, j);
                    }
                }
            }
            for (int k = 0; k < _points.Count; k++)
            {
                for (int i = k+1; i < _points.Count; i++)
                {
                    double mult = G[i, k] / G[k, k];
                    B[i] -= B[k] * mult;
                    for (int j = k; j < _points.Count; j++)
                    {
                        G[i, j] = G[i, j] - (G[k, j] * mult);
                    }
                }
            }
            for (int i = _points.Count - 1; i >= 0; i--)
            {
                double sum = 0.0;
                for (int j=i; j<_points.Count;j++){
                 sum+=G[i,j]*a[j];   
                }
                a[i] = (B[i]-sum)/G[i,i];
            }
            string result = "";

            for (int i = 0; i < _points.Count; i++)
            {
                for (int j = 0; j < _points.Count; j++)
                {
                    result += G[i, j] + " ";
                }
                result += "\n";
            }

            result += "\nA = ";

            for (int i = 0; i < _points.Count; i++ )
            {
                result += a[i] + " ";
            }

            //MessageBox.Show(result);
            button = true;
            Refresh();
        }
    }
}
