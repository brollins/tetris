using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace Tetris
{
    public class TetrisBoard
    {
        protected Collection<Tetromino> tetrominosOnScreen;
        private Queue<Tetromino> tetrominoQueue;
        private object drawingContext;
        private Tetromino currentTetromino;
        private Tetromino nextTetromino;
        private static Random random = new Random();
        private int previewOffset = 7;
        private int clearLocation = 200;
        private DispatcherTimer timer = new DispatcherTimer();
        private int topRow = 1;
        private int bottomRow = 20;
        private int leftMostColumn = 1;
        private int rightMostColumn = 10;

        public TetrisBoard() : this(null)
        {

        }

        public TetrisBoard(object drawingContext)
        {
            this.drawingContext = drawingContext;
        }

        public object DrawingContext
        {
            get
            {
                return drawingContext;
            }

            set
            {
                drawingContext = value;
            }
        }

        public Tetromino CurrentTetromino
        {
            get
            {
                return currentTetromino;
            }

            set
            {
                currentTetromino = value;
            }
        }

        public int TopRow
        {
            get
            {
                return topRow;
            }            
        }

        public int BottomRow
        {
            get
            {
                return bottomRow;
            }            
        }

        public int LeftMostColumn
        {
            get
            {
                return leftMostColumn;
            }            
        }

        public int RightMostColumn
        {
            get
            {
                return rightMostColumn;
            }
        }

        public void StartGame()
        {
            tetrominosOnScreen = new Collection<Tetromino>();
            tetrominoQueue = new Queue<Tetromino>();

            DropNewTetromino();

            timer.Interval = TimeSpan.FromSeconds(1.25);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {

            if (CurrentTetromino.IsAtBottom())
            {
                tetrominosOnScreen.Add(CurrentTetromino);
                DropNewTetromino();
            }
            else
            {
                if (!IsColliding())
                {
                    CurrentTetromino.Drop();
                    ClearCompletedLines();
                }
            }
        }

        private void DropNewTetromino()
        {
            bool gameOver = false;
            if (CurrentTetromino == null)
            {
                tetrominoQueue.Enqueue(GetRandomTetromino());
                CurrentTetromino = GetRandomTetromino();
                CurrentTetromino.Draw();
            }
            else
            {
                CurrentTetromino = tetrominoQueue.Dequeue();
                CurrentTetromino.Draw();

                foreach (var tetrominoOnScreen in tetrominosOnScreen)
                {
                    foreach (var block in tetrominoOnScreen.Blocks)
                    {
                        foreach (var currentblock in CurrentTetromino.Blocks)
                        {
                            if (block.Column == currentblock.Column && block.Row == currentblock.Row)
                            {
                                gameOver = true;
                            }
                        }
                    }
                }

                if (gameOver)
                {
                    timer.Stop();
                    if (MessageBox.Show(null, "Would you like to play again?", "Game Over", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        StartGame();
                    }
                    else
                    {
                        //need to figure out how to stop the game.
                    }
                }
                else
                {
                    tetrominoQueue.Enqueue(GetRandomTetromino());
                }
            }
        }

        public Tetromino GetRandomTetromino()
        {
            int randomNumber = random.Next(1, 8);
            Tetromino randomTetromino = null;
            switch (randomNumber)
            {                
                case 1:
                    randomTetromino = new ITetromino(this, tetrominosOnScreen);
                    break;
                case 2:
                    randomTetromino = new JTetromino(this, tetrominosOnScreen);
                    break;
                case 3:
                    randomTetromino = new OTetromino(this, tetrominosOnScreen);
                    break;
                case 4:
                    randomTetromino = new ZTetromino(this, tetrominosOnScreen);
                    break;
                case 5:
                    randomTetromino = new STetromino(this, tetrominosOnScreen);
                    break;
                case 6:
                    randomTetromino = new LTetromino(this, tetrominosOnScreen);
                    break;
                case 7:
                    randomTetromino = new TTetromino(this, tetrominosOnScreen);
                    break;
                default:
                    randomTetromino = new ITetromino(this, tetrominosOnScreen);
                    break;
            }
            return randomTetromino;
        }

        public bool IsColliding()
        {
            bool isColliding = false;
            foreach (var tetrominoOnScreen in tetrominosOnScreen)
            {
                foreach (var blockOnScreen in tetrominoOnScreen.Blocks)
                {
                    foreach (var block in CurrentTetromino.Blocks)
                    {
                        if (block.Row + 1 == blockOnScreen.Row && block.Column == blockOnScreen.Column)
                        {
                            isColliding = true;
                        }
                    }
                    if (blockOnScreen.Row == TopRow)
                    {
                        isColliding = true;
                    }
                }
            }
            return isColliding;
        }

        public void Draw(int column, int row, Color color)
        {
            DrawCore(column, row, color);
        }

        protected virtual void DrawCore(int column, int row, Color color)
        {
            //public void Draw()
            //{
            //    if (BlockGraphic == null)
            //    {
            //        Rectangle rect = new Rectangle();
            //        Canvas.SetTop(rect, y);
            //        Canvas.SetLeft(rect, x);
            //        rect.Height = 48;
            //        rect.Width = 48;
            //        rect.Stroke = Brushes.White;
            //        rect.StrokeThickness = 1;
            //        rect.Fill = new SolidColorBrush(Color);
            //        board.Children.Add(rect);
            //        BlockGraphic = rect;
            //    }

            //    else
            //    {
            //        Canvas.SetTop(BlockGraphic, y);
            //        Canvas.SetLeft(BlockGraphic, X);
            //    }
            //}
        }

        private void ClearCompletedLines()
        {
            Collection<TetrisBlock> blocksInALine = new Collection<TetrisBlock>();
            for (int i = 1000; i > 0; i -= 50)
            {
                foreach (var tetrominoOnScreen in tetrominosOnScreen)
                {
                    foreach (var blockOnScreen in tetrominoOnScreen.Blocks)
                    {
                        if (blockOnScreen.Row == i)
                        {
                            blocksInALine.Add(blockOnScreen);
                        }
                    }
                }
                if (blocksInALine.Count == 10)
                {
                    foreach (var block in blocksInALine)
                    {
                        Canvas.SetTop(block.BlockGraphic, -clearLocation);
                        Canvas.SetLeft(block.BlockGraphic, -clearLocation);
                        block.Column = -clearLocation;
                        block.Row = -clearLocation;
                        board.Children.Remove(block.BlockGraphic);
                        Score += 1000;

                    }

                    foreach (var tetrominoOnScreen in tetrominosOnScreen)
                    {
                        foreach (var block in tetrominoOnScreen.Blocks)
                        {
                            if (block.Column < i)
                            {
                                block.MoveDown();
                            }
                        }
                    }
                }
                blocksInALine.Clear();
            }
        }

    }
}
