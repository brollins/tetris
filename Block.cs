using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Tetris
{
    public class TetrisBlock
    {
        private double x;
        private double y;
        private Color color;
        private Canvas canvas;
        private Rectangle blockGraphic;
        
        public TetrisBlock(double x, double y, Color color, Canvas canvas)
        {
            this.x = x;
            this.y = y;
            this.Color = color;
            this.canvas = canvas;

        }

        public double X
        {
            get
            {
                return x;
            }

            set
            {
                if (value > 0 && value < 410)
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
                {
                    y = value;
                }
            }
        }

        public void Drop()
        {
            y = y + 40;
            Draw();
        }

        public void MoveLeft()
        {
            x = x - 40;
            Draw();

        }

        public void MoveRight()
        {
            x = x + 40;
            Draw();

        }

        public void MoveDown()
        {
            Drop();
            Draw();

        }

        public Rectangle BlockGraphic
        {
            get
            {
                return blockGraphic;
            }
            set
            {
                blockGraphic = value;
            }

        }

        public Color Color
        {
            get
            {
                return color;
            }

            set
            {
                color = value;
            }
        }

        public void Draw()
        {
            if (BlockGraphic == null)
            {
                Rectangle rect = new Rectangle();
                Canvas.SetTop(rect, y);
                Canvas.SetLeft(rect, x);
                rect.Height = 40;
                rect.Width = 40;
                rect.Stroke = Brushes.PowderBlue;
                rect.StrokeThickness = 1;
                rect.Fill = new SolidColorBrush(Color);
                canvas.Children.Add(rect);
                BlockGraphic = rect;
            }
            else
            {
                Canvas.SetTop(BlockGraphic, y);
                Canvas.SetLeft(BlockGraphic, X);
            }
        }
    }
}
