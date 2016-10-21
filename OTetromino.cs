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
            this.Blocks.Add(new TetrisBlock(200, 0, this.Color, canvas));
            this.Blocks.Add(new TetrisBlock(250, 0, this.Color, canvas));
            this.Blocks.Add(new TetrisBlock(200, 50, this.Color, canvas));
            this.Blocks.Add(new TetrisBlock(250, 50, this.Color, canvas));
        }

        protected override void RotateCounterClockwiseCore()
        {            
        }

        protected override void RotateClockwiseCore()
        {
        }
        public override Tetromino Clone()
        {
            OTetromino otetromino = new OTetromino(canvas, tetrominosOnScreen);
            otetromino.Color = this.Color;
            return otetromino;
        }
    }
}
