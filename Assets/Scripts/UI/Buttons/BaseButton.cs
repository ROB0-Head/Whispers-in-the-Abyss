using System;
using UnityEngine;

namespace UI.Buttons
{
    public class BaseButton : MonoBehaviour
    {
        public virtual void Setup(ButtonSettings buttonSettings, Action onClickAction)
        {
            
        }
    }

    [Serializable]
    public class ButtonSettings
    {
        
    }
}