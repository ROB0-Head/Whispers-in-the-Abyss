using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Settings;
using Settings.BattleManager.Cards;
using UnityEngine;

namespace SaveSystem
{
    public static class SaveManager
    {
        #region Saves Data Names

        private static readonly string CHARACTER_DATA_NAME = "CharacterData.json";
        private static readonly string DECK_DATA_NAME = "DeckData.json";

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

        public static void SaveDeck(List<Card> deck)
        {
            string path = Path.Combine(Application.persistentDataPath, DECK_DATA_NAME);

            string jsonData = JsonConvert.SerializeObject(deck);
            string encryptedData = Encrypt(jsonData);
            File.WriteAllText(path, encryptedData);
        }

        public static void SaveCharacterData(Character userData)
        {
            string jsonData = JsonUtility.ToJson(userData);

            string path = Path.Combine(Application.persistentDataPath, CHARACTER_DATA_NAME);
            string encryptedData = Encrypt(jsonData);

            File.WriteAllText(path, encryptedData);
        }

        #endregion

        #region Public Load Method

        public static List<Card> LoadDeck()
        {
            string path = Path.Combine(Application.persistentDataPath, DECK_DATA_NAME);
            if (File.Exists(path))
            {
                string encryptedData = File.ReadAllText(path);
                string decryptedData = Decrypt(encryptedData);
                return JsonConvert.DeserializeObject<List<Card>>(decryptedData);
            }
            else
            {
                return new List<Card>(); 
            }
        }
        
        public static Character LoadCharacterData()
        {
            string path = Path.Combine(Application.persistentDataPath, CHARACTER_DATA_NAME);

            if (File.Exists(path))
            {
                string jsonData = File.ReadAllText(path);
                string decryptedData = Decrypt(jsonData);
                return JsonUtility.FromJson<Character>(decryptedData);
            }
            else
            {
                return new Character();
            }
        }

        #endregion

        #region Utils

        private static string Encrypt(string data)
        {
            StringBuilder encryptedData = new StringBuilder();
            foreach (char c in data)
            {
                encryptedData.Append((char)(c ^ 42));
            }

            return encryptedData.ToString();
        }

        private static string Decrypt(string data)
        {
            return Encrypt(data);
        }

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