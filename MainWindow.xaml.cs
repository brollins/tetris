using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using System.Collections.ObjectModel;
using Tetris;
using System.Collections.Generic;
using System.Windows.Controls;
using System.ComponentModel;

namespace WpfApplication2
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private Tetromino tetromino;
        private Collection<Tetromino> tetrominosOnScreen;
        private Queue<Tetromino> tetrominoQueue;
        private Tetromino nextTetromino;
        private static Random random = new Random();
        private int score;
        private DispatcherTimer timer = new DispatcherTimer();

        public int Score
        {
            get
            {
                return score;
            }

            set
            {
                score = value;
                if (PropertyChanged != null)
                {
                    var args = new PropertyChangedEventArgs("Score");
                    PropertyChanged(this, args);
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
            tetrominosOnScreen = new Collection<Tetromino>();
            tetrominoQueue = new Queue<Tetromino>();

            Score = 0;
            DropNewTetromino();

            timer.Interval = TimeSpan.FromSeconds(1.25);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {

            if (tetromino.IsAtBottom())
            {
                tetrominosOnScreen.Add(tetromino);
                DropNewTetromino();
            }
            else
            {
                if (!IsColliding())
                {
                    tetromino.Drop();
                    ClearLines();
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
                    foreach (var block in tetromino.Blocks)
                    {
                        if (block.Y + 50 == blockOnScreen.Y && block.X == blockOnScreen.X)
                        {
                            isColliding = true;
                        }
                    }
                    if (blockOnScreen.Y == 0)
                    {
                        isColliding = true;
                    }
                }
            }
            return isColliding;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Right)
            {
                if (!IsColliding())
                {
                    tetromino.MoveRight();
                }
            }

            if (e.Key == Key.Left)
            {
                if (!IsColliding())
                {
                    tetromino.MoveLeft();
                }
            }

            if (e.Key == Key.Down)
            {
                if (!IsColliding())
                {
                    tetromino.MoveDown();
                }
            }

            if (e.Key == Key.A)
            {
                if (!IsColliding())
                {
                    tetromino.RotateCounterClockwise();
                }
            }

            if (e.Key == Key.D)
            {
                if (!IsColliding())
                {
                    tetromino.RotateClockwise();
                }
            }
        }

        private int RandomNumber()
        {
            int randomNumber = random.Next(1, 8);
            return randomNumber;
        }

        public Tetromino RandomTetromino()
        {
            int randomNumber = RandomNumber();

            if (randomNumber == 1)
            {
                return new ITetromino(board, tetrominosOnScreen);
            }
            if (randomNumber == 2)
            {
                return new JTetromino(board, tetrominosOnScreen);
            }
            if (randomNumber == 3)
            {
                return new OTetromino(board, tetrominosOnScreen);
            }
            if (randomNumber == 4)
            {
                return new ZTetromino(board, tetrominosOnScreen);
            }
            if (randomNumber == 5)
            {
                return new STetromino(board, tetrominosOnScreen);
            }
            if (randomNumber == 6)
            {
                return new LTetromino(board, tetrominosOnScreen);
            }
            if (randomNumber == 7)
            {
                return new TTetromino(board, tetrominosOnScreen);
            }
            else
            {
                return null;
            }
        }
        private void ClearLines()
        {
            Collection<TetrisBlock> blocksInALine = new Collection<TetrisBlock>();
            for (int i = 1000; i > 0; i -= 50)
            {
                foreach (var tetrominoOnScreen in tetrominosOnScreen)
                {
                    foreach (var blockOnScreen in tetrominoOnScreen.Blocks)
                    {
                        if (blockOnScreen.Y == i)
                        {
                            blocksInALine.Add(blockOnScreen);
                        }
                    }
                }
                if (blocksInALine.Count == 10)
                {
                    foreach (var block in blocksInALine)
                    {
                        Canvas.SetTop(block.BlockGraphic, -10000);
                        Canvas.SetLeft(block.BlockGraphic, -10000);
                        block.X = -10000;
                        block.Y = -10000;
                        board.Children.Remove(block.BlockGraphic);
                        Score += 1000;

                    }

                    foreach (var tetrominoOnScreen in tetrominosOnScreen)
                    {
                        foreach (var block in tetrominoOnScreen.Blocks)
                        {
                            if (block.Y < i)
                            {
                                block.Drop();
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
            if (tetromino == null)
            {
                tetrominoQueue.Enqueue(RandomTetromino());
                nextTetromino = tetrominoQueue.Peek();
                foreach (var block in nextTetromino.Blocks)
                {
                    Canvas.SetTop(block.BlockGraphic, block.X + 350);
                    block.X = block.X + 350;
                }
                nextTetromino.Draw();
                tetromino = RandomTetromino();
                tetromino.Draw();
            }
            else
            {
                tetromino = tetrominoQueue.Dequeue();

                foreach (var block in tetromino.Blocks)
                {
                    Canvas.SetTop(block.BlockGraphic, block.X - 350);
                    block.X = block.X - 350;
                }

                tetromino.Draw();

                foreach (var tetrominoOnScreen in tetrominosOnScreen)
                {
                    foreach (var block in tetrominoOnScreen.Blocks)
                    {
                        foreach (var currentblock in tetromino.Blocks)
                        {
                            if (block.X == currentblock.X && block.Y == currentblock.Y)
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
                        tetromino = null;
                        DropNewTetromino();
                    }
                    else
                    {
                        this.Close();
                    }
                }
                else
                {
                    tetrominoQueue.Enqueue(RandomTetromino());
                    nextTetromino = tetrominoQueue.Peek();
                    foreach (var block in nextTetromino.Blocks)
                    {
                        Canvas.SetTop(block.BlockGraphic, block.X + 350);
                        block.X = block.X + 350;
                    }
                    nextTetromino.Draw();
                }
            }
        }
    }
}

