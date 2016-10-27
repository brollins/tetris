using System.Windows.Media;

namespace Tetris
{
    public class TetrisBlock
    {

   // convert x and y to Columnn and row
   // remove anything related to pixels on moves
   // remove the drop method
   // abstract the drawing to new canvas class, have draw simply draw with color and coordinates (new scaffolding/stub class "TetrisBoard" with Draw method to allow it to compile)
   // have draw pass in a canvas
   // remove fields that are not core to block's properties
   // simplify constructor

        private int column;
        private int row;

        public TetrisBlock () : this(0, 0)
        {

        }

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
