using System;
using System.IO;
using Newtonsoft.Json;
using Settings;
using UnityEngine;

namespace SaveSystem
{
    public static class SaveManager
    {
        #region Saves Data Names

        private static readonly string CHARACTER_DATA_NAME = "CharacterData.json";

        #endregion

        #region Gloval Save And Load Methods

        public static void SaveData<T>(T data, string fileName)
        {
            string path = Path.Combine(Application.persistentDataPath, fileName);
            string json = JsonConvert.SerializeObject(data);
            File.WriteAllText(path, json);
        }

        public static T LoadData<T>(string fileName, T defaultData)
        {
            string path = Path.Combine(Application.persistentDataPath, fileName);

            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                return JsonConvert.DeserializeObject<T>(json);
            }
            else
            {
                SaveData(defaultData, fileName);
                return defaultData;
            }
        }

        public static T LoadData<T>(string fileName) where T : ScriptableObject, new()
        {
            string path = Path.Combine(Application.persistentDataPath, fileName);

            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                JsonUtility.FromJsonOverwrite(json, SettingsProvider.Get<T>());
                return SettingsProvider.Get<T>();
            }
            else
            {
                return new T();
            }
        }

        #endregion

        #region Public Save Method

        public static void SaveCharacterData(Character userData)
        {
            string path = Path.Combine(Application.persistentDataPath, CHARACTER_DATA_NAME);

            string jsonData = JsonUtility.ToJson(userData);

            File.WriteAllText(path, jsonData);
        }

        #endregion

        #region Public Load Method

        public static Character LoadCharacterData()
        {
            string path = Path.Combine(Application.persistentDataPath, CHARACTER_DATA_NAME);

            if (File.Exists(path))
            {
                string jsonData = File.ReadAllText(path);
                Character loadedUserData = JsonUtility.FromJson<Character>(jsonData);
                return loadedUserData;
            }
            else
            {
                return new Character();
            }
        }

        #endregion

        #region Public Delete Method

        public static void DeleteChatSave(string name)
        {
            string filePath = Path.Combine(Application.persistentDataPath, $"{name}.json");
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    Debug.Log("File delete: " + $"{name}.json");
                }
                else
                {
                    Debug.LogWarning("File not found: " + $"{name}.json");
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Delete has error: " + e.Message);
            }
        }

        #endregion
    }
}