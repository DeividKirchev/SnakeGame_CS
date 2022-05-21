using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Clicker_Hero
{
    class Point
    {
        public int x;
        public int y;
        Point()
        {
            x = 0;
            y = 0;
        }
        public Point(int x1,int y1)
        {
            x = x1;
            y = y1;
        }
    }
    class Snake
    {
        int edgex;
        int edgey;
        public int headx;
        public int heady;
        public int lenght;
        string facing;
        public Queue<Point> q = new Queue<Point>();
        bool Dead = false;
        public bool CheckDead()
        {
            if (Dead == true) return true;
            else return false;
        }
        public void SetDead(bool x)
        {
            Dead = x;
        }
        Snake ()
        {
            headx = 0;
            heady = 0;
            edgex = 10;
            edgey = 10;
            lenght = 2;
            facing = "down";
        }
        public Snake(int x,int y,int ex,int ey)
        {
            
            headx = x;
            heady = y;
            edgex = ex;
            edgey = ey;
            lenght = 2;
            facing = "down";
        }
        public void Move()
        {
            if (facing == "down") heady++;
            if (facing == "up") heady--;
            if (facing == "left") headx--;
            if (facing == "right") headx++;
            if (headx < 0 || headx > edgex) Dead = true;
            if (heady < 0 || heady > edgey) Dead = true;
            q.Enqueue(new Point(headx, heady));
        }
        public void TurnLeft()
        {
            if (facing == "down" || facing == "up") facing = "left";
        }
        public void TurnRight()
        {
            if (facing == "down" || facing == "up") facing = "right";
        }
        public void TurnUp()
        {
            if (facing == "left" || facing == "right") facing = "up";
        }
        public void TurnDown()
        {
            if (facing == "left" || facing == "right") facing = "down";
        }
        public string Facing()
        {
            return facing;
        }


    }
}
