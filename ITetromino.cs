﻿using System;
using System.Windows.Controls;

namespace Tetris
{
    public class ITetromino : Tetromino
    {
        private bool isUpright = true;
        private bool isRight = false;
        private bool isLeft = false;
        private bool isDown = false;

        public ITetromino(Canvas canvas) : base(canvas)
        {
            this.Blocks.Add(new TetrisBlock(200, 1, this.Color, canvas));
            this.Blocks.Add(new TetrisBlock(200, 50, this.Color, canvas));
            this.Blocks.Add(new TetrisBlock(200, 100, this.Color, canvas));
            this.Blocks.Add(new TetrisBlock(200, 150, this.Color, canvas));
        }

        public override void RotateLeft()
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
                Blocks[3].X -= 50;
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
                Blocks[3].Y += 50;
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
                Blocks[3].X += 50;
                Blocks[3].Y += 50;
                Draw();
                isRight = false;
                isUpright = true;
            }
            #endregion
        }

        public override void RotateRight()
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
                Blocks[3].Y += 50;
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
                Blocks[3].X += 50;
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
                Blocks[3].Y -= 50;
                Draw();
                isRight = false;
                isDown = true;
            }
            #endregion
        }
    }
}
