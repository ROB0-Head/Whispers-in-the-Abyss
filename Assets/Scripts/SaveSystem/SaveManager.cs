using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using BattleSystem.Cards;
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
            RootObject saveDeck = new RootObject();
            saveDeck.CardsList = new List<CardData>();
            foreach (var card in deck)
            {
                CardData cardData = new CardData()
                {
                    CardTitle = card.CardTitle,
                    IsUpgraded = card.IsUpgraded,
                    CardDescription = card.CardDescription,
                    CardEnergy = card.CardEnergy,
                    CardStat = card.CardStat,
                    BuffAmount = card.BuffAmount,
                    CardType = card.CardType
                };

                if (card is AttackCard attackCard)
                {
                    cardData.AttackType = attackCard.AttackType;
                }
                else if (card is DefenseCard defenseCard)
                {
                    cardData.DefenseType = defenseCard.DefenseType;
                }
                else if (card is SkillCard skillCard)
                {
                    cardData.SkillType = skillCard.SkillType;
                }

                saveDeck.CardsList.Add(cardData);
            }

            string jsonData = JsonConvert.SerializeObject(saveDeck);
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
                List<Card> deck = new List<Card>();
                string encryptedData = File.ReadAllText(path);
                string decryptedData = Decrypt(encryptedData);
                RootObject root = JsonConvert.DeserializeObject<RootObject>(decryptedData);

                foreach (var card in root.CardsList)
                {
                    if (card.CardType == CardType.Attack)
                    {
                        AttackCard attackCard = new AttackCard(
                            card.CardTitle,
                            card.IsUpgraded,
                            new CardDescription
                            {
                                baseAmount = card.CardDescription.baseAmount,
                                upgradedAmount = card.CardDescription.upgradedAmount
                            },
                            new CardAmount
                            {
                                baseAmount = card.CardEnergy.baseAmount,
                                upgradedAmount = card.CardEnergy.upgradedAmount
                            },
                            new CardAmount
                            {
                                baseAmount = card.CardStat.baseAmount,
                                upgradedAmount = card.CardStat.upgradedAmount
                            },
                            new CardAmount
                            {
                                baseAmount = card.BuffAmount.baseAmount,
                                upgradedAmount = card.BuffAmount.upgradedAmount
                            },
                            card.CardType,
                            card.AttackType
                        );
                        deck.Add(attackCard);
                    }
                    else if (card.CardType == CardType.Defense)
                    {
                        DefenseCard defenseCard = new DefenseCard(
                            card.CardTitle,
                            card.IsUpgraded,
                            new CardDescription
                            {
                                baseAmount = card.CardDescription.baseAmount,
                                upgradedAmount = card.CardDescription.upgradedAmount
                            },
                            new CardAmount
                            {
                                baseAmount = card.CardEnergy.baseAmount,
                                upgradedAmount = card.CardEnergy.upgradedAmount
                            },
                            new CardAmount
                            {
                                baseAmount = card.CardStat.baseAmount,
                                upgradedAmount = card.CardStat.upgradedAmount
                            },
                            new CardAmount
                            {
                                baseAmount = card.BuffAmount.baseAmount,
                                upgradedAmount = card.BuffAmount.upgradedAmount
                            },
                            card.CardType,
                            card.DefenseType
                        );
                        deck.Add(defenseCard);
                    }
                    else if (card.CardType == CardType.Skill)
                    {
                        SkillCard skillCard = new SkillCard(
                            card.CardTitle,
                            card.IsUpgraded,
                            new CardDescription
                            {
                                baseAmount = card.CardDescription.baseAmount,
                                upgradedAmount = card.CardDescription.upgradedAmount
                            },
                            new CardAmount
                            {
                                baseAmount = card.CardEnergy.baseAmount,
                                upgradedAmount = card.CardEnergy.upgradedAmount
                            },
                            new CardAmount
                            {
                                baseAmount = card.CardStat.baseAmount,
                                upgradedAmount = card.CardStat.upgradedAmount
                            },
                            new CardAmount
                            {
                                baseAmount = card.BuffAmount.baseAmount,
                                upgradedAmount = card.BuffAmount.upgradedAmount
                            },
                            card.CardType,
                            card.SkillType
                        );
                        deck.Add(skillCard);
                    }
                }

                return deck;
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