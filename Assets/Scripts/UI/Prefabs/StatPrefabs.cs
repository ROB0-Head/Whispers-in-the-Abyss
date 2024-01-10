using Settings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Prefabs
{
    public class StatPrefab : BasePrefabs
    {
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _value;
        [SerializeField] private Button _upgradeValue;
        [SerializeField] private Button _downgradeValue;

        private Stat _currentStat;

        public void Setup(Stat stat)
        {
            _currentStat = stat;

            _name.text = stat.StatName;
            _value.text = stat.CurrentStatValue.ToString();

            _upgradeValue.onClick.AddListener(UpgradeStatValue);
            _downgradeValue.onClick.AddListener(DowngradeStatValue);
        }

        private void UpgradeStatValue()
        {
            _currentStat.CurrentStatValue += 1;
            _value.text = _currentStat.CurrentStatValue.ToString();
        }

        private void DowngradeStatValue()
        {
            _currentStat.CurrentStatValue -= 1;
            _value.text = _currentStat.CurrentStatValue.ToString();
        }
    }
}