using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

        public bool IsColliding()
        {
            bool isColliding = false;
            foreach (var tetrominoOnScreen in tetrominosOnScreen)
            {
                foreach (var blockOnScreen in tetrominoOnScreen.Blocks)
                {
                    foreach (var block in CurrentTetromino.Blocks)
                    {
                        if (block.Column + 1 == blockOnScreen.Column && block.Row == blockOnScreen.Row)
                        {
                            isColliding = true;
                        }
                    }
                    if (blockOnScreen.Column == 0)
                    {
                        isColliding = true;
                    }
                }
            }
            return isColliding;
        }                

        public Tetromino GetRandomTetromino()
        {
            int randomNumber = random.Next(1, 8);

            if (randomNumber == 1)
            {
                return new ITetromino(this, tetrominosOnScreen);
            }
            if (randomNumber == 2)
            {
                return new JTetromino(this, tetrominosOnScreen);
            }
            if (randomNumber == 3)
            {
                return new OTetromino(this, tetrominosOnScreen);
            }
            if (randomNumber == 4)
            {
                return new ZTetromino(this, tetrominosOnScreen);
            }
            if (randomNumber == 5)
            {
                return new STetromino(this, tetrominosOnScreen);
            }
            if (randomNumber == 6)
            {
                return new LTetromino(this, tetrominosOnScreen);
            }
            if (randomNumber == 7)
            {
                return new TTetromino(this, tetrominosOnScreen);
            }
            else
            {
                return null;
            }
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

        private void DropNewTetromino()
        {
            bool gameOver = false;
            if (CurrentTetromino == null)
            {
                tetrominoQueue.Enqueue(GetRandomTetromino());
                nextTetromino = tetrominoQueue.Peek();
                foreach (var block in nextTetromino.Blocks)
                {
                    Canvas.SetTop(block.BlockGraphic, block.Column + previewOffset);
                    block.Column = block.Column + previewOffset;
                }
                nextTetromino.Draw();
                CurrentTetromino = GetRandomTetromino();
                CurrentTetromino.Draw();
            }
            else
            {
                CurrentTetromino = tetrominoQueue.Dequeue();

                foreach (var block in CurrentTetromino.Blocks)
                {
                    Canvas.SetTop(block.BlockGraphic, block.Column - previewOffset);
                    block.Column = block.Column - previewOffset;
                }

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
                    if (MessageBox.Show(this, "Would you like to play again?", "Game Over", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        tetrominosOnScreen.Clear();
                        board.Children.Clear();
                        Score = 0;
                        timer.Start();
                        tetrominoQueue.Clear();
                        CurrentTetromino = null;
                        DropNewTetromino();
                    }
                    else
                    {
                        this.Close();
                    }
                }
                else
                {
                    tetrominoQueue.Enqueue(GetRandomTetromino());
                    nextTetromino = tetrominoQueue.Peek();
                    foreach (var block in nextTetromino.Blocks)
                    {
                        Canvas.SetTop(block.BlockGraphic, block.Column + previewOffset);
                        block.Column = block.Column + previewOffset;
                    }
                    nextTetromino.Draw();
                }
            }
        }  
    }
}
