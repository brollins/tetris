using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tetris
{
    public class OTetromino : Tetromino
    {
        public OTetromino(Canvas canvas) : base(canvas)
        {
            this.Blocks.Add(new TetrisBlock(200, 0, this.Color, canvas));
            this.Blocks.Add(new TetrisBlock(250, 0, this.Color, canvas));
            this.Blocks.Add(new TetrisBlock(200, 50, this.Color, canvas));
            this.Blocks.Add(new TetrisBlock(250, 50, this.Color, canvas));
        }

        public override void RotateCounterClockwise()
        {            
        }

        public override void RotateClockwise()
        {
        }
    }
}
