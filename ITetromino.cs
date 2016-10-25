using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace Tetris
{
    public class ITetromino : Tetromino
    {
        private bool isUpright = true;
        private bool isRight = false;
        private bool isLeft = false;
        private bool isDown = false;

        public ITetromino(Canvas canvas, Collection<Tetromino> tetrominosOnScreen) : base(canvas, tetrominosOnScreen)
        {
            this.Blocks.Add(new TetrisBlock(200, 0, this.Color, canvas));
            this.Blocks.Add(new TetrisBlock(200, 50, this.Color, canvas));
            this.Blocks.Add(new TetrisBlock(200, 100, this.Color, canvas));
            this.Blocks.Add(new TetrisBlock(200, 150, this.Color, canvas));
        }

        protected override void RotateCounterClockwiseCore()
        {
            if (isUpright)
            #region
            {
                Blocks[0].X -= 100;
                Blocks[0].Y += 100;
                Blocks[1].X -= 50;
                Blocks[1].Y += 50;
                Blocks[3].X += 50;
                Blocks[3].Y -= 50;
                isUpright = false;
                isLeft = true;
            }

            else if (isLeft)
            {
                Blocks[0].X += 100;
                Blocks[0].Y += 100;
                Blocks[1].X += 50;
                Blocks[1].Y += 50;
                Blocks[3].X -= 50;
                Blocks[3].Y -= 50;
                isLeft = false;
                isDown = true;
            }

            else if (isDown)
            {
                Blocks[0].X += 100;
                Blocks[0].Y -= 100;
                Blocks[1].X += 50;
                Blocks[1].Y -= 50;
                Blocks[3].X -= 50;
                Blocks[3].Y += 50;
                isDown = false;
                isRight = true;
            }

            else if (isRight)
            {
                Blocks[0].X -= 100;
                Blocks[0].Y -= 100;
                Blocks[1].X -= 50;
                Blocks[1].Y -= 50;
                Blocks[3].X += 50;
                Blocks[3].Y += 50;
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
                Blocks[0].X += 100;
                Blocks[0].Y += 100;
                Blocks[1].X += 50;
                Blocks[1].Y += 50;
                Blocks[3].X -= 50;
                Blocks[3].Y -= 50;
                isUpright = false;
                isRight = true;
            }

            else if (isLeft)
            {
                Blocks[0].X += 100;
                Blocks[0].Y -= 100;
                Blocks[1].X += 50;
                Blocks[1].Y -= 50;
                Blocks[3].X -= 50;
                Blocks[3].Y += 50;
                isLeft = false;
                isUpright = true;
            }

            else if (isDown)
            {
                Blocks[0].X -= 100;
                Blocks[0].Y -= 100;
                Blocks[1].X -= 50;
                Blocks[1].Y -= 50;
                Blocks[3].X += 50;
                Blocks[3].Y += 50;
                isDown = false;
                isLeft = true;
            }

            else if (isRight)
            {
                Blocks[0].X -= 100;
                Blocks[0].Y += 100;
                Blocks[1].X -= 50;
                Blocks[1].Y += 50;
                Blocks[3].X += 50;
                Blocks[3].Y -= 50;
                isRight = false;
                isDown = true;
            }
            #endregion
        }
    }
}
