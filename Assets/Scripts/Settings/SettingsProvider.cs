using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "SettingsProvider", menuName = "WITA/SettingsProvider", order = 0)]
    public class SettingsProvider : ScriptableObject
    {
        [ContextMenu("Sort alphabetically")]
        public void SortAlphabetically()
        {
#if UNITY_EDITOR
            _settingsList = _settingsList.OrderBy(element => element.name).ToList();

            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
#endif
        }

        private static string _containerName = "SettingsProvider";
        private static Dictionary<Type, ScriptableObject> _settings;
        
        [SerializeField] private List<ScriptableObject> _settingsList;
        public List<ScriptableObject> SettingsList => _settingsList;

        [ContextMenu("Check list for identical types")]
        public void CheckTypes()
        {
            var types = new List<Type>();
            
            foreach (var s in _settingsList)
            {
                if (types.Contains(s.GetType()))
                {
                    Debug.LogError($"Found identical type: {types.Count()} - {s.GetType()}");
                }
                types.Add(s.GetType());
            }
            
        }
        
        private static void CheckSettings()
        {
            if (_settings != null)
                return;
            
            var settingsContainer = Resources.Load<SettingsProvider>(_containerName);
            SetupSettings(settingsContainer);
        }
        
        private static void SetupSettings(SettingsProvider settingsContainer)
        {
            try
            {
                _settings = settingsContainer.SettingsList.ToDictionary(x => x.GetType(), x => x);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                throw;
            }
        }

        public static T Get<T>() where T : ScriptableObject
        {
            CheckSettings();

            if (_settings.ContainsKey(typeof(T)))
            {
                return (T)_settings[typeof(T)];
            }

            Debug.LogWarning($"Not found settings of type \"{typeof(T).FullName}\"");
            return null;
        }
    }
}