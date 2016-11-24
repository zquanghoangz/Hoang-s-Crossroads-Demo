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
        private const int RedLightTime = 10;
        private const int GreenLightTime = 8;

        public Point Start { get; set; }
        public int[] _tranficLights;
        private int _count;

        public CrossroadsForm()
        {
            InitializeComponent();

            Start = new Point(30, 30);
            _tranficLights = new int[4];
            _tranficLights[0] = _tranficLights[2] = 0; //Red
            _tranficLights[1] = _tranficLights[3] = 2; //Green

            //Timer to change tranfic light
            Timer tranficLightTimer = new Timer { Interval = 1000 }; //1s
            tranficLightTimer.Tick += TranficLightTimerTick;
            _count = 0;
            tranficLightTimer.Start();
        }

        private void TranficLightTimerTick(object sender, EventArgs e)
        {
            _count++;
            if (_count == RedLightTime)
            {
                //Red to green, Green to red
                _tranficLights[0] = _tranficLights[2] = _tranficLights[0] == 0 ? 2 : 0;
                _tranficLights[1] = _tranficLights[3] = _tranficLights[1] == 0 ? 2 : 0;
                //Reset count
                _count = 0;

                //Re-draw form
                Invalidate();
            }
            else if (_count == GreenLightTime)
            {
                //Green to yellow
                if (_tranficLights[0] == 2)
                {
                    _tranficLights[0] = _tranficLights[2] = 1;
                }
                if (_tranficLights[1] == 2)
                {
                    _tranficLights[1] = _tranficLights[3] = 1;
                }

                Invalidate();
            }
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
                onNumber: _tranficLights[0]);

            DrawTranficLight(
                g: g,
                startX: Start.X + AreaSize / 2 + AreaSize / 48,
                startY: Start.Y + 2 * AreaSize / 3,
                size: AreaSize / 8,
                isHozontal: true,
                onNumber: _tranficLights[2]);

            DrawTranficLight(
               g: g,
               startX: Start.X + AreaSize / 3 - AreaSize / 24,
               startY: Start.Y + AreaSize / 2 + AreaSize / 48,
               size: AreaSize / 8,
               isHozontal: false,
               onNumber: _tranficLights[1]);

            DrawTranficLight(
               g: g,
               startX: Start.X + 2*AreaSize / 3,
               startY: Start.Y + AreaSize / 3 + AreaSize / 48,
               size: AreaSize / 8,
               isHozontal: false,
               onNumber: _tranficLights[3]);
        }

        private void DrawTranficLight(Graphics g, int startX, int startY, int size, bool isHozontal, int onNumber)
        {
            int incWidth = 0, incHeight = size / 3, sizeOfLight = size / 3;
            if (isHozontal)
            {
                incWidth = size / 3;
                incHeight = 0;
            }

            g.FillEllipse(Brushes.Red, startX, startY, sizeOfLight, sizeOfLight);
            g.FillEllipse(Brushes.Yellow, startX + incWidth, startY + incHeight, sizeOfLight, sizeOfLight);
            g.FillEllipse(Brushes.Green, startX + 2 * incWidth, startY + 2 * incHeight, sizeOfLight, sizeOfLight);

            g.DrawEllipse(new Pen(Brushes.Orange, 3), startX + onNumber * incWidth, startY + onNumber * incHeight, sizeOfLight, sizeOfLight);
        }
    }
}
