using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace AsteroidStrike
{
    [Serializable()]
    class GameWorld
    {
        
        List<Asteroid> AsteroidList = new List<Asteroid>();//asteroids in play
        List<Missile> MissileList = new List<Missile>();//missiles in play
        List<Building> BuildingList = new List<Building>();//Buildings in play
        int GameHeight;
        int GameWidth;
        string GameState = "Playing";
        int ExplosionSize = 54;
        int buffer = 16;
        int buildingwidth = 16;
        public int money = 40000;// starts at 50000 , enough for 5 missiles
        SpriteFont myFont;
        long frameNumber;// frame number the game is at
        int daysAlive= 0;// how many days the player has survived
        int dayLength = 60;// how many frames in day
        int scorePerBuilding = 177;// how much money each building gives per day
        public int missileCost = 5000;// how much missiles cost
        int asteroidEntranceDay = 25;// the day that another asteroid comes
        int maxMissiles = 3; // maximum number of missiles in the sky at once

        public GameWorld(int GameHeight, int GameWidth, SpriteFont Font)
        {
            myFont = Font;
            Random myRandom = new Random();


            for (int i = buffer; i < GameWidth - buffer; i = i + buildingwidth)
            {
                BuildingList.Add(new Building(i, myRandom));
            }

            int centerBuilding = (int)BuildingList.Count / 2;//find center of array
            BuildingList[centerBuilding] = new Base(BuildingList[centerBuilding].X);//center building with old building's X
            BuildingList[centerBuilding+1].Destroy(); //out with the old, in with the new!

            
            AsteroidList.Add(new Asteroid(GameHeight, GameWidth,myRandom));

            this.GameWidth = GameWidth;
            this.GameHeight = GameHeight;

        }
        //update
        #region Update Functions
        public string Update(Controller myController)
        {
            GameState = "Playing";
            //update things
            Random myRandom = new Random();
            frameNumber++;//update the frame number
            if (frameNumber % dayLength == 0)//every every day
            {
                daysAlive++;
                int buildingsRemaining = 0;
                foreach (Building building in BuildingList)
                {
                    if (!building.destroyed)//if not destoyed
                    {
                        buildingsRemaining++;
                    }
                }
                money += buildingsRemaining * scorePerBuilding;
                if (daysAlive % asteroidEntranceDay == 0)
                {
                    AsteroidList.Add(new Asteroid(GameHeight,GameWidth, myRandom ));
                }
            }

            foreach (Asteroid asteroid in AsteroidList)
            {
                asteroid.Update();
                if (asteroid.Exploded)
                {
                    //check which buildings the explosion is colling with
                    foreach (Building building in BuildingList)
                    {
                        if (Math.Abs(asteroid.X - building.X) < ExplosionSize / 2)//checks the distance between them, and the explosion size
                        {
                            building.Destroy();// make it rubble
                        }
                        if (building is Base)// if this building is a base
                        {
                            if ((Math.Abs(asteroid.X - building.X) < ExplosionSize / 2) ||
                                (Math.Abs(asteroid.X - building.midPoint) < ExplosionSize / 2))// if the x or the midpoint hit the explosion
                            {
                                GameState = "GameOver";
                            }
                        }
                    }
 
                }
            }//update asteroids
            
            //Check if missiles are colliding asteroids
            foreach (Missile missile in MissileList)
              {
                    missile.Update();
                    if (missile.Exploded)
                    {
                        foreach (Asteroid asteroid in AsteroidList)
                        {
                            if (CheckDistance(missile.X, missile.Y, asteroid.X, asteroid.Y) < ExplosionSize/2)
                            {
                                AsteroidList.Add(new Asteroid(GameHeight, GameWidth, myRandom));
                                AsteroidList.Remove(asteroid);
                                break;
                            }
                        }
                    }
                    if (missile.framesUntilStrike < 1)
                    {
                        MissileList.Remove(missile);
                        break;
                    }
                }//update Missiles
            

            //fire missiles
            if (myController.MouseClickFrame)//if the mouse button is clicked
            {
                if (money >= missileCost)
                { 
                    if (MissileList.Count < maxMissiles)//there are not already too many missiles missiles
                    {
                        MissileList.Add(new Missile(GameWidth / 2, GameHeight, myController.mousePosition.X, myController.mousePosition.Y, 6));
                        money -= missileCost;// charge points for missiles
                    }
                }
            }


            if (myController.enterPressedFrame)//checks if space was pressed this frame
            {
                GameState = "Paused";//changes the game to paused
            }

            return GameState;//return if the game is playing paused or gameover
            
        }

        private double CheckDistance(double startX, double startY, double endX, double endY)
        {
            //find out how far there is to travel
            double distanceX = endX - startX;
            double distanceY = endY - startY;

            //figure out how long the path is using a^2 +b^2 = c^2 algorithm
            double distanceToDestination = Math.Sqrt((distanceY * distanceY) + (distanceX * distanceX));

            return distanceToDestination;
        }

        #endregion Update Functions
        #region Draw
        public void Draw(SpriteBatch spriteBatch, Dictionary<string, Texture2D> TextureDictionary, Controller myController)
        {
            spriteBatch.DrawString(myFont, "$ " + money, new Vector2(40, 10), Color.LimeGreen);
            spriteBatch.DrawString(myFont, "Days Alive: " + daysAlive, new Vector2(GameWidth - 200, 10), Color.Red);

            spriteBatch.Draw(TextureDictionary["SkyGradiant"], new Rectangle(0, 0, GameWidth, GameHeight), Color.White);

            foreach (Building building in BuildingList)
            {
                if (building is Base)
                {
                    spriteBatch.Draw(TextureDictionary["Base"], new Rectangle(building.X, 
                        GameHeight - TextureDictionary["Base"].Height, 
                        TextureDictionary["Base"].Width, 
                        TextureDictionary["Base"].Height), 
                        Color.White);
                }
                else if (building.Height < 20)
                {
                    //draw small building
                    spriteBatch.Draw(TextureDictionary["SmallBuilding"], new Rectangle(building.X, GameHeight - building.Height, 16, TextureDictionary["SmallBuilding"].Height), Color.White);
                }
                else if (building.Height < 42)
                {
                    //drawMediumBuilding
                    spriteBatch.Draw(TextureDictionary["MediumBuilding"], new Rectangle(building.X, GameHeight - building.Height, 16, TextureDictionary["MediumBuilding"].Height), Color.White);
                }
                else
                {
                    //draw largebuilding
                    spriteBatch.Draw(TextureDictionary["TallBuilding"], new Rectangle(building.X, GameHeight - TextureDictionary["TallBuilding"].Height, 16, TextureDictionary["TallBuilding"].Height), Color.White);//always drawn at texture Height
                }
            }


            foreach (Asteroid asteroid in AsteroidList)
            {
                spriteBatch.Draw(TextureDictionary["Asteroid"], new Rectangle((int)asteroid.X, (int)asteroid.Y, 8, 8), Color.White);
                if (asteroid.Exploded)
                {
                    //draw an explosion
                    spriteBatch.Draw(TextureDictionary["Asteroid"], new Rectangle((int)asteroid.X - ExplosionSize / 2,
                                                                    (int)asteroid.Y - ExplosionSize / 2,
                                                                    ExplosionSize,
                                                                    ExplosionSize),
                                                                    Color.Red);

                }
            }//draw asteroids

            foreach (Missile missile in MissileList)
            {
                spriteBatch.Draw(TextureDictionary["Asteroid"], new Rectangle((int)missile.X, (int)missile.Y, 8, 8), Color.White);
                if (missile.Exploded)
                {
                    //draw an explosion
                    spriteBatch.Draw(TextureDictionary["Asteroid"], new Rectangle((int)missile.X - ExplosionSize / 2,
                                                                    (int)missile.Y - ExplosionSize / 2,
                                                                    ExplosionSize,
                                                                    ExplosionSize),
                                                                    Color.Red);

                }
            }


            //drawCrosshairs
            spriteBatch.Draw(TextureDictionary["Crosshairs"], new Rectangle(myController.mousePosition.X-(TextureDictionary["Crosshairs"].Width/2),
                                                                            myController.mousePosition.Y -(TextureDictionary["Crosshairs"].Height/2),
                                                                            TextureDictionary["Crosshairs"].Width ,TextureDictionary["Crosshairs"].Height), Color.White);



        }
        #endregion
    }
}
