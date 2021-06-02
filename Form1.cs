//Made by Ewan Peterson ICS3U

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
using System.Threading;

namespace Air_Hockey
{
    public partial class Form1 : Form
    {
        //setup variables
        Rectangle player1 = new Rectangle(181, 430, 39, 40);
        Rectangle player2 = new Rectangle(181, 31, 39, 40);

        Rectangle puck = new Rectangle(192, 242, 15, 15);

        Rectangle p1Goal = new Rectangle(150, 500, 100, 1);
        Rectangle p2Goal = new Rectangle(150, 1, 100, 1);

        int player1Score = 0;
        int player2Score = 0;

        int puckYspeed = 0;
        int puckXspeed = 0;
        int playerSpeed = 8;

        int puckSlowCount = 0;

        bool wDown = false;
        bool sDown = false;
        bool aDown = false;
        bool dDown = false;
        bool upArrowDown = false;
        bool downArrowDown = false;
        bool leftDown = false;
        bool rightDown = false;

        SoundPlayer hit = new SoundPlayer(Properties.Resources.hit);
        SoundPlayer horn = new SoundPlayer(Properties.Resources.horn);
        SoundPlayer win = new SoundPlayer(Properties.Resources.winner);


        SolidBrush playerBrush = new SolidBrush(Color.Blue);
        SolidBrush puckBrush = new SolidBrush(Color.Black);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = true;
                    break;
                case Keys.S:
                    sDown = true;
                    break;
                case Keys.A:
                    aDown = true;
                    break;
                case Keys.D:
                    dDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
                case Keys.Left:
                    leftDown = true;
                    break;
                case Keys.Right:
                    rightDown = true;
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = false;
                    break;
                case Keys.S:
                    sDown = false;
                    break;
                case Keys.A:
                    aDown = false;
                    break;
                case Keys.D:
                    dDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
                case Keys.Left:
                    leftDown = false;
                    break;
                case Keys.Right:
                    rightDown = false;
                    break;
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //store player and puck location
            int puckX = puck.X;
            int puckY = puck.Y;
          

            int player1X = player1.X;
            int player1Y = player1.Y;
            int player2X = player2.X;
            int player2Y = player2.Y;


            //move puck
            puck.X += puckXspeed;
            puck.Y += puckYspeed;
            puckSlowCount++;

            if (puckSlowCount % 5 == 0)
            {
                if (puckXspeed < 0)
                {
                    puckXspeed++;
                }
                if (puckXspeed > 0)
                {
                    puckXspeed--;
                }

                if (puckYspeed < 0)
                {
                    puckYspeed++;
                }
                if (puckYspeed > 0)
                {
                    puckYspeed--;
                }
            }

            //move player
            if (wDown == true && player1.Y > 0)
            {
                player1.Y -= playerSpeed;
            }

            if (sDown == true && player1.Y < this.Height - player1.Height)
            {
                player1.Y += playerSpeed;
            }

            if (aDown == true && player1.X > 0)
            {
                player1.X -= playerSpeed;
            }

            if (dDown == true && player1.X < this.Width - player1.Width)
            {
                player1.X += playerSpeed;
            }

            if (upArrowDown == true && player2.Y > 0)
            {
                player2.Y -= playerSpeed;
            }

            if (downArrowDown == true && player2.Y < this.Height - player2.Height)
            {
                player2.Y += playerSpeed;
            }

            if (leftDown == true && player2.X > 0)
            {
                player2.X -= playerSpeed;
            }

            if (rightDown == true && player2.X < this.Width - player2.Width)
            {
                player2.X += playerSpeed;
            }



            //check for wall collision
            if (puck.X < 0)
            {
                puckXspeed *= -1;
                puckXspeed += 1;
                puck.X = 1;
            }

            if (puck.X > this.Width - puck.Width)
            {
                puckXspeed += -1;
                puckXspeed *= -1;
                puck.X = 389;
            }

            if (puck.Y < 0)
            {
                puckYspeed += 1;
                puckYspeed *= -1;
                puck.Y = 1;
            }

            if (puck.Y > this.Height - puck.Height)
            {
                puckYspeed += -1;
                puckYspeed *= -1;
                puck.Y = 489;
            }

            //check for player collision
            Rectangle p1Top = new Rectangle(player1X, player1Y, 40, 4);
            Rectangle p1Left = new Rectangle(player1X, player1Y, 4, 40);
            Rectangle p1Right = new Rectangle(player1X + player1.Width, player1Y, 4, 40);
            Rectangle p1Bottom = new Rectangle(player1X, player1Y + player1.Height, 40, 4);

            Rectangle p2Top = new Rectangle(player2X, player2Y, 40, 4);
            Rectangle p2Left = new Rectangle(player2X, player2Y, 4, 40);
            Rectangle p2Right = new Rectangle(player2X + player2.Width, player2Y, 4, 40);
            Rectangle p2Bottom = new Rectangle(player2X, player2Y + player2.Height, 40, 4);

            if (puck.IntersectsWith(p1Top))
            {
                puckYspeed = -10;
                hit.Play();
            }

            if (puck.IntersectsWith(p1Left))
            {
                puckXspeed = -10;
                hit.Play();
            }

            if (puck.IntersectsWith(p1Right))
            {
                puckXspeed = 10;
                hit.Play();
            }

            if (puck.IntersectsWith(p1Bottom))
            {
                puckYspeed = 10;
                hit.Play();
            }

            if (puck.IntersectsWith(p2Top))
            {
                puckYspeed = -10;
                hit.Play();
            }

            if (puck.IntersectsWith(p2Left))
            {
                puckXspeed = -10;
                hit.Play();
            }

            if (puck.IntersectsWith(p2Right))
            {
                puckXspeed = 10;
                hit.Play();
            }

            if (puck.IntersectsWith(p2Bottom))
            {
                puckYspeed = 10;
                hit.Play();
            }

            //check for goal
            if (puck.IntersectsWith(p1Goal))
            {
                player2Score++;
                p2ScoreLabel.Text = $"{player2Score}";
                horn.Play();

                puckXspeed = 0;
                puckYspeed = 0;
                puck.X = 192;
                puck.Y = 242;

                player1.X = 181;
                player1.Y = 430;
                player2.X = 181;
                player2.Y = 30;
            }

            if (puck.IntersectsWith(p2Goal))
            {
                player1Score++;
                p1ScoreLabel.Text = $"{player1Score}";
                horn.Play();

                puckXspeed = 0;
                puckYspeed = 0;
                puck.X = 192;
                puck.Y = 242;

                player1.X = 181;
                player1.Y = 430;
                player2.X = 181;
                player2.Y = 30;
            }

            //end game
            if (player1Score >= 3)
            {
                winLabel.Text = "Player 1 Wins!";
                
                puckXspeed = 0;
                puckYspeed = 0;
                puck.X = 192;
                puck.Y = 242;

                player1.X = 181;
                player1.Y = 430;
                player2.X = 181;
                player2.Y = 30;

                Refresh();
                win.Play();
                Thread.Sleep(5000);
                Application.Exit();
            }

            if (player2Score >= 3)
            {
                winLabel.Text = "Player 2 Wins!";

                puckXspeed = 0;
                puckYspeed = 0;
                puck.X = 192;
                puck.Y = 242;

                player1.X = 181;
                player1.Y = 430;
                player2.X = 181;
                player2.Y = 30;


                Refresh();
                win.Play();
                Thread.Sleep(5000);
                Application.Exit();
            }


            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(playerBrush, player1);
            e.Graphics.FillRectangle(playerBrush, player2);
            e.Graphics.FillRectangle(puckBrush, puck);
        }
    }    
}
