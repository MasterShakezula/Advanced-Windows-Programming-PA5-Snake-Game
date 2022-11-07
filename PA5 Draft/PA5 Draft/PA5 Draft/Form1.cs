using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;



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
        private bool paused = false;
        private SoundPlayer s, t, u; // to play sound effects
        private Random r; // to help choose random opacity
        //private Image applePic = '@apple.png'; //ask about this 
        public MainForm()
        {
            r = new Random();
            OptionsMenu Options = new OptionsMenu();
            s = new SoundPlayer(PA5_Draft.Properties.Resources.VOXScrm_Wilhelm_scream__ID_0477__BSB);
            t = new SoundPlayer(PA5_Draft.Properties.Resources.Mario_Powerup);
            u = new SoundPlayer(PA5_Draft.Properties.Resources.crash_6711);

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
            u.Play();
            GameOver = true;
            mainTimer.Stop();
            Field.Refresh();
            
            
        }
        private void Game_HitSnakeAndLose()
        {
            // use game over here
            s.Play();
            GameOver = true;
            mainTimer.Stop();
            Field.Refresh();
            
        }

        private void Game_EatAndGrow()
        {
            t.Play();
            ++applesEaten; //you can only eat 1 apple at a time of course
            if (applesEaten % 10 == 0 && Step != 10)
            {
                Step++;
                toolStripProgressBar1.Value++; 
                // progress on the player's speed stat as they get apples
                // 100 apples = max speed achieved
            }
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
            Brush AppleBrush = new SolidBrush(Color.FromArgb(r.Next(100, 256), 250, 50, 50));
            // random object to help choose opacity 
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

        private void Field_Click(object sender, EventArgs e)
        {
            // pause/unpause on click
            if (!paused)
            {
                paused = true;
                mainTimer.Stop();
            }
            else
            {
                paused = false;
                mainTimer.Start();
            }
        }
    }
}
