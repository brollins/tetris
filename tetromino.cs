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

        public Tetromino()
        {

        }

        public Tetromino(Canvas canvas)
        {
            Blocks = new Collection<TetrisBlock>();
            this.canvas = canvas;

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
            if (!isAtBottom())
            {
                foreach (var tetrisBlock in Blocks)
                {
                    tetrisBlock.Drop();
                }
            }
        }

        public void MoveLeft()
        {
            if (!isAtBottom())
            {
                foreach (var tetrisBlock in Blocks)
                {
                    tetrisBlock.MoveLeft();
                }
            }
        }

        public void MoveRight()
        {
            if (!isAtBottom())
            {
                foreach (var tetrisBlock in Blocks)
                {
                    tetrisBlock.MoveRight();
                }
            }
        }

        public void MoveDown()
        {
            if (!isAtBottom())
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

        public bool isAtBottom()
        {
            bool atBottom = false;
            foreach (var tetrisblock in Blocks)
            {
                if (tetrisblock.Y >= 950)
                {
                    atBottom = true;
                }
            }
            return atBottom;
        }

        protected virtual void RotateCounterClockwiseCore()
        {

        }

        protected virtual void RotateClockwiseCore()
        {

        }

        public void RotateCounterClockwise()
        {
            if (!isAtBottom())
            {
                RotateCounterClockwiseCore();
            }
        }

        public void RotateClockwise()
        {
            if (!isAtBottom())
            {
                RotateClockwiseCore();
            }
        }
    }
}
