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
        private DispatcherTimer timer = new DispatcherTimer();
        public event PropertyChangedEventHandler PropertyChanged;
        private static Random random = new Random();
        private Tetromino currentTetromino;
        private object drawingContext;
        private int previewOffset = 7;
        private int clearLocation = 200;
        private int topRow = 0;
        private int bottomRow = 19;
        private int leftMostColumn = 0;
        private int rightMostColumn = 9;
        private int score;
        
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
            // updates score on label when score is updated.

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
            // Initialization of collection and queue to be used in dropping tetrominos as well as base score.
            tetrominosOnScreen = new Collection<Tetromino>();
            tetrominoQueue = new Queue<Tetromino>();
            Score = 0;
            
            // Reset of current tetromino for restarting game. 
            currentTetromino = null;

            DropNewTetromino();

            // Timer interval will be modified as lines clear.  Timer will be stopped when game ends. 
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
                // TetrominosOnScreen is a collection of "locked" blocks.
                tetrominosOnScreen.Add(CurrentTetromino);
                ClearCompletedLines();
                DropNewTetromino();
                RedrawBoard();
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
                // Enqueueing tetrominos in order to facilitate peeking to draw the upcoming tetromino.
                tetrominoQueue.Enqueue(GetRandomTetromino());
                CurrentTetromino = GetRandomTetromino();
                CurrentTetromino.Draw();
            }
            else
            {
                // Dequeueing tetromino to become the new current tetromino.
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
                        timer.Stop();
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

            // Looping through all tetrominos "locked" on screen and the current tetromino to check if they would collide.
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
            // Drawing new rectangle and adding it to drawing object.
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

            // Called to draw all blocks on the screen in their new positions.
            foreach (var tetrominoOnScreen in tetrominosOnScreen)
            {
                tetrominoOnScreen.Draw();
            }
            currentTetromino.Draw();

            if (tetrominoQueue.Count > 0)
            {
                Tetromino nextTetromino = tetrominoQueue.Peek();
                foreach (var block in nextTetromino.Blocks)
                {
                    // Loops through next block in queue and draws it as a preview to the side of the board.
                    block.Column += previewOffset;
                    block.Draw(this, nextTetromino.Color);
                    block.Column -= previewOffset;
                }
            }
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
                // Looping through all "locked" tetrominos and adding them to a list to check completed lines.
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
                    // Executed if there are completed rows.
                    foreach (var block in blocksInALine)
                    {
                        block.Column = clearLocation;
                        block.Row = clearLocation;                       
                    }

                    // Score incremented for each line completed.  Then the speed is increased every 5000 points/5 completed lines.
                    Score += 1000;
                    double level = Score / 5000;
                    timer.Interval = TimeSpan.FromSeconds(1.25 - (level * .1));

                    foreach (var tetrominoOnScreen in tetrominosOnScreen)
                    {
                        // Move "locked" blocks down to compensate for cleared lines.
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
