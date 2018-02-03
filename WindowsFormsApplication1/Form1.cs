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
        public List<int> B;
        public Form1()
        {
            InitializeComponent();
            _points = new List<Point>();
            B = new List<int>();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(new Pen(new SolidBrush(Color.Black), 5), Width/2, 0, Width/2, Height);
            e.Graphics.DrawLine(new Pen(new SolidBrush(Color.Black), 5), 0, Height/2, Width, Height/2);

            foreach (var point in _points)
            {
                e.Graphics.FillEllipse(new SolidBrush(Color.Red), point.X, point.Y, 5, 5);
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            _points.Add(new Point(e.X, e.Y));
            Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < _points.Count; i++){
                int temp = 0;
                for (int j = 0; j < _points.Count; j++){
                    temp += _points[j].Y * (int)Math.Pow(_points[j].X, i);  
    
                }
                B.Add(temp);
            }

        }
    }
}
