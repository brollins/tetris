using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Tetris
{
    public class TetrisBoard : INotifyPropertyChanged
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
        private int topRow = 0;
        private int bottomRow = 19;
        private int leftMostColumn = 0;
        private int rightMostColumn = 9;
        private int score;
        public event PropertyChangedEventHandler PropertyChanged;


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

        public int Score
        {
            get
            {
                return score;
            }

            set
            {
                score = value;
                OnPropertyChanged("Score");               
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                var args = new PropertyChangedEventArgs(propertyName);
                PropertyChanged(this, args);
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
            currentTetromino = null;

            DropNewTetromino();

            timer.Interval = TimeSpan.FromSeconds(1.25);
            timer.Tick -= timer_Tick;
            timer.Tick += timer_Tick;

            timer.Start();
            RedrawBoard();
        }

        void timer_Tick(object sender, EventArgs e)
        {

            if (CurrentTetromino.IsAtBottom())
            {
                tetrominosOnScreen.Add(CurrentTetromino);
                ClearCompletedLines();
                DropNewTetromino();

            }
            else
            {
                if (!IsColliding())
                {
                    Drop();
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
                                timer.Stop();
                            }
                        }
                    }
                }

                if (gameOver)
                {
                    timer.Stop();
                    if (MessageBox.Show("Would you like to play again?", "Game Over", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
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
            Canvas playArea = (Canvas)drawingContext;
            Rectangle rect = new Rectangle();
            Canvas.SetTop(rect, row * 50);
            Canvas.SetLeft(rect, column * 50);
            rect.Height = 48;
            rect.Width = 48;
            rect.Stroke = Brushes.White;
            rect.StrokeThickness = 1;
            rect.Fill = new SolidColorBrush(color);
            playArea.Children.Add(rect);

        }

        public void Drop()
        {
            currentTetromino.Drop();
            RedrawBoard();
        }

        public void MoveLeft()
        {
            currentTetromino.MoveLeft();
            RedrawBoard();

        }

        public void MoveRight()
        {
            currentTetromino.MoveRight();
            RedrawBoard();

        }

        public void MoveDown()
        {
            currentTetromino.MoveDown();
            RedrawBoard();

        }

        public void RotateCounterClockwise()
        {
            currentTetromino.RotateCounterClockwise();
            RedrawBoard();

        }

        public void RotateClockwise()
        {
            currentTetromino.RotateClockwise();
            RedrawBoard();

        }

        public void RedrawBoard()
        {
            ClearCanvas();
            foreach (var tetrominoOnScreen in tetrominosOnScreen)
            {
                tetrominoOnScreen.Draw();
            }
            currentTetromino.Draw();

        }

        public void ClearCanvas()
        {
            Canvas playArea = (Canvas)drawingContext;
            playArea.Children.Clear();
        }

        private void ClearCompletedLines()
        {
            Collection<TetrisBlock> blocksInALine = new Collection<TetrisBlock>();
            for (int i = bottomRow + 1; i > 0; i -= 1)
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
                        block.Column = clearLocation;
                        block.Row = clearLocation;
                        Score += 1000;

                    }

                    foreach (var tetrominoOnScreen in tetrominosOnScreen)
                    {
                        foreach (var block in tetrominoOnScreen.Blocks)
                        {
                            if (block.Row <= i)
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
