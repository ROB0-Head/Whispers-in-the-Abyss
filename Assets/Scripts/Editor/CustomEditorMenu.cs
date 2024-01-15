using System.IO;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class CustomEditorMenu : EditorWindow
    {
        [MenuItem("WITA/Show Menu")]
        public static void ShowWindow()
        {
            GetWindow<CustomEditorMenu>("Menu");
        }

        private void OnGUI()
        {
            GUILayout.Label("Custom Menu", EditorStyles.boldLabel);

            if (GUILayout.Button("Delete Save Files"))
            {
                DeleteSaveFiles();
                ClearPlayerPrefs();
            }

            if (GUILayout.Button("Clear PlayerPrefs"))
            {
                ClearPlayerPrefs();
            }
        }

        private void DeleteSaveFiles()
        {
            string savePath = Application.persistentDataPath;
            string[] saveFiles = Directory.GetFiles(savePath);

            foreach (string file in saveFiles)
            {
                File.Delete(file);
            }

            Debug.Log("Save files deleted successfully.");
        }

        private void ClearPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("PlayerPrefs cleared successfully.");
        }
    }
}