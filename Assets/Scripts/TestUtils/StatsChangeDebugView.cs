using RPG.Character;
using RPG.Inventories;
using RPG.Stats;
using UnityEngine;

namespace TestUtils
{
    public sealed class StatsChangeDebugView : MonoBehaviour
    {
        [SerializeField] private PlayerEntity playerEntity;
        private BaseStats _stats;
        private Equipment _equipment;
        
        public void Start()
        {
            _stats = playerEntity.BaseStats;
            _equipment = playerEntity.Equipment;
            _equipment.EquipmentUpdated += ShowStatsChanges;
            _stats.OnLevelUp += ShowLevelUp;
            ShowStatsChanges();
        }

        private void ShowStatsChanges()
        {
            Debug.Log($"Heath: {_stats.GetStat(Stat.Health)}/{_stats.GetBaseStat(Stat.Health)}");
            Debug.Log($"Damage: {_stats.GetStat(Stat.Damage)}/{_stats.GetBaseStat(Stat.Damage)}");
            Debug.Log($"Mana: {_stats.GetStat(Stat.Mana)}/{_stats.GetBaseStat(Stat.Mana)}");
            Debug.Log($"Defence: {_stats.GetStat(Stat.Defence)}/{_stats.GetBaseStat(Stat.Defence)}");
        }

        private void ShowLevelUp()
        {
            Debug.Log($"New level: {_stats.GetLevel()}");
        }

        ~StatsChangeDebugView()
        {
            _equipment.EquipmentUpdated -= ShowStatsChanges;
            _stats.OnLevelUp -= ShowLevelUp;
        }
    }
}
