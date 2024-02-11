using System;
using RPG.Inventories;

namespace RPG.Stats
{
    public sealed class BaseStats
    {
        private readonly Experience _experience;
        private readonly StatsEquipment _equipment;
        private readonly CharacterClass _characterClass;
        private readonly Progression _progression;
        private int _currentLevel;

        public event Action OnLevelUp;

        public BaseStats(Experience experience, CharacterClass characterClass, Progression progression, StatsEquipment equipment) 
        {
            _experience = experience;
            _equipment = equipment;
            _characterClass = characterClass;
            _progression = progression;
            _currentLevel = CalculateLevel();
            _experience.OnExperienceGained += UpdateLevel;
        }

        ~BaseStats() 
        {
            _experience.OnExperienceGained -= UpdateLevel;
        }

        private void UpdateLevel() 
        {
            var newLevel = CalculateLevel();
            if (newLevel > _currentLevel)
            {
                _currentLevel = newLevel;
                OnLevelUp?.Invoke();
            }
        }

        public float GetStat(Stat stat)
        {
            return (GetBaseStat(stat) + GetAdditiveModifier(stat)) * (1 + GetPercentageModifier(stat)/100);
        }

        public float GetBaseStat(Stat stat)
        {
            return _progression.GetStat(stat, _characterClass, GetLevel());
        }

        public int GetLevel()
        {
            return _currentLevel;
        }

        private float GetAdditiveModifier(Stat stat)
        {
            float total = 0;
            foreach (var modifier in _equipment.GetAdditiveModifiers(stat))
            {
                total += modifier;
            }
            return total;
        }

        private float GetPercentageModifier(Stat stat)
        {
            float total = 0;
            
            foreach (var modifier in _equipment.GetPercentageModifiers(stat))
            {
                total += modifier;
            }
            return total;
        }

        private int CalculateLevel()
        {
            var currentXp = _experience.GetPoints();
            var penultimateLevel = _progression.GetLevels(Stat.ExperienceToLevelUp, _characterClass);
            for (var level = 1; level <= penultimateLevel; level++)
            {
                var xpToLevelUp = _progression.GetStat(Stat.ExperienceToLevelUp, _characterClass, level);
                if (xpToLevelUp > currentXp)
                {
                    return level - 1;
                }
            }
            return penultimateLevel + 1;
        }
    }
}
