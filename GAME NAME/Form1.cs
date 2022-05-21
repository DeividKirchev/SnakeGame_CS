using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;

namespace Clicker_Hero
{
    public partial class Form1 : Form
    {
        Snake snake = new Snake(11,11,21,21);
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Paint += new PaintEventHandler(Form1_Paint);
        }
        
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 50; i < 700; i = i + 30)
            {
                for (int j = 100; j < 750; j = j + 30)
                {
                    Rectangle rectangle1 = new Rectangle(i, j, 30, 30);

                    e.Graphics.DrawRectangle(Pens.Black, rectangle1);
                }
            }
            Pen boldblack = new Pen(Color.Black, 10);
            Rectangle rectangle2 = new Rectangle(45, 95,670,670);
            e.Graphics.DrawRectangle(boldblack, rectangle2);
            /*Rectangle rectangle3 = new Rectangle(snake.headx * 30 + 50, snake.heady * 30 + 100, 30, 30);
            e.Graphics.FillRectangle(Brushes.Green, rectangle3);*/
        } // paint the grid
        private void paintSnake(int x,int y) // paints the head when moving
        {
            Graphics dc = this.CreateGraphics();
            SolidBrush greenbrush = new SolidBrush(Color.Green);
            dc.FillRectangle(greenbrush, x * 30 + 50 + 1, y * 30 + 100 + 1, 29, 29);
        }
        private void paintApple(int i, int j) // paints the head when moving
        {
            Graphics dc = this.CreateGraphics();
            SolidBrush redbrush = new SolidBrush(Color.Red);
            dc.FillRectangle(redbrush, i * 30 + 50 + 1, j * 30 + 100 + 1, 29, 29);
        }
        int score = 0;
        private void paintSnakeBody() // removes the back of the snake upon moving
        {
            Graphics dc = this.CreateGraphics();
            Color color = new Color();
            color = Color.FromArgb(255,153, 182, 2);
            SolidBrush bground = new SolidBrush(color);
            dc.FillRectangle(bground, snake.q.Peek().x * 30 + 50 + 1, snake.q.Peek().y * 30 + 100 + 1, 29, 29);
        }
        private void paintSquare(int i,int j)
        {
            Graphics dc = this.CreateGraphics();
            Color color = new Color();
            color = Color.FromArgb(255, 153, 182, 2);
            SolidBrush brush = new SolidBrush(color);
            dc.FillRectangle(brush, i * 30 + 50 + 1, j * 30 + 100 + 1, 29, 29);
        }
        private void paintEyes()
        {
            Graphics dc = this.CreateGraphics();
            Color color = new Color();
            color = Color.Black;
            SolidBrush brush = new SolidBrush(color);
            if (snake.Facing() == "down")
            {
                dc.FillRectangle(brush, snake.headx * 30 + 50 + 1 + 5, snake.heady * 30 + 100 + 1 + 20, 3, 3);
                dc.FillRectangle(brush, snake.headx * 30 + 50 + 1 + 22, snake.heady * 30 + 100 + 1 + 20, 3, 3);
            }
            if (snake.Facing() == "up")
            {
                dc.FillRectangle(brush, snake.headx * 30 + 50 + 1 + 5, snake.heady * 30 + 100 + 1 + 5, 3, 3);
                dc.FillRectangle(brush, snake.headx * 30 + 50 + 1 + 22, snake.heady * 30 + 100 + 1 + 5, 3, 3);
            }
            if (snake.Facing() == "left")
            {
                dc.FillRectangle(brush, snake.headx * 30 + 50 + 1 + 5, snake.heady * 30 + 100 + 1 + 5, 3, 3);
                dc.FillRectangle(brush, snake.headx * 30 + 50 + 1 + 5, snake.heady * 30 + 100 + 1 + 22, 3, 3);
            }
            if (snake.Facing() == "right")
            {
                dc.FillRectangle(brush, snake.headx * 30 + 50 + 1 + 22, snake.heady * 30 + 100 + 1 + 5, 3, 3);
                dc.FillRectangle(brush, snake.headx * 30 + 50 + 1 + 22, snake.heady * 30 + 100 + 1 + 22, 3, 3);
            }
        }
        private void AppleFound()
        {
            snake.lenght++;
            gridApples[snake.headx, snake.heady] = false;
            score++;
            Score.Text = score.ToString();
            SetApple();
        }
        private void MoveSnake() // Moves the snake (duh)
        {
            paintSnake(snake.headx, snake.heady);
            snake.Move();
            try
            {
                if (grid[snake.headx, snake.heady] && !gridApples[snake.headx, snake.heady])
                {
                    snake.SetDead(true);
                    return;
                }
            }
            catch { }
            try
            {
                if (gridApples[snake.headx, snake.heady])
                {
                    AppleFound();
                }
            }
            catch{}
            try
            {
                grid[snake.headx, snake.heady] = true;
            }
            catch {}
            if (snake.CheckDead())
            {
                timer1.Stop();
                return;
            }
            if(snake.q.Count>snake.lenght)
            {

                try
                {
                    grid[snake.q.Peek().x , snake.q.Peek().y] = false;
                    //Score.Text = grid[snake.q.Peek().x, snake.q.Peek().y].ToString();
                }
                catch {}
                paintSnakeBody();
                snake.q.Dequeue();
            }
            paintSnake(snake.headx,snake.heady);
            paintEyes();
        } // moves the snake
        bool flag = true;
        private void timer1_Tick(object sender, EventArgs e)
        {
            MoveSnake();
            flag = true;
        } // timer for moving

        private void Form1_KeyDown(object sender, KeyEventArgs key)
        {
            if (flag)
            {
                if (key.KeyCode == Keys.W)
                    snake.TurnUp();
                if (key.KeyCode == Keys.S)
                    snake.TurnDown();
                if (key.KeyCode == Keys.A)
                    snake.TurnLeft();
                if (key.KeyCode == Keys.D)
                    snake.TurnRight();

                flag = false;
            }
        }

        private void Start_Click(object sender, EventArgs e)
        {
            score = 0;
            for (int i = 0;i <= 21; i++)
            {
                for (int j = 0; j <= 21; j++)
                {
                    grid[i, j] = false;
                    gridApples[i, j] = false;
                    paintSquare(i, j);
                }
            }
            Score.Text = score.ToString();
            snake.headx = 11;
            snake.heady = 11;
            snake.TurnLeft();
            snake.TurnDown();
            snake.SetDead(false);
            
            while (snake.q.Count > 0) snake.q.Dequeue();
            SetApple();
            timer1.Start();
            timer2.Start();
            snake.lenght = 2;
            
        } // START OR RESTART

        bool[,] grid = new bool[22,22];
        bool[,] gridApples = new bool[22, 22];
        private void SetApple() //set random apple position
        {
            Random rand = new Random();
            int i=0, j=0;
            while(true)
            {
                i = rand.Next(21);
                j = rand.Next(21);
                if (!grid[i, j]) break;
            }
            grid[i, j] = true;
            gridApples[i, j] = true;
            paintApple(i, j);
        }
        
        /*static Color GetPixel(System.Drawing.Point position) // ???? nz dali bachka
        {
            using (var bitmap = new Bitmap(1, 1))
            {
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    graphics.CopyFromScreen(position, new System.Drawing.Point(0, 0), new Size(1, 1));
                }
                return bitmap.GetPixel(0, 0);
            }
        }*/

        private void Score_Click(object sender, EventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            paintSquare(11, 11);
            timer2.Stop();
        }
    }
}
