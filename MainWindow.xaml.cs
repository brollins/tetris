using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using System.Collections.ObjectModel;
using Tetris;
using System.Collections.Generic;
using System.Windows.Controls;

namespace WpfApplication2
{
    public partial class MainWindow : Window
    {
        private Tetromino tetromino;
        private Collection<Tetromino> tetrominosOnScreen;
        private Queue<Tetromino> tetrominoQueue;
        private Tetromino nextTetromino;
        private static Random random = new Random();


        public MainWindow()
        {
            InitializeComponent();
            tetrominosOnScreen = new Collection<Tetromino>();
            tetrominoQueue = new Queue<Tetromino>();

            //tetromino = RandomTetromino();
            //tetromino.Draw();
            //tetrominoQueue.Enqueue(RandomTetromino());
            DropNewTetromino();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(.5);
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
                            //tetromino.atBottom = true;
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
                        //increase score by 1000
                        //update score on textbox?
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

                //if (nextTetromino != null)
                //{
                //    foreach (var block in nextTetromino.Blocks)
                //    {
                //        Canvas.SetTop(block.BlockGraphic, -10000);
                //        Canvas.SetLeft(block.BlockGraphic, -10000);
                //        block.X = -10000;
                //        block.Y = -10000;
                //        board.Children.Remove(block.BlockGraphic);
                //    }
                //}

                tetromino.Draw();
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




        // new function to draw next tetromino
        // peek to grab next tetromino
        // loop through tetromino.blocks.blockGraphic  
        // canvas settop/setleft to position it 
        // draw tetromino

        // new function to drop next tetromino
        // handle enqueue and dequeue

        // peek and clone tetromino
        // dispose method to clear out tetromino
        // peek tetromino.clone


        //game end 
        // if Y = 0 in tetrominosOnScreen, end game

        //next block
        //can't currently place tetromino outside game board without shutting off drop
    }
}

