using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsteroidStrike
{
    [Serializable()]
    class Asteroid
    {
        int DestinationX;//random Endpoint at bottom of screen
        int StartX;// Random Start point at top of screen
        int Speed;//how many frames until it strikes
        int TimeAlive;//how many frames the astroid has lived
        int TimeRemaining;
        public double X;
        public double Y;
        int GroundY;// when the asteroid explodes/ otherwise known as the ground.
        int Buffer = 15;
        public bool Exploded= false;
        int GameHeight;
        int GameWidth;
        Random myRandom;
        public double framesUntilStrike;
        double vectorX;
        double vectorY;


        public Asteroid()
        {
            //donothing
        }

        public Asteroid(int GameHeight, int GameWidth, Random myRandom)
        {

            GroundY = GameHeight;// find the ground

            this.GameHeight = GameHeight;
            this.GameWidth = GameWidth;
            this.myRandom = myRandom;

            SetMotion(
                    (int)myRandom.Next(Buffer, GameWidth - Buffer),
                    0,
                    (int)myRandom.Next(Buffer, GameWidth - Buffer), GroundY, (double)myRandom.Next(15, 20) * .1);

        }

        /*
        public void StartDrop(  )
        {
            Exploded = false;

            StartX = (int)myRandom.Next(Buffer, GameWidth - Buffer);//random start point at top of screen
            DestinationX = (int)myRandom.Next(Buffer, GameWidth - Buffer);//random destination point at bottom of screen
            //this is to ensure that the asteroid always starts and ends in the play area

            double Speed = myRandom.Next(15,20);//pick a random int
            Speed *= .1;//make it smaller
            //speed is how many pixels it will move each frame

            //find out how far there is to travel
            double distanceX = (double)DestinationX - StartX;
            double distanceY = GroundY;

            //figure out how long the path is using a^2 +b^2 = c^2 algorithm
            double distanceToDestination = Math.Sqrt((distanceY * distanceY) + (distanceX * distanceX));

            //number of frames before the asteroid will Strike
            framesUntilStrike = distanceToDestination / Speed;

            Console.WriteLine("framesUntilStrike: " + framesUntilStrike);
            vectorX = distanceX / framesUntilStrike;
            vectorY = distanceY / framesUntilStrike;

            TimeAlive = 0;

            Y = 0;
            X = StartX;
        }*/


        public void SetMotion( int startX, int startY, int endX, int endY, double Speed)
        {
            Exploded = false;

            //speed is how many pixels it will move each frame devided by 10

            //find out how far there is to travel
            double distanceX = endX - startX;
            double distanceY = endY - startY;

            //figure out how long the path is using a^2 +b^2 = c^2 algorithm
            double distanceToDestination = Math.Sqrt((distanceY * distanceY) + (distanceX * distanceX));

            //number of frames before the asteroid will Strike
            framesUntilStrike = distanceToDestination / Speed;

            vectorX = distanceX / framesUntilStrike;
            vectorY = distanceY / framesUntilStrike;

            Y = startY;
            X = startX;
        }

        public void Update()
        {
           framesUntilStrike -= 1;
            X += vectorX;
            Y += vectorY;

            

            if (framesUntilStrike < 5.0)
            {
                Exploded = true;
            }
            else
            {
                Exploded = false;
            }

            if (framesUntilStrike < 0)
            {
                SetMotion(
                    (int)myRandom.Next(Buffer, GameWidth - Buffer),
                    0,
                    (int)myRandom.Next(Buffer, GameWidth - Buffer),GroundY ,  (double)myRandom.Next(15,20) * .1 );
            }
            
        }

    }
}
