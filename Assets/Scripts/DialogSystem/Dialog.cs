using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialog", menuName = "Dialog")]
public class Dialog : ScriptableObject
{
    [System.Serializable]
    public struct DialogNode
    {
        public string speakerName;
        public string dialogText;
        public Sprite speakerImage;
        public List<DialogOption> options;
    }

    [System.Serializable]
    public struct DialogOption
    {
        public string optionText;
        public Dialog nextDialog;
    }

    public List<DialogNode> dialogNodes = new List<DialogNode>();
}