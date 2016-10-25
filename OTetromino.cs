using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tetris
{
    public class OTetromino : Tetromino
    {
        public OTetromino(Canvas canvas, Collection<Tetromino> tetrominosOnScreen) : base(canvas, tetrominosOnScreen)
        {
            this.Blocks.Add(new TetrisBlock(4, 0));
            this.Blocks.Add(new TetrisBlock(5, 0));
            this.Blocks.Add(new TetrisBlock(4, 0));
            this.Blocks.Add(new TetrisBlock(5, 0));
        }

        protected override void RotateCounterClockwiseCore()
        {            
        }

        protected override void RotateClockwiseCore()
        {
        }
    }
}
