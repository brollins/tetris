using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Tetris;

namespace WpfApplication2
{
    public partial class MainWindow : Window
    {
        private Tetromino tetromino;

        public MainWindow()
        {
            InitializeComponent();

            tetromino = RandomTetromino();
            tetromino.Draw();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(2);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            tetromino.Drop();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Right)
            {
                tetromino.MoveRight();
            }

            if (e.Key == Key.Left)
            {
                tetromino.MoveLeft();
            }

            if (e.Key == Key.Down)
            {
                tetromino.MoveDown();
            }

            if (e.Key == Key.A)
            {
                tetromino.RotateCounterClockwise();
            }

            if (e.Key == Key.D)
            {
                tetromino.RotateClockwise();
            }
        }

        private int RandomNumber ()
        {
            Random random = new Random();
            int randomNumber = random.Next(0, 8);
            return randomNumber;
        }

        public Tetromino RandomTetromino()
        {
            int randomNumber = RandomNumber();

            if (randomNumber == 1)
            {
                return new ITetromino(canvas);
            }
            if (randomNumber == 2)
            {
                return new JTetromino(canvas);
            }
            if (randomNumber == 3)
            {
                return new OTetromino(canvas);
            }
            if (randomNumber == 4)
            {
                return new ZTetromino(canvas);
            }
            if (randomNumber == 5)
            {
                return new STetromino(canvas);
            }
            if (randomNumber == 6)
            {
                return new LTetromino(canvas);
            }
            if (randomNumber == 7)
            {
                return new TTetromino(canvas);
            }
            else
            {
                return null;
            }
        }

    }
}

