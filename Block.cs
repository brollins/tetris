using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfApplication2
{
    class Block
    {
        private double x;
        private double y;
        private Color color;
        private Rectangle rect;

        public Block(): this(0,0, Colors.PowderBlue)
        {

        }

        public Block(double x, double y, Color color)
        {
            this.x = x;
            this.y = y;
            this.color = color;
                       
        }

        public double X
        {
            get
            {
                return x;
            }

            set
            {
                if(value>0)
                {
                    x = value;
                }
            }
        }

        public double Y
        {
            get
            {
                return y;
            }

            set
            {
                y = value;
            }
        }

        public void Draw(Canvas canvas)
        {
            Rectangle rect = new Rectangle();
            Canvas.SetTop(rect, y);
            Canvas.SetLeft(rect, x);
            rect.Height = 50;
            rect.Width = 50;
            rect.Stroke = Brushes.PowderBlue;
            rect.StrokeThickness = 2;
            rect.Fill = new SolidColorBrush(color);
            canvas.Children.Add(rect);
            
        }
    }
    }
