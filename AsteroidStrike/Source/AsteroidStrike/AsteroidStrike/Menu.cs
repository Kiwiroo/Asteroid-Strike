using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.IO;
using System.Text.RegularExpressions;

namespace AsteroidStrike
{
    class Menu
    {
        int selection;
        //pause menu items
        List<string> pauseMenuItems = new List<string>(){
            {"Resume"},
            {"New"},
            {"Save"},
            {"Load"},
            {"How To Play"},
            {"Quit"},
        };
        //start menu items
        List<string> startMenuItems = new List<string>(){
            {"Start"},
            {"Load"},
            {"How To Play"},
            {"Quit"},
        };
        List<string> gameOverMenuItems = new List<string>(){
            {"New"},
            {"Load"},
            {"How To Play"},
            {"Quit"},
        };
        int PauseMax;//number of items in pause menu, calulated in constructor
        int StartMax;//number of items in start menu, calculated in constructor
        string inputString = "";// string the input screen uses
        Controller refController;// this is a refrence to the controller
        SpriteFont myFont;// font the menu will be written in
        Vector2 MenuPosition = new Vector2(300, 200);// this is where the top left corner of the menu is
        Vector2 MenuSpacing = new Vector2(0, 30);// how much space between each menu item
        Vector2 TitleOffset = new Vector2(-20, -30);// this defines how much offset static text will have, as to not confuse players
        List<string> gameFiles = new List<string>();// this is a holder for the game file names in the loading screen

        public Menu(Controller Controller, SpriteFont myFont)// constructor
        {
            PauseMax = (pauseMenuItems.Count - 1);
            StartMax = (startMenuItems.Count - 1);//one less than the count, since selection includes 0
            refController = Controller;
            this.myFont = myFont;
            selection = 0;
            
        }

        public string UpdateStart()
        {// Start menu Update function
            string gameState = "Start";// default gamestate is it's self
            
            UpdateSelection(StartMax);// update the selector

            if (refController.enterPressedFrame)
            {
                switch (selection)// change the gamestate based on selection
                {
                    case 0: gameState = "Playing";
                        break;
                    case 1: gameState = "Load";
                        break;
                    case 2: gameState = "HowToPlay";
                        break;
                    case 3: gameState = "Quit";
                        break;
                }
                Reset();//resets the menu
            }
            return gameState;

        }
        #region Save
        public string UpdateSave(ref GameWorld myGame)
        {
            string gameState = "Save";
            inputString = refController.KeyboardInputString(inputString);

            if (refController.enterPressedFrame)
            {
                if (inputString != "") //if they typed a name
                {
                    GameFilePackager.Save<GameWorld>(myGame, inputString, "ATS");//save game under name
                    Reset();// empties string

                }
                gameState = "Paused";//either way return to the pause screen
            }//if key is pressed


            return gameState;
        }

        #endregion
        public string UpdateHowToPlay(string previousGameState)
        {
            string gameState = "HowToPlay";
            if (refController.enterPressedFrame)// escape from howtoplay
            {
                gameState = previousGameState;
            }

            return gameState;
        }
        #region Load
        public string UpdateLoad( ref GameWorld myGame, string prevMenu)
        {
            bool gameLoaded = false;

            if (gameFiles.Count == 0)//if I haven't loaded any files
            {
                selection = 0;
                gameFiles = GameFilePackager.GetFileNames("ATS");
            }// end if I haven't loaded files
            else//I have loaded files
            {
                UpdateSelection(gameFiles.Count - 1);
            }

            if (refController.enterPressedFrame )// if enter key is pressed
            {
                if (gameFiles.Count > 0)
                {
                    myGame = GameFilePackager.Load<GameWorld>(gameFiles[selection], "ATS");//load the selected file
                    gameLoaded = true;
                }
                else
                {
                    Reset();
                    return "Start";
                }
  
            }//if enter is pressed

            if (gameLoaded)// if game loading was successful
            {
                Reset();// sets selection to 0
                return "Paused";
            }
            return "Load";
        }
#endregion

