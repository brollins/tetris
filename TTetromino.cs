using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tetris
{
    public class TTetromino : Tetromino
    {
        private bool isUpright = true;
        private bool isRight = false;
        private bool isLeft = false;
        private bool isDown = false;

        public TTetromino(Canvas canvas, Collection<Tetromino> tetrominosOnScreen) : base(canvas, tetrominosOnScreen)
        {
            this.Blocks.Add(new TetrisBlock(3, 0));
            this.Blocks.Add(new TetrisBlock(4, 0));
            this.Blocks.Add(new TetrisBlock(5, 0));
            this.Blocks.Add(new TetrisBlock(4, 50));
        }

        protected override void RotateCounterClockwiseCore()
        {
            if (isUpright)
            #region
            {
                Blocks[0].Column += 50;
                Blocks[0].Row += 50;
                Blocks[2].Column -= 50;
                Blocks[2].Row -= 50;
                Blocks[3].Column += 50;
                Blocks[3].Row -= 50;
                isUpright = false;
                isLeft = true;
            }

            else if (isLeft)
            {
                Blocks[0].Column += 50;
                Blocks[0].Row -= 50;
                Blocks[2].Column -= 50;
                Blocks[2].Row += 50;
                Blocks[3].Column -= 50;
                Blocks[3].Row -= 50;
                isLeft = false;
                isDown = true;
            }

            else if (isDown)
            {
                Blocks[0].Column -= 50;
                Blocks[0].Row -= 50;
                Blocks[2].Column += 50;
                Blocks[2].Row += 50;
                Blocks[3].Column -= 50;
                Blocks[3].Row += 50;
                isDown = false;
                isRight = true;
            }

            else if (isRight)
            {
                Blocks[0].Column -= 50;
                Blocks[0].Row += 50;
                Blocks[2].Column += 50;
                Blocks[2].Row -= 50;
                Blocks[3].Column += 50;
                Blocks[3].Row += 50;
                isRight = false;
                isUpright = true;
            }
            #endregion
        }

        protected override void RotateClockwiseCore()
        {
            if (isUpright)
            #region
            {
                Blocks[0].Column += 50;
                Blocks[0].Row -= 50;
                Blocks[2].Column -= 50;
                Blocks[2].Row += 50;
                Blocks[3].Column -= 50;
                Blocks[3].Row -= 50;
                isUpright = false;
                isRight = true;
            }

            else if (isLeft)
            {
                Blocks[0].Column -= 50;
                Blocks[0].Row -= 50;
                Blocks[2].Column += 50;
                Blocks[2].Row += 50;
                Blocks[3].Column -= 50;
                Blocks[3].Row += 50;
                isLeft = false;
                isUpright = true;
            }

            else if (isDown)
            {
                Blocks[0].Column -= 50;
                Blocks[0].Row += 50;
                Blocks[2].Column += 50;
                Blocks[2].Row -= 50;
                Blocks[3].Column += 50;
                Blocks[3].Row += 50;
                isDown = false;
                isLeft = true;
            }

            else if (isRight)
            {
                Blocks[0].Column += 50;
                Blocks[0].Row += 50;
                Blocks[2].Column -= 50;
                Blocks[2].Row -= 50;
                Blocks[3].Column += 50;
                Blocks[3].Row -= 50;
                isRight = false;
                isDown = true;
            }
            #endregion
        }
    }
}
