using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tetris
{
    public class JTetromino : Tetromino
    {
        private bool isUpright = true;
        private bool isRight = false;
        private bool isLeft = false;
        private bool isDown = false;

        public JTetromino(Canvas canvas) : base(canvas)
        {
            this.Blocks.Add(new TetrisBlock(200, 0, this.Color, canvas));
            this.Blocks.Add(new TetrisBlock(200, 50, this.Color, canvas));
            this.Blocks.Add(new TetrisBlock(200, 100, this.Color, canvas));
            this.Blocks.Add(new TetrisBlock(150, 100, this.Color, canvas));
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
                Blocks[3].Y += 50;
                Draw();
                isUpright = false;
                isLeft = true;
            }

            else if (isLeft)
            {
                Blocks[0].X += 100;
                Blocks[0].Y += 100;
                Blocks[1].X += 50;
                Blocks[1].Y += 50;
                Blocks[3].X += 50;
                Blocks[3].Y -= 50;
                Draw();
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
                Blocks[3].Y -= 50;
                Draw();
                isDown = false;
                isRight = true;
            }

            else if (isRight)
            {
                Blocks[0].X -= 100;
                Blocks[0].Y -= 100;
                Blocks[1].X -= 50;
                Blocks[1].Y -= 50;
                Blocks[3].X -= 50;
                Blocks[3].Y += 50;
                Draw();
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
                Blocks[3].X += 50;
                Blocks[3].Y -= 50;
                Draw();
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
                Blocks[3].Y -= 50;
                Draw();
                isLeft = false;
                isUpright = true;
            }

            else if (isDown)
            {
                Blocks[0].X -= 100;
                Blocks[0].Y -= 100;
                Blocks[1].X -= 50;
                Blocks[1].Y -= 50;
                Blocks[3].X -= 50;
                Blocks[3].Y += 50;
                Draw();
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
                Blocks[3].Y += 50;
                Draw();
                isRight = false;
                isDown = true;
            }
            #endregion
        }
    }
}
