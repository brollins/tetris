using System;
using System.Collections.Generic;
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

namespace WpfApplication2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Block block;
        private Block block2;
        private Block block3;
        public MainWindow()
        {
            InitializeComponent();
            block = new Block(1, 1, Color.FromArgb(255, 255, 255, 255));
            block2 = new Block(2, 2, Color.FromArgb(255, 255, 255, 255));
            block3 = new Block(1, 2, Color.FromArgb(255, 255, 255, 255));
            block.Draw(bobross);
            block2.Draw(bobross);
            block3.Draw(bobross);
            

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            bobross.Children.Clear();
            block.Y = block.Y + 20;
            block.Draw(bobross);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {            
            if (e.Key == Key.Right)
            {
                bobross.Children.Clear();
                block.X = block.X + 20;
                block.Draw(bobross);
            }
            if (e.Key == Key.Left)
            {
                bobross.Children.Clear();

                block.X = block.X - 20;
                block.Draw(bobross);
            }
            if (e.Key == Key.Down)
            {
                bobross.Children.Clear();
                block.Y = block.Y + 20;
                block.Draw(bobross);
            }
        }
    }
}
