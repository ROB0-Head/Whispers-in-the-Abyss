using System.Collections.Generic;
using System.Linq;
using Map;
using UI.Buttons;
using UI.Panels;
using UI.Popups;
using UI.Prefabs;
using UI.Screens;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "BattlePrefabSet", menuName = "WITA/BattlePrefabSet", order = 0)]
    public class BattlePrefabSet : ScriptableObject
    {
        [field: SerializeField] public List<BasePanel> Panels { private set; get; }
        [field: SerializeField] public List<DefaultScreen> Screens { private set; get; }
        [field: SerializeField] public List<BasePopup> Popups { private set; get; }
        [field: SerializeField] public List<BaseButton> Buttons { private set; get; }
        [field: SerializeField] public List<BasePrefabs> Prefabs { private set; get; }
        [field: SerializeField] public RewardContainer RewardContainer { private set; get; }

        public T GetPanel<T>() where T : BasePanel
        {
            return Panels.First(x => x is T) as T;
        }

        public T GetScreen<T>() where T : DefaultScreen
        {
            return Screens.First(x => x is T) as T;
        }

        public T GetPopup<T>() where T : BasePopup
        {
            return Popups.First(x => x is T) as T;
        }

        public T GetButton<T>() where T : BaseButton
        {
            return Buttons.First(x => x is T) as T;
        }
        
        public T GetPrefab<T>() where T : BasePrefabs
        {
            return Prefabs.First(x => x is T) as T;
        }
    }
}