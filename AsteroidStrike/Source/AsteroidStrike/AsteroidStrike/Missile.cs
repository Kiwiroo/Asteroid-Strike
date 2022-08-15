using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsteroidStrike
{
    [Serializable()]
    class Missile: Asteroid
    {   
        public Missile( int startX, int startY, int endX, int endY, double Speed)
        {
            SetMotion(startX, startY, endX, endY, Speed);
        }

    }
}
