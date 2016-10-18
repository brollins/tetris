using System;
using System.Windows.Controls;

namespace Tetris
{
    public class ITetromino : Tetromino
    {
        private bool isUpright = true;
        private bool isRight = false;
        private bool isLeft = false;

        public ITetromino(Canvas canvas) : base(canvas)
        {
            this.Blocks.Add(new TetrisBlock(200, 1, this.Color, canvas));
            this.Blocks.Add(new TetrisBlock(200, 50, this.Color, canvas));
            this.Blocks.Add(new TetrisBlock(200, 100, this.Color, canvas));
            this.Blocks.Add(new TetrisBlock(200, 150, this.Color, canvas));
        }

        public override void RotateLeft()
        {
            if (isUpright && !isLeft && !isRight)
            {
                Blocks[0].X = Blocks[0].X - 100;
                Blocks[0].Y = Blocks[0].Y + 100;
                Blocks[1].X = Blocks[1].X - 50;
                Blocks[1].Y = Blocks[1].Y + 50;
                Blocks[3].X = Blocks[3].X + 50;
                Blocks[3].Y = Blocks[3].Y - 50;
                Draw();
                isUpright = false;
                isLeft = true;
            } 
            
            if (!isUpright && !isLeft && !isRight)
            {
                Blocks[0].X = Blocks[0].X + 100;
                Blocks[0].Y = Blocks[0].Y - 100;
                Blocks[1].X = Blocks[1].X + 50;
                Blocks[1].Y = Blocks[1].Y - 50;
                Blocks[3].X = Blocks[3].X - 50;
                Blocks[3].Y = Blocks[3].Y + 50;
                Draw();
                isUpright = true;

            }          
        }

        public override void RotateRight()
        {
            if (isUpright && !isLeft && !isRight)
            {
                Blocks[0].X = Blocks[0].X + 100;
                Blocks[0].Y = Blocks[0].Y + 100;
                Blocks[1].X = Blocks[1].X + 50;
                Blocks[1].Y = Blocks[1].Y + 50;
                Blocks[3].X = Blocks[3].X - 50;
                Blocks[3].Y = Blocks[3].Y - 50;
                Draw();
                isUpright = false;
                isRight = true;
            }

            if (!isUpright && !isLeft && !isRight)
            {
                Blocks[0].X = Blocks[0].X - 100;
                Blocks[0].Y = Blocks[0].Y - 100;
                Blocks[1].X = Blocks[1].X - 50;
                Blocks[1].Y = Blocks[1].Y - 50;
                Blocks[3].X = Blocks[3].X + 50;
                Blocks[3].Y = Blocks[3].Y + 50;
                Draw();
                isUpright = true;
            }
        }  
    }
}
