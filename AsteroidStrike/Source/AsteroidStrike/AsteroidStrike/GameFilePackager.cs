using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


namespace AsteroidStrike
{
    /// <summary>
    /// A static class for saving and loading function created to be resusuable in many different games and applications.
    /// All objects being saved, and all objects contained within the object being saved must have the 
    /// [Serializable()] attribute.
    /// Dictionary tables cannot be Serialized.
    /// Files saved using this system will not be compatible if the object's class is changed between saving and loading
    /// </summary>
    class GameFilePackager
    {
        /* Hi!! This is a class I created for a simple all in one 
         * solution to saving games!
         * You first need to initialize all of the data you're going 
         * to want to save.
         * if you are going to have lists, you'll have to assign them 
         * values in the constructor, I left an example for you.
         * You may want to create a default file to load in case there 
         * is no game to load.
         * Also don't forget to change the namespace so that it works 
         * in your game
         * to Save simply use these lines
         * 
         * SaveFile mySave = new SaveFile(...);
         * SaveFile.Save(mySave);
         * 
         * To load the game use this line
         * 
         * SaveFile GameToLoad =  SaveFile.Load("FileName");
         * 
         * Written By KiwiRoo
         * 
         */

        public string fileName = "default";//required

        /// <summary>
        /// Saves an object to the hard drive with a certain name, the extension is defined as a constant in the method
        /// </summary>
        /// <typeparam name="T">Object type of file you are saving</typeparam>
        /// <param name="gameFile">The object to be saved</param>
        /// <param name="fileName">Name of the file you are saving</param>
        /// <returns></returns>
        public static bool Save<T>(T gameFile , string fileName, string fileExtention)
        {
            /* Here we save
            */

            try
            {
                string currentDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);// find the path
                FileStream fileStream = new FileStream(currentDirectory + "\\" + fileName +"."+fileExtention, FileMode.Create);// start  the filestream
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fileStream, gameFile );
                fileStream.Close();
                Console.WriteLine("Game File Hash: " + gameFile.GetHashCode());
                Console.WriteLine("Game  Successfully");
                return true;
            }
            catch( SerializationException exception)
            {
                Console.WriteLine("Save file intercepted : " + exception);
                return false;
            }
        }

        /// <summary>
        /// Loads an object of a given type with the file name without an extention
        /// </summary>
        /// <typeparam name="T">Type of file to be loaded</typeparam>
        /// <param name="fileName">Name of the file you are loading</param>
        /// <returns>returns an object of given type if it is loaded, returns a null object otherwise</returns>
        public static T Load<T>(string fileName, string fileExtention)
        {
            T LoadedGame = default(T);

            try
            {
                string currentDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);//Find current directory
                FileStream fileStream = new FileStream(currentDirectory + "\\" + fileName + '.'+fileExtention , FileMode.Open);//open filestream
                BinaryFormatter formatter = new BinaryFormatter();//create formatter
                LoadedGame = (T)formatter.Deserialize(fileStream);//load the file
                fileStream.Close();//close the stream
                Console.WriteLine("Game HashCode :" + LoadedGame.GetHashCode()); //display the hash code (used to confirm the object is the same object that was saved)
                Console.WriteLine("Game Loaded Successfully");// Write success message to console
            }
            catch( SerializationException exception)
            {
                Console.WriteLine("ERROR : The file didn't load : " + exception);
            }
            return (T)LoadedGame;
        }

/// <summary>
        /// Reads the file names of the file extention defined in the method
/// </summary>
/// <returns>Returns a list of strings with file names</returns>
        public static List<string> GetFileNames(string fileExtention)
        {

            string currentDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            List<string> gameFileNames = System.IO.Directory.GetFiles(currentDirectory, "*." + fileExtention , SearchOption.AllDirectories).ToList<string>();
            Console.WriteLine(currentDirectory);


            for (int i = 0; i < gameFileNames.Count; i++)// this loop removes all the exccess file path parts
            {

                List<string> pathParsed = gameFileNames[i].Split('\\').ToList();
                gameFileNames[i] = pathParsed[pathParsed.Count - 1];//gets the last thing after \
                pathParsed = gameFileNames[i].Split('.').ToList(); //removes the file extension
                gameFileNames[i] = pathParsed[0];//puts it into the gameFile holder
            }// end path parsing

            return gameFileNames;
        }




    }
}
