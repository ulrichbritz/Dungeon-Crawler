using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

namespace UB
{
    public class SaveFileDataWriter
    {
        public string saveDataDirectoryPath = "";
        public string saveFileName = "";

        //before creating new save file, check to see if file already exists
        public bool CheckToSeeIfFileExists()
        {
            if(File.Exists(Path.Combine(saveDataDirectoryPath, saveFileName)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //used to delete save slots
        public void DeleteSaveFile()
        {
            File.Delete(Path.Combine(saveDataDirectoryPath, saveFileName));
        }

        // used to create save file upon starting a new game
        public void CreateNewSaveFile(PlayerSaveData playerSaveData)
        {
            // make a path to the file (location on pc)
            string savePath = Path.Combine(saveDataDirectoryPath, saveFileName);

            try
            {
                // create directory file will be written to, if it does not already exist
                Directory.CreateDirectory(Path.GetDirectoryName(savePath));
                Debug.Log("Creating Save File at Save Path: " + savePath);

                //serialize the C# game data obj to JSON
                string dataToStore = JsonUtility.ToJson(playerSaveData, true);

                //write file to system
                using(FileStream stream = new FileStream(savePath, FileMode.Create))
                {
                    using (StreamWriter fileWriter = new StreamWriter(stream))
                    {
                        fileWriter.Write(dataToStore);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("ERROR WHILE TRYING TO SAVE PLAYER DATA " + savePath + "\n" + ex);
            }
        }

        //used to load a save file upon loading 
        public PlayerSaveData LoadSaveFile()
        {
            PlayerSaveData playerSaveData = null;

            // make a path to load the file (location on pc)
            string loadPath = Path.Combine(saveDataDirectoryPath, saveFileName);

            if (File.Exists(loadPath))
            {
                try
                {
                    string dataToLoad = "";

                    using (FileStream stream = new FileStream(loadPath, FileMode.Open))
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            dataToLoad = reader.ReadToEnd();
                        }
                    }

                    //deserialize the data from JSON to Unity format
                    playerSaveData = JsonUtility.FromJson<PlayerSaveData>(dataToLoad);
                }
                catch (Exception ex)
                {
                    Debug.LogError("Failed to load file");
                }

                
            }

            return playerSaveData;

        }
    }
}

