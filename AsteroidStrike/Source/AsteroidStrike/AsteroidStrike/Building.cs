using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsteroidStrike
{
    [Serializable()]
    class Building
    {
        public int X;
        public int Height;
        public int midPoint;
        public bool destroyed = false;

        public Building(int x, Random random)
        {
            Height = random.Next(2,22)*2;
            X = x;
        }
        public Building()
        {
        }
        public void Destroy()
        {
            Height = 1;
            destroyed = true;
        }

    }
    [Serializable()]
    class Base : Building
    {
        //the base has two collision zones, midpoint and X

        public Base(int x)
        {
            X = x;
            midPoint = x + 16;
            
        }
    }
}
