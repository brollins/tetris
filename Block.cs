using System.Windows;
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
        private Canvas board;
        private Rectangle blockGraphic;

        public TetrisBlock(double x, double y, Color color, Canvas board)
        {
            this.x = x;
            this.y = y;
            this.Color = color;
            this.board = board;
        }

        public double X
        {
            get
            {
                return x;
            }

            set
            {
                x = value;
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

        public void Drop()
        {
            y = y + 50;
            Draw();
        }

        public void MoveLeft()
        {
            x = x - 50;
            Draw();
        }

        public void MoveRight()
        {
            x = x + 50;
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
                rect.Height = 48;
                rect.Width = 48;
                rect.Stroke = Brushes.White;
                rect.StrokeThickness = 1;
                rect.Fill = new SolidColorBrush(Color);
                board.Children.Add(rect);
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
