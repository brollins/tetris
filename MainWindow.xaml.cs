using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Tetris;

namespace WpfApplication2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isColliding;
        private ITetromino tetromino;


        public MainWindow()
        {
            InitializeComponent();
            tetromino = new ITetromino(canvas);
            tetromino.Draw();
            isColliding = false;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(800);
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
        }
    }
}

