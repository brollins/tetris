using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Tetris
{
    public class ITetromino: Tetromino
    {
        public ITetromino(Canvas canvas): base(canvas)
        {
            this.Blocks.Add(new TetrisBlock(200, 1, Color.FromArgb(255, 245, 25, 235), canvas));
            this.Blocks.Add(new TetrisBlock(200, 50, Color.FromArgb(255, 245, 25, 235), canvas));
            this.Blocks.Add(new TetrisBlock(250, 50, Color.FromArgb(255, 245, 25, 235), canvas));
        }

    }
}
