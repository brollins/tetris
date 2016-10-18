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
            tetromino = new ITetromino(canvas);
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
                tetromino.RotateLeft();
            }

            if (e.Key == Key.D)
            {
                tetromino.RotateRight();
            }
        }
    }
}

