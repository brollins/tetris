using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using System.Collections.ObjectModel;
using Tetris;

namespace WpfApplication2
{
    public partial class MainWindow : Window
    {
        private Tetromino tetromino;
        private Collection<Tetromino> tetrominosOnScreen;


        public MainWindow()
        {
            InitializeComponent();
            tetrominosOnScreen = new Collection<Tetromino>();
            tetromino = RandomTetromino();
            tetromino.Draw();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(.5);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (tetromino == null)
            {
                tetromino = RandomTetromino();
                tetromino.Draw();
            }
            else
            {
                if (tetromino.IsAtBottom())
                {
                    tetrominosOnScreen.Add(tetromino);
                    tetromino = RandomTetromino();
                    tetromino.Draw();
                }
                else
                {
                    if (!IsColliding())
                    {
                        tetromino.Drop();
                    }
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
                            tetromino.atBottom = true;
                        }                        
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
            Random random = new Random();
            int randomNumber = random.Next(1, 7);
            return randomNumber;
        }

        public Tetromino RandomTetromino()
        {
            int randomNumber = RandomNumber();

            if (randomNumber == 1)
            {
                return new ITetromino(board);
            }
            if (randomNumber == 2)
            {
                return new JTetromino(board);
            }
            if (randomNumber == 3)
            {
                return new OTetromino(board);
            }
            if (randomNumber == 4)
            {
                return new ZTetromino(board);
            }
            if (randomNumber == 5)
            {
                return new STetromino(board);
            }
            if (randomNumber == 6)
            {
                return new LTetromino(board);
            }
            if (randomNumber == 7)
            {
                return new TTetromino(board);
            }
            else
            {
                return null;
            }
        }
        private bool ClearLines()
        {            
            bool isFull = false;
            foreach (var tetrominoOnScreen in tetrominosOnScreen)
            {
                foreach (var blockOnScreen in tetrominoOnScreen.Blocks)
                {
                    Console.WriteLine(blockOnScreen.X);
                }
            }
            return isFull;
        }
    }    
}

