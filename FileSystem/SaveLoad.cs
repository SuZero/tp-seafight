using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Storage;
using BaseObject;

namespace FileSystem
{
    public static class SaveLoad
    {
        
        public enum DifficultyState
        {
            Easy,
            Normal,
            Hard
        }

        public static StorageDevice device;


        public struct SaveGameData
        {
            public DifficultyState difficultyState;
            public BattleField.Placement[] placement;
        }

        public static void LoadGame()
        {
            
            IAsyncResult result =
             device.BeginOpenContainer("SeaFightSave", null, null);

            // Wait for the WaitHandle to become signaled.
            result.AsyncWaitHandle.WaitOne();

            StorageContainer container = device.EndOpenContainer(result);

            // Close the wait handle.
            result.AsyncWaitHandle.Close();
            string filename = "savegame.sav";

            // Check to see whether the save exists.
            if (!container.FileExists(filename))
            {
                // If not, dispose of the container and return.
                container.Dispose();
                return;
            }
            Stream stream = container.OpenFile(filename, FileMode.Open);
            XmlSerializer serializer = new XmlSerializer(typeof(SaveGameData));
            SaveGameData data = (SaveGameData)serializer.Deserialize(stream);
            // Close the file.
            stream.Close();
            // Dispose the container.
            container.Dispose();
            Thread.Sleep(1000);
        }
        public static void GetDevice()
        {
            IAsyncResult result1 = StorageDevice.BeginShowSelector(PlayerIndex.One, null, null);

            // Wait for the WaitHandle to become signaled.
            result1.AsyncWaitHandle.WaitOne();

            //Sets the global variable.
            device = StorageDevice.EndShowSelector(result1);

            // Close the wait handle.
            result1.AsyncWaitHandle.Close();
        }
        public static void SaveGame( DifficultyState diff)
        {
            SaveGameData savefile;
            savefile.difficultyState = diff;
            savefile.placement =new BattleField.Placement[] {new BattleField.Placement(new BattleField.Ship(4),4,4,Orientation.Horizontal)};
           
            //load the game images into the content pipeline
            IAsyncResult result = device.BeginOpenContainer("SeaFightSave", null, null);

            // Wait for the WaitHandle to become signaled.
            result.AsyncWaitHandle.WaitOne();

            StorageContainer container = device.EndOpenContainer(result);

            // Close the wait handle.
            result.AsyncWaitHandle.Close();

            string filename = "savegame.sav";

            // Check to see whether the save exists.
            if (container.FileExists(filename))
                // Delete it so that we can create one fresh.
                container.DeleteFile(filename);

            // Create the file.
            Stream stream = container.CreateFile(filename);
            // Convert the object to XML data and put it in the stream.
            XmlSerializer serializer = new XmlSerializer(typeof(SaveGameData));

            serializer.Serialize(stream, savefile);
        }
    }
}
