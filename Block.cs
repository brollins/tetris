using System.Windows.Media;

namespace Tetris
{
    public class TetrisBlock
    {        
        private int column;
        private int row;

        public TetrisBlock() : this(0, 0) { }

        public TetrisBlock(int column, int row)
        {
            this.column = column;
            this.row = row;
        }
        
        public int Column
        {
            get
            {
                return column;
            }

            set
            {
                column = value;
            }
        }

        public int Row
        {
            get
            {
                return row;
            }

            set
            {
                row = value;
            }
        }

        public void MoveLeft()
        {
            column--;
        }

        public void MoveRight()
        {
            column++;
        }

        public void MoveDown()
        {
            row++;
        }

        public void Draw(TetrisBoard tetrisBoard, Color color)
        {
            tetrisBoard.Draw(column, row, color);
        }        
    }
}
