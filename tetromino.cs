﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Tetris
{
    public class Tetromino
    {
        private Collection<TetrisBlock> blocks;
        private Canvas canvas;

        public Tetromino()
        {

        }

        public Tetromino(Canvas canvas)
        {
            Blocks = new Collection<TetrisBlock>();
            this.canvas = canvas;
        }

        public Collection<TetrisBlock> Blocks
        {
            get
            {
                return blocks;
            }

            set
            {
                blocks = value;
            }
        }
        public void Drop()
        {            
            if (!isAtBottom())
            {
                foreach (var tetrisBlock in Blocks)
                {
                    tetrisBlock.Drop();
                }
            }
        }

        public void MoveLeft()
        {
            if (!isAtBottom())
            {
                foreach (var tetrisBlock in Blocks)
                {
                    tetrisBlock.MoveLeft();
                }
            }
        }

        public void MoveRight()
        {
            if (!isAtBottom())
            {
                foreach (var tetrisBlock in Blocks)
                {
                    tetrisBlock.MoveRight();
                }
            }
        }

        public void MoveDown()
        {
            if (!isAtBottom())
            {
                foreach (var tetrisBlock in Blocks)
                {
                    tetrisBlock.MoveDown();
                }
            }
        }
        public void Draw()
        {
            foreach (var tetrisBlock in Blocks)
            {
                tetrisBlock.Draw();
            }
        }
        private bool isAtBottom ()
        {
            bool atBottom = false;
            foreach (var tetrisblock in Blocks)
            {
                if (tetrisblock.Y >= canvas.ActualHeight - 50)
                {
                    atBottom = true;
                }
            }
            return atBottom;
        }
    }
}