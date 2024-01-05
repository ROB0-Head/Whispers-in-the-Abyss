using System.Collections.Generic;
using UnityEngine;

namespace DialogSystem
{
    [CreateAssetMenu(fileName = "Dialog", menuName = "WITA/Dialog")]
    public class Dialog : ScriptableObject
    {    
        public string speakerName;
        public DialogNode dialogNodes;

    
        [System.Serializable]
        public struct DialogNode
        {
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

  
    }
}