using System.Collections.Generic;
using UnityEngine;

namespace RPG.Inventories
{
    [CreateAssetMenu(menuName = ("RPG/Inventory/Drop Library"))]
    public sealed class DropLibrary : ScriptableObject
    {
        [SerializeField] private DropConfig[] potentialDrops;
        [SerializeField] private float[] dropChancePercentage;
        [SerializeField] private int[] minDrops;
        [SerializeField] private int[] maxDrops;

        [System.Serializable]
        class DropConfig
        {
            public InventoryItem item;
            public float[] relativeChance;
            public int[] minNumber;
            public int[] maxNumber;

            public int GetRandomNumber(int level)
            {
                if (!item.IsStackable()) return 1;
                return Random.Range(GetByLevel(minNumber, level), GetByLevel(maxNumber, level) + 1);
            }
        }

        public struct Dropped
        {
            public InventoryItem item;
            public int number;
        }
        
        public IEnumerable<Dropped> GetRandomDrops(int level)
        {
            if (!ShouldRandomDrop(level))
            {
                yield break;
            }
            for (int i = 0; i < GetRandomNumberOfDrops(level); i++)
            {
                yield return GetRandomDrop(level);
            }
        }

        private bool ShouldRandomDrop(int level)
        {
            return Random.Range(0, 100) < GetByLevel(dropChancePercentage, level);
        }

        private int GetRandomNumberOfDrops(int level)
        {
            var min = GetByLevel(minDrops, level);
            var max = GetByLevel(maxDrops, level);
            return Random.Range(min, max);
        }

        private Dropped GetRandomDrop(int level)
        {
            var drop = SelectRandomItem(level);
            var result = new Dropped
            {
                item = drop.item,
                number = drop.GetRandomNumber(level)
            };
            return result;
        }

        private DropConfig SelectRandomItem(int level)
        {
            var totalChance = GetTotalChance(level);
            var randomRoll = Random.Range(0, totalChance);
            var chanceTotal = 0f;
            foreach (var drop in potentialDrops)
            {
                chanceTotal += GetByLevel(drop.relativeChance, level);
                if (chanceTotal > randomRoll)
                {
                    return drop;
                }
            }
            return null;
        }

        private float GetTotalChance(int level)
        {
            float total = 0;
            foreach (var drop in potentialDrops)
            {
                total += GetByLevel(drop.relativeChance, level);
            }
            return total;
        }

        private static T GetByLevel<T>(T[] values, int level)
        {
            if (values.Length == 0)
            {
                return default;
            }
            if (level > values.Length)
            {
                return values[values.Length - 1];
            }
            if (level <= 0)
            {
                return default;
            }
            return values[level - 1];
        }
    }
}
