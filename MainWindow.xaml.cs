using System.Windows;
using System.Windows.Input;
using Tetris;
using System.ComponentModel;

namespace WpfApplication2
{
    public partial class MainWindow : Window
    {

        private TetrisBoard tetrisBoard;

        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
            tetrisBoard = new TetrisBoard(playArea);
            tetrisBoard.StartGame();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Right)
            {
                tetrisBoard.MoveRight();
            }

            if (e.Key == Key.Left)
            {
                tetrisBoard.MoveLeft();
            }

            if (e.Key == Key.Down)
            {
                tetrisBoard.MoveDown();
            }

            if (e.Key == Key.A)
            {
                tetrisBoard.RotateCounterClockwise();
            }

            if (e.Key == Key.D)
            {
                tetrisBoard.RotateClockwise();
            }
        }
    }
}