        public string UpdateGameOver()
        {
            string gameState = "GameOver";
            UpdateSelection(gameOverMenuItems.Count - 1);

            if (refController.enterPressedFrame)
            {
                switch (selection)
                {
                    case 0:
                        gameState = "New";
                        break;
                    case 1 :
                        gameState = "Load";
                        break;
                    case 2:
                        gameState = "HowToPlay";
                        break;
                    case 3:
                        gameState = "Quit";
                        break;
                }//end switch selection
            }//end if enter pressed

            return gameState;
        }//updategameOver

        public string UpdatePause()
        {
            string gameState = "Paused";

            UpdateSelection(PauseMax);

            if (refController.enterPressedFrame)
            {
                switch (selection)
                {
                    case 0: gameState = "Playing";
                        break;
                    case 1: gameState = "New";
                        break;
                    case 2: gameState = "Save";
                        break;
                    case 3: gameState = "Load";
                        break;
                    case 4: gameState = "HowToPlay";
                        break;
                    case 5: gameState = "Quit";
                        break;
                }
                Reset();// sets selection to 0
            }
            return gameState;

        }

        private void UpdateSelection(int Max)//this function detects up and down arrows and changes the selection
        {
            if (refController.downPressedFrame)
            {
                selection++;
                if (selection > Max)
                {
                    selection = 0;
                }
            }

            if (refController.upPressedFrame)
            {
                selection--;
                if (selection < 0)
                {
                    selection = Max;
                }
            }

        }

        public void Reset()//  resets the menu, might get bigger as
        {
            selection = 0;//resets selection
            inputString = "";//flush the input string
            gameFiles = new List<string>();// empty game files so we can reload next time
        }


        #region Drawing
        public void DrawPause(SpriteBatch spriteBatch)
        {
            for(int index = 0 ; index <= PauseMax ; index++)
            {
                spriteBatch.DrawString(myFont, pauseMenuItems[index], MenuPosition+(MenuSpacing * index) , Color.White);
            }
            spriteBatch.DrawString(myFont, pauseMenuItems[selection], MenuPosition+(MenuSpacing * selection), Color.Red);
        }



        public void DrawStart(SpriteBatch spriteBatch)
        {

            spriteBatch.DrawString(myFont, "ASTEROID STRIKE", MenuPosition + TitleOffset, Color.White);
            for(int index = 0 ; index <= StartMax ; index++)
            {
                spriteBatch.DrawString(myFont, startMenuItems[index], MenuPosition+(MenuSpacing * index) , Color.White);
            }
            spriteBatch.DrawString(myFont, startMenuItems[selection], MenuPosition+(MenuSpacing * selection), Color.Red);
        }

        public void DrawHowToPlay(SpriteBatch spriteBatch, ref GameWorld  myGame)
        {
            spriteBatch.DrawString(myFont, "Welcome to Asteroid Strike! \nMissiles cost $ " + myGame.missileCost + " each. \nEvery day you get more money. \nThe More Buildings you have \nthe more you will make each day", MenuPosition + TitleOffset, Color.White);
        }
        
        
        public void DrawSave(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(myFont, "Please input a file name, press enter when done.", MenuPosition - new Vector2(100,0), Color.White);
            spriteBatch.DrawString(myFont, inputString + "_", MenuPosition + MenuSpacing, Color.Red);
        }

        public void DrawLoad(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(myFont, "GameFiles:", MenuPosition + TitleOffset  , Color.White);
            try
            {
                for (int i = 0; i < gameFiles.Count; i++)
                {
                    Color textColor = (selection == i) ? Color.Red : Color.White; 
                    spriteBatch.DrawString(myFont, gameFiles[i], MenuPosition + (MenuSpacing * (i+1)), textColor );
                }
            }
            catch
            {
                spriteBatch.DrawString(myFont, "No Files to Load", MenuPosition + (MenuSpacing * 1), Color.Red);
            }
        }
        public void DrawGameOver(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(myFont, "GAME OVER", MenuPosition + TitleOffset , Color.White);
            for (int i = 0; i < gameOverMenuItems.Count; i++)
            {
                Color textColor = (selection == i) ? Color.Red : Color.White;
                spriteBatch.DrawString(myFont, gameOverMenuItems[i], MenuPosition + (MenuSpacing * (i + 1)), textColor);
            }
        }

        #endregion Drawing
    }
}
