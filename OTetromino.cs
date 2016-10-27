using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tetris
{
    public class OTetromino : Tetromino
    {
        public OTetromino(TetrisBoard tetrisBoard, Collection<Tetromino> tetrominosOnScreen) : base(tetrisBoard, tetrominosOnScreen)
        {
            this.Blocks.Add(new TetrisBlock(4, 0));
            this.Blocks.Add(new TetrisBlock(5, 0));
            this.Blocks.Add(new TetrisBlock(4, 1));
            this.Blocks.Add(new TetrisBlock(5, 1));
        }
                
        protected override void RotateCounterClockwiseCore()
        {            
        }

        protected override void RotateClockwiseCore()
        {
        }
    }
}
