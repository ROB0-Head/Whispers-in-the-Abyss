using System;
using Settings;
using UI.Prefabs;
using UnityEngine;

namespace UI.Panels
{
    public class StatPanel : BasePanel
    {
        [SerializeField] private GameObject _statParent;

        public void Setup(StatPanelSettings statSettings)
        {
            foreach (var stat in statSettings.Stats)
            {
                var statPrefab = Instantiate(SettingsProvider.Get<PrefabSet>().GetPrefab<StatPrefab>(), _statParent.transform);
                statPrefab.Setup(stat);
            }
        }
    }
}