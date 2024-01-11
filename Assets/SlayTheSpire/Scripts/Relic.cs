using System.Collections;
using System.Collections.Generic;
using Settings;
using UnityEngine;
using UnityEngine.Serialization;

namespace TJ
{
    [CreateAssetMenu]
    public class Relic : ScriptableObject
    {
        public RelicType RelicType;
        public string relicDescription;
        public Sprite relicIcon;
    }
}