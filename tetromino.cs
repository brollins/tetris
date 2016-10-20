using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tetris
{
    public abstract class Tetromino
    {
        private Collection<TetrisBlock> blocks;
        private Canvas canvas;
        private Color color;
        public bool atBottom = false;


        public Tetromino()
        {
        }

        public Tetromino(Canvas board)
        {
            Blocks = new Collection<TetrisBlock>();
            this.canvas = board;

        }

        public Collection<TetrisBlock> Blocks
        {
            get
            {
                return blocks;
            }

            set
            {
                blocks = value;
            }
        }

        public Color Color
        {
            get
            {
                if (color == new Color())
                {
                    Random random = new Random();
                    color = Color.FromRgb((byte)random.Next(0, 255), (byte)random.Next(0, 255), (byte)random.Next(0, 255));
                }
                return color;
            }

            set
            {
                color = value;
            }
        }

        public void Drop()
        {
            if (!IsAtBottom())
            {
                foreach (var tetrisBlock in Blocks)
                {
                    tetrisBlock.Drop();
                }
            }
        }

        public void MoveLeft()
        {
            if (!IsAtBottom() && CanMoveLeft())
            {
                foreach (var tetrisBlock in Blocks)
                {
                    tetrisBlock.MoveLeft();
                }
            }
        }

        public void MoveRight()
        {
            if (!IsAtBottom() && CanMoveRight())
            {
                foreach (var tetrisBlock in Blocks)
                {
                    tetrisBlock.MoveRight();
                }
            }
        }

        public void MoveDown()
        {
            if (!IsAtBottom())
            {
                foreach (var tetrisBlock in Blocks)
                {
                    tetrisBlock.MoveDown();
                }
            }
        }

        public void Draw()
        {
            foreach (var tetrisBlock in Blocks)
            {
                tetrisBlock.Draw();
            }
        }

        public bool IsAtBottom()
        {
            foreach (var tetrisblock in Blocks)
            {
                if (tetrisblock.Y >= 951)
                {
                    atBottom = true;
                }
            }
            return atBottom;
        }

        public bool CanMoveLeft()
        {
            bool canMoveLeft = true;
            foreach (var tetrisblock in blocks)
            {
                if (tetrisblock.X == 0)
                {
                    canMoveLeft = false;
                }
            }
            return canMoveLeft;
        }

        public bool CanMoveRight()
        {
            bool canMoveRight = true;
            foreach (var tetrisblock in blocks)
            {
                if (tetrisblock.X >= 450)
                {
                    canMoveRight = false;
                }
            }
            return canMoveRight;
        }

        protected virtual void RotateCounterClockwiseCore()
        {

        }

        protected virtual void RotateClockwiseCore()
        {

        }

        public void RotateCounterClockwise()
        {
            if (!IsAtBottom())
            {
                RotateCounterClockwiseCore();
            }
        }

        public void RotateClockwise()
        {
            if (!IsAtBottom())
            {
                RotateClockwiseCore();
            }
        }
    }
}
