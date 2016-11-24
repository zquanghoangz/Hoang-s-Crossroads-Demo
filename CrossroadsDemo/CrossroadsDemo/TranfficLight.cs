using System.Drawing;

namespace CrossroadsDemo
{
    public class TranfficLight
    {
        private int _lightOnNumber; //zero base: Red, Yellow, Green

        public TranfficLight()
        {
            _lightOnNumber = 0;
        }

        public int LightNumber { get; set; }
        public Point Start { get; set; }
        public int LightSize { get; set; }
        public bool IsHozontal { get; set; }

        public void Draw(Graphics g)
        {
            int incWidth = 0, incHeight = LightSize;
            if (IsHozontal)
            {
                incWidth = LightSize;
                incHeight = 0;
            }

            g.FillEllipse(Brushes.Red, Start.X, Start.Y, LightSize, LightSize);
            g.FillEllipse(Brushes.Yellow, Start.X + incWidth, Start.Y + incHeight, LightSize, LightSize);
            g.FillEllipse(Brushes.Green, Start.X + 2 * incWidth, Start.Y + 2 * incHeight, LightSize, LightSize);

            g.DrawEllipse(new Pen(Brushes.Orange, 3), Start.X + _lightOnNumber * incWidth,
                Start.Y + _lightOnNumber * incHeight, LightSize, LightSize);
        }

        public void TurnRedOn()
        {
            //Yellow turn red
            if (_lightOnNumber == 1)
            {
                _lightOnNumber = 0;
            }
        }

        public void TurnYellowOn()
        {
            //Green turn yellow
            if (_lightOnNumber == 2)
            {
                _lightOnNumber = 1;
            }
        }

        public void TurnGreenOn()
        {
            //Red turn green
            if (_lightOnNumber == 0)
            {
                _lightOnNumber = 2;
            }
        }

        public void SwitchLight()
        {
            if (_lightOnNumber == 0)
            {
                _lightOnNumber = 2;
            }
            else if (_lightOnNumber == 1)
            {
                _lightOnNumber = 0;
            }
            else if (_lightOnNumber == 2)
            {
                _lightOnNumber = 1;
            }
        }
    }
}