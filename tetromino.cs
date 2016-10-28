using System;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace Tetris
{
    public abstract class Tetromino
    {
        private Collection<TetrisBlock> blocks;
        protected TetrisBoard tetrisBoard;
        private Color color;
        protected Collection<Tetromino> tetrominosOnScreen;
        private static Random random = new Random();
      

        public Tetromino()
        {
        }

        public Tetromino(TetrisBoard tetrisBoard, Collection<Tetromino> tetrominosOnScreen)
        {
            Blocks = new Collection<TetrisBlock>();
            this.tetrisBoard = tetrisBoard;
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
            if (!IsAtBottom() && !IsTouching())
            {
                foreach (var tetrisBlock in Blocks)
                {
                    tetrisBlock.MoveDown();
                }
            }
        }

        public void MoveLeft()
        {
            //if (!IsAtBottom())
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
            //if (!IsAtBottom())
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
                tetrisBlock.Draw(tetrisBoard, Color);
            }
        }

        public bool IsAtBottom()
        {
            bool atBottom = false;
            foreach (var tetrisblock in Blocks)
            {
                if (tetrisblock.Row > tetrisBoard.BottomRow || IsTouching())
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
                if (tetrisblock.Column > tetrisBoard.RightMostColumn)
                    isValidPosition = false;

                if (tetrisblock.Column < tetrisBoard.LeftMostColumn)
                    isValidPosition = false;

                foreach (var tetrominoOnScreen in tetrominosOnScreen)
                {
                    foreach (var block in tetrominoOnScreen.Blocks)
                    {
                        if (block.Column == tetrisblock.Column && block.Row == tetrisblock.Row)
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
                        if (block.Row + 1 == blockOnScreen.Row && block.Column == blockOnScreen.Column)
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
