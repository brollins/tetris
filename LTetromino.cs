using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tetris
{
    public class LTetromino : Tetromino
    {
        public LTetromino(Canvas canvas) : base(canvas)
        {
            this.Blocks.Add(new TetrisBlock(200, 1, this.Color, canvas));
            this.Blocks.Add(new TetrisBlock(200, 50, this.Color, canvas));
            this.Blocks.Add(new TetrisBlock(200, 100, this.Color, canvas));
            this.Blocks.Add(new TetrisBlock(250, 100, this.Color, canvas));
        }

        public override void RotateCounterClockwise()
        {
            throw new NotImplementedException();
        }

        public override void RotateClockwise()
        {
            throw new NotImplementedException();
        }
    }
}
