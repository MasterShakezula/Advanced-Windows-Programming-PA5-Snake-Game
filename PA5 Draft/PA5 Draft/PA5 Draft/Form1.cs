﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PA5_Draft
{
    public partial class MainForm : Form
    {
        private int Step = 1; //speed of the snake
        private readonly SnakeGame Game;
        private int OPACITY = 255;
        private Boolean GameOver = false;
        private int NumberOfApples; //apples seen on screen at all times
        private int applesEaten = 0;
        //private Image applePic = '@apple.png'; //ask about this 
        public MainForm()
        {
            OptionsMenu Options = new OptionsMenu();

            Options.ShowDialog();
            //this.Enabled = true;
            NumberOfApples = Options.getApples(); //Part 1 is finished with this line of code.
            InitializeComponent();
            Game = new SnakeGame(new System.Drawing.Point((Field.Width - 20) / 2, Field.Height / 2), 40, NumberOfApples, Field.Size);
            Field.Image = new Bitmap(Field.Width, Field.Height);
            Game.EatAndGrow += Game_EatAndGrow;
            Game.HitWallAndLose += Game_HitWallAndLose;
            Game.HitSnakeAndLose += Game_HitSnakeAndLose;
        }

        private void Game_HitWallAndLose()
        { // use game over here
            GameOver = true;
            mainTimer.Stop();
            Field.Refresh();
        }
        private void Game_HitSnakeAndLose()
        {
            // use game over here
            GameOver = true;
            mainTimer.Stop();
            Field.Refresh();
        }

        private void Game_EatAndGrow()
        {
            ++applesEaten; //you can only eat 1 apple at a time of course
            if (applesEaten % 10 == 0 && Step != 10) Step++;
            //increase snakes speed by 1 for every 10 apples, maxing out at 10

            
        }

        private void MainTimer_Tick(object sender, EventArgs e)
        {
            Game.Move(Step);
            Field.Invalidate();
        }

        private void Field_Paint(object sender, PaintEventArgs e)
        {
            Pen PenForObstacles = new Pen(Color.FromArgb(40,40,40),2);
            Pen PenForSnake = new Pen(Color.FromArgb(100, 100, 100), 2);
            Brush BackGroundBrush = new SolidBrush(Color.FromArgb(150, 250, 150));
            Brush AppleBrush = new SolidBrush(Color.FromArgb(250, 50, 50));
            using (Graphics g = Graphics.FromImage(Field.Image))
            {
                g.FillRectangle(BackGroundBrush,new Rectangle(0,0,Field.Width,Field.Height));
                foreach (Point Apple in Game.Apples)
                    g.FillEllipse(AppleBrush, new Rectangle(Apple.X - SnakeGame.AppleSize / 2, Apple.Y - SnakeGame.AppleSize / 2,
                        SnakeGame.AppleSize, SnakeGame.AppleSize));
                foreach (LineSeg Obstacle in Game.Obstacles)
                    g.DrawLine(PenForObstacles, new System.Drawing.Point(Obstacle.Start.X, Obstacle.Start.Y)
                        , new System.Drawing.Point(Obstacle.End.X, Obstacle.End.Y));
                foreach (LineSeg Body in Game.SnakeBody)
                    g.DrawLine(PenForSnake, new System.Drawing.Point((int)Body.Start.X, (int)Body.Start.Y)
                        , new System.Drawing.Point((int)Body.End.X, (int)Body.End.Y));
                if (GameOver)
                {
                    
                    g.DrawString("Sorry Loser. You lost and only ate " + applesEaten + " apples.", DefaultFont, AppleBrush, RectangleToScreen(new Rectangle(0, 0, Field.Width, Field.Height))); 
                }
            }
            
        }



        private void Snakes_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    Game.Move(Step, Direction.UP);
                    break;
                case Keys.Down:
                    Game.Move(Step, Direction.DOWN);
                    break;
                case Keys.Left:
                    Game.Move(Step, Direction.LEFT);
                    break;
                case Keys.Right:
                    Game.Move(Step, Direction.RIGHT);
                    break;
            }
        }
    }
}