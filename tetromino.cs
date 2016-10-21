using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tetris
{
    public abstract class Tetromino
    {
        private Collection<TetrisBlock> blocks;
        protected Canvas canvas;
        private Color color;
        protected Collection<Tetromino> tetrominosOnScreen;
        private static Random random = new Random();


        public Tetromino()
        {
        }

        public Tetromino(Canvas board, Collection<Tetromino> tetrominosOnScreen)
        {
            Blocks = new Collection<TetrisBlock>();
            this.canvas = board;
            this.tetrominosOnScreen = tetrominosOnScreen;
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
                    color = Color.FromRgb((byte)random.Next(0, 255), (byte)random.Next(0, 255), (byte)random.Next(0, 255));
                    Debug.WriteLine(color);
                }
                return color;
            }

            set
            {
                color = value;
            }
        }

        public abstract Tetromino Clone();


        public void Drop()
        {
            if (!IsAtBottom() && !IsTouching())
            {
                foreach (var tetrisBlock in Blocks)
                {
                    tetrisBlock.Drop();
                }
            }
        }

        public void MoveLeft()
        {
            if (!IsAtBottom())
            {
                foreach (var tetrisBlock in Blocks)
                {
                    tetrisBlock.MoveLeft();
                }
                if (!IsValidPosition())
                {
                    foreach (var tetrisBlock in Blocks)
                    {
                        tetrisBlock.MoveRight();
                    }
                }
            }
        }

        public void MoveRight()
        {
            if (!IsAtBottom())
            {
                foreach (var tetrisBlock in Blocks)
                {
                    tetrisBlock.MoveRight();
                }
                if (!IsValidPosition())
                {
                    foreach (var tetrisBlock in Blocks)
                    {
                        tetrisBlock.MoveLeft();
                    }
                }
            }
        }

        public void MoveDown()
        {
            if (!IsAtBottom() && !IsTouching())
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
            bool atBottom = false;
            foreach (var tetrisblock in Blocks)
            {
                if (tetrisblock.Y >= 951 || IsTouching())
                {
                    atBottom = true;
                }
            }
            return atBottom;
        }

        public bool IsValidPosition()
        {
            bool isValidPosition = true;
            foreach (var tetrisblock in Blocks)
            {
                if (tetrisblock.X > 450)
                    isValidPosition = false;

                if (tetrisblock.X < 0)
                    isValidPosition = false;

                if (tetrisblock.Y > 950)
                    isValidPosition = false;

                foreach (var tetrominoOnScreen in tetrominosOnScreen)
                {
                    foreach (var block in tetrominoOnScreen.Blocks)
                    {
                        if (block.X == tetrisblock.X && block.Y == tetrisblock.Y)
                        {
                            isValidPosition = false;
                        }
                    }
                }
            }
            return isValidPosition;
        }

        public bool IsTouching()
        {
            bool isTouching = false;
            foreach (var tetrominoOnScreen in tetrominosOnScreen)
            {
                foreach (var blockOnScreen in tetrominoOnScreen.Blocks)
                {
                    foreach (var block in Blocks)
                    {
                        if (block.Y + 50 == blockOnScreen.Y && block.X == blockOnScreen.X)
                        {
                            isTouching = true;
                        }
                    }
                }
            }
            return isTouching;
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
                if (!IsValidPosition())
                {
                    RotateClockwiseCore();
                }

                Draw();
            }
        }

        public void RotateClockwise()
        {
            if (!IsAtBottom())
            {
                RotateClockwiseCore();
                if (!IsValidPosition())
                {
                    RotateCounterClockwiseCore();
                }
                Draw();
            }
        }
    }
}
