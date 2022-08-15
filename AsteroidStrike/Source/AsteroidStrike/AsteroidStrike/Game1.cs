#region Using Statements
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace AsteroidStrike
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameWorld myGame;
        Menu myMenu;
        string GameState = "Start";
        int GameWidth = 800;
        int GameHeight = 600;
        string PreGameState; // this gamestate never equals how to play , it's the gamestate that was active before how to play was
        

        Controller myController = new Controller();


        Dictionary<string, Texture2D> TextureDictionary = new Dictionary<string, Texture2D>();
        SpriteFont gameFont;


        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = GameHeight;
            graphics.PreferredBackBufferWidth = GameWidth;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            myGame = new GameWorld(GameHeight, GameWidth, gameFont);
            myMenu = new Menu(myController, gameFont);

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            TextureDictionary = LoadAllContent("Textures");
            gameFont  = Content.Load<SpriteFont>("Fonts/QuartsMS");
            
            
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        /// <summary>
        /// This function loads all of the files in a folder into a dictionary
        /// I invented this to save my wrists from typing so much, also whenever 
        /// I add a file to the game, it's instantly avaliable for use.
        /// Will crash if loading
        /// </summary>
        /// <param name="FolderName"> it takes the folder that you want to load</param>
        /// <returns> Dictionary<string, Texture2D>  the name of the texture, in the file, and the Texture Asscociated with it</returns>
        protected Dictionary<string, Texture2D> LoadAllContent(string FolderName)
        {
            
            List<string> fileNames = new List<string>();

            Dictionary<string, Texture2D> fileDictionary = new Dictionary<string, Texture2D>();
            string[] names = { "Asteroid", "Base", "MediumBuilding", "SkyGradiant", "SmallBuilding", "TallBuilding","Crosshairs"};

            fileNames.AddRange(names);

            foreach (string fileName in fileNames)
            {
                fileDictionary[fileName] = Content.Load<Texture2D>(FolderName + '/' + fileName);
            }

            return fileDictionary ;
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            /*if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();*/

            // TODO: Add your update logic here
            myController.Update();
            switch (GameState)
            {
                case "Playing":
                    GameState = myGame.Update(myController);
                    myMenu.Reset();
                    break;
                case "Paused":
                    PreGameState = GameState;
                    GameState = myMenu.UpdatePause();//update pause menu
                    break;
                case  "Start":
                    PreGameState = GameState;
                    GameState = myMenu.UpdateStart();//update Start Menu
                    break;
                case "Save":
                    GameState = myMenu.UpdateSave(ref myGame);//Update SaveGame menu
                    break;
                case "Load" :
                    GameState = myMenu.UpdateLoad(ref myGame, PreGameState);//update load game Menu
                    break;
                case "New":
                    myGame = new GameWorld(GameHeight, GameWidth, gameFont);//create a new game
                    GameState = "Paused";//Pause the game
                    break;
                case "HowToPlay":
                    GameState = myMenu.UpdateHowToPlay(PreGameState);
                    break;
                case "GameOver":
                    PreGameState = GameState;
                    GameState = myMenu.UpdateGameOver();//updates the gameover screen
                    break;
                case "Quit" :
                    Exit();
                    break;
            }
            

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkSlateBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            myGame.Draw(spriteBatch, TextureDictionary, myController );//Always draw the game in the background
            switch (GameState)
            {
                case "Paused":
                    myMenu.DrawPause(spriteBatch);
                    break;
                case "Start":
                    myMenu.DrawStart(spriteBatch);
                    break;
                case "Load":
                    myMenu.DrawLoad(spriteBatch);
                    break;
                case "HowToPlay":
                    myMenu.DrawHowToPlay(spriteBatch, ref myGame);
                    break;
                case "GameOver":
                    myMenu.DrawGameOver(spriteBatch);
                    break;
                case "Save":
                    myMenu.DrawSave(spriteBatch);
                    break;
            }
           
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
