using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CrossroadsDemo
{
    public partial class CrossroadsForm : Form
    {
        /* This code fixed size panel is 600 pixel square
         * RedLightTime = GreenLightTime + YellowLightTime
         */
        private const int AreaSize = 600;
        private const int RedLightTime = 5;
        private const int GreenLightTime = 3;
        private int _count;
        //public int[] _trafficLights;
        public List<TranfficLight> TranfficLights;

        public CrossroadsForm()
        {
            InitializeComponent();

            Start = new Point(30, 30);

            InitializeTrafficLight();

            //Timer to change traffic light
            var trafficLightTimer = new Timer { Interval = 1000 }; //1s
            trafficLightTimer.Tick += TrafficLightTimerTick;
            _count = 0;
            trafficLightTimer.Start();
        }

        public Point Start { get; set; }

        private void InitializeTrafficLight()
        {
            TranfficLights = new List<TranfficLight>
            {
                new TranfficLight
                {
                    LightNumber = 1,
                    Start = new Point(Start.X + AreaSize/3 + AreaSize/48, Start.Y + AreaSize/3 - AreaSize/24),
                    LightSize = AreaSize/24,
                    IsHozontal = true
                },
                new TranfficLight
                {
                    LightNumber = 2,
                    Start = new Point(Start.X + AreaSize/3 - AreaSize/24, Start.Y + AreaSize/2 + AreaSize/48),
                    LightSize = AreaSize/24,
                    IsHozontal = false
                },
                new TranfficLight
                {
                    LightNumber = 3,
                    Start = new Point(Start.X + AreaSize/2 + AreaSize/48, Start.Y + 2*AreaSize/3),
                    LightSize = AreaSize/24,
                    IsHozontal = true
                },
                new TranfficLight
                {
                    LightNumber = 4,
                    Start = new Point(Start.X + 2*AreaSize/3, Start.Y + AreaSize/3 + AreaSize/48),
                    LightSize = AreaSize/24,
                    IsHozontal = false
                }
            };
            TranfficLights[0].TurnRedOn();
            TranfficLights[1].TurnGreenOn();
            TranfficLights[2].TurnRedOn();
            TranfficLights[3].TurnGreenOn();
        }

        private void TrafficLightTimerTick(object sender, EventArgs e)
        {
            _count++;
            if (_count == RedLightTime)
            {
                //Red to green, Green to red
                foreach (var light in TranfficLights)
                {
                    light.SwitchLight();
                }
                //Reset count
                _count = 0;

                //Re-draw form
                Invalidate();
            }
            else if (_count == GreenLightTime)
            {
                //Green to yellow
                foreach (var light in TranfficLights)
                {
                    light.TurnYellowOn();
                }

                Invalidate();
            }
        }

        private void CrossroadsForm_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            //Draw Area
            g.DrawRectangle(new Pen(Brushes.Black, 5), Start.X, Start.Y, AreaSize, AreaSize);

            //Draw field
            var fieldSize = AreaSize / 3;

            g.FillRectangle(Brushes.DarkGreen, Start.X, Start.Y, fieldSize, fieldSize);
            g.FillRectangle(Brushes.DarkGreen, 2 * fieldSize + Start.X, Start.Y, fieldSize, fieldSize);
            g.FillRectangle(Brushes.DarkGreen, Start.X, 2 * fieldSize + Start.Y, fieldSize, fieldSize);
            g.FillRectangle(Brushes.DarkGreen, 2 * fieldSize + Start.X, 2 * fieldSize + Start.Y, fieldSize, fieldSize);

            //Center lines
            g.DrawLine(new Pen(Brushes.Black, 3), Start.X, AreaSize / 2 + Start.Y, AreaSize + Start.X,
                AreaSize / 2 + Start.Y);
            g.DrawLine(new Pen(Brushes.Black, 3), AreaSize / 2 + Start.X, Start.Y, AreaSize / 2 + Start.X,
                AreaSize + Start.Y);

            //Draw trafffic light
            // * This is right side traffic
            foreach (var light in TranfficLights)
            {
                light.Draw(g);
            }
        }
    }
}