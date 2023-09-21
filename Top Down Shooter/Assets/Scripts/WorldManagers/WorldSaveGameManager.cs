using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace UB
{
    public class WorldSaveGameManager : MonoBehaviour
    {
        public static WorldSaveGameManager instance;

        private PlayerManager playerManager;

        [Header("Save/Load")]
        [SerializeField] bool saveGame;
        [SerializeField] bool loadGame;

        [Header("Save Data Writer")]
        private SaveFileDataWriter saveFileDataWriter;

        [Header("Save Slot")]
        public PlayerSaveData saveSlot01;
        public PlayerSaveData saveSlot02;

        [Header("Current Player Data")]
        public SaveSlot currentSlotBeingUsed;
        public PlayerSaveData currentSaveData;
        private string saveFileName;

        public List<GameObject> charactersList;

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            /*
            if (saveGame)
            {
                saveGame = false;
                SaveGame();
            }

            if (loadGame)
            {
                loadGame = false;
                LoadAPreviousGame();
            }
            */
        }

        private void CreateSaveFileNameBasedOnSlotBeingUsed()
        {
            switch (currentSlotBeingUsed)
            {
                case SaveSlot.SaveSlot_01:
                    saveFileName = "SaveSlot_01";
                    break;
                case SaveSlot.SaveSlot_02:
                    saveFileName = "SaveSlot_02";
                    break;
            }
        }

        public void CreateNewGame()
        {
            //create a new file with filename depending on slot in use
            CreateSaveFileNameBasedOnSlotBeingUsed();

            currentSaveData = new PlayerSaveData();
        }

        public void LoadAPreviousGame()
        {
            //load previous file with filename depending on which slot we are using
            CreateSaveFileNameBasedOnSlotBeingUsed();

            saveFileDataWriter = new SaveFileDataWriter();
            
            //generally works on most machine types
            saveFileDataWriter.saveDataDirectoryPath = Application.persistentDataPath;
            saveFileDataWriter.saveFileName = saveFileName;

            currentSaveData = saveFileDataWriter.LoadSaveFile();

            playerManager.LoadGameFromCurrentSaveSlot(ref currentSaveData);

            WorldSceneManager.instance.LoadGame();
        }

        public void SaveGame()
        {
            //save current file under specific file name
            CreateSaveFileNameBasedOnSlotBeingUsed();

            saveFileDataWriter = new SaveFileDataWriter();

            //generally works on most machine types
            saveFileDataWriter.saveDataDirectoryPath = Application.persistentDataPath;
            saveFileDataWriter.saveFileName = saveFileName;

            //pass save info to slot save data
            playerManager.SaveGameDataToCurrentSaveSlot(ref currentSaveData);

            //write that info to json
            saveFileDataWriter.CreateNewSaveFile(currentSaveData);

        }

        
    }
}

