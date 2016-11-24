using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrossroadsDemo
{
    public partial class CrossroadsForm : Form
    {
        /* This code fixed size panel is 600 pixel square
         */
        private const int AreaSize = 600;
        public Point Start { get; set; }

        public CrossroadsForm()
        {
            InitializeComponent();

            Start = new Point(30, 30);
        }

        private void CrossroadsForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            //Draw Area
            g.DrawRectangle(new Pen(Brushes.Black, 5), Start.X, Start.Y, AreaSize, AreaSize);

            //Draw field
            var fieldSize = AreaSize / 3;

            g.FillRectangle(Brushes.DarkGreen, Start.X, Start.Y, fieldSize, fieldSize);
            g.FillRectangle(Brushes.DarkGreen, 2 * fieldSize + Start.X, Start.Y, fieldSize, fieldSize);
            g.FillRectangle(Brushes.DarkGreen, Start.X, 2 * fieldSize + Start.Y, fieldSize, fieldSize);
            g.FillRectangle(Brushes.DarkGreen, 2 * fieldSize + Start.X, 2 * fieldSize + Start.Y, fieldSize, fieldSize);

            //Center lines
            g.DrawLine(new Pen(Brushes.Black, 3), Start.X, AreaSize / 2 + Start.Y, AreaSize + Start.X, AreaSize / 2 + Start.Y);
            g.DrawLine(new Pen(Brushes.Black, 3), AreaSize / 2 + Start.X, Start.Y, AreaSize / 2 + Start.X, AreaSize + Start.Y);

            //Draw tranffic light
            // * This is right side tranfic
            DrawTranficLight(
                g: g,
                startX: Start.X + AreaSize / 3 + AreaSize / 48,
                startY: Start.Y + AreaSize / 3 - AreaSize / 24,
                size: AreaSize / 8,
                isHozontal: true,
                onNumber: 0);

            //TODO: draw 3 other tranfic lights

        }

        private void DrawTranficLight(Graphics g, int startX, int startY, int size, bool isHozontal, int onNumber)
        {

            if (isHozontal)
            {
                int width = size, heigh = size / 3;

                g.DrawRectangle(new Pen(Brushes.Black), startX, startY, width, heigh);

                g.FillRectangle(Brushes.Red, startX, startY, heigh, heigh);
                g.FillRectangle(Brushes.Yellow, startX + heigh, startY, heigh, heigh);
                g.FillRectangle(Brushes.Green, startX + 2 * heigh, startY, heigh, heigh);

                g.DrawRectangle(new Pen(Brushes.Orange, 3), startX + onNumber * Height, startY, heigh, heigh);
            }
            else
            {
                int width = size / 3, heigh = size;
                //TODO: vertical tranfix light
            }

        }
    }
}
