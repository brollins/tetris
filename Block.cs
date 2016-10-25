using System.Windows.Media;
using System.Windows.Shapes;

namespace Tetris
{
    public class TetrisBlock
    {

   // convert x and y to columnn and row
   // remove anything related to pixels on moves
   // remove the drop method
   // abstract the drawing to new canvas class, have draw simply draw with color and coordinates (new scaffolding/stub class "TetrisBoard" with Draw method to allow it to compile)
   // have draw pass in a canvas
   // remove fields that are not core to block's properties
   // simplify constructor

        private int column;
        private int row;

        public TetrisBlock(int column, int row)
        {
            this.column = column;
            this.row = row;
        }

        public int column
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

        public int row
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

        public void Draw(TetrisBoard tetrisboard)
        {
            tetrisboard.Draw()
        }        
    }
}
