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

namespace WpfApplication2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Block block;
        private bool isColliding;

        public MainWindow()
        {
            InitializeComponent();
            block = new Block(200, 1, Color.FromArgb(255, 245, 25, 235));            
            block.Draw(bobross);
            isColliding = false;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(800);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            checkCollision(block, bobross);
            if (!isColliding)
            {
                Debug.WriteLine(isColliding);
                bobross.Children.Clear();
                block.Y = block.Y + 40;
                block.Draw(bobross);
            }      
            if(isColliding)
            {
                bobross.Children.Clear();
            }
            
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Right)
            {
                checkCollision(block, bobross);
                if (!isColliding)
                {
                    bobross.Children.Clear();
                    block.X = block.X + 40;
                    block.Draw(bobross);
                }               
            }
            if (e.Key == Key.Left)
            {
                checkCollision(block, bobross);
                if (!isColliding)
                {
                    bobross.Children.Clear();
                    block.X = block.X - 40;
                    block.Draw(bobross);
                }
            }
            if (e.Key == Key.Down)
            {
                checkCollision(block, bobross);
                if (!isColliding)
                {
                    bobross.Children.Clear();
                    block.Y = block.Y + 40;
                    block.Draw(bobross);
                }
            }        
        }
        private void checkCollision(Block block, Canvas canvas)
        {
            if (block.Y >= canvas.ActualHeight - 50)
            {
                isColliding = true;
            }
            
        }
    }
}
