using RPG.Inventories;
using RPG.Stats;
using UnityEngine;

namespace RPG.Character
{
    public sealed class PlayerEntity : MonoBehaviour
    {
        [Tooltip("Allowed Inventory size")]
        [SerializeField] private int inventorySize = 16;
        [Tooltip("Experience Points on begin game ")]
        [SerializeField] private float startExperiencePoint;
        [SerializeField] private CharacterClass characterClass;
        [SerializeField] private Progression progression;

        public Inventory Inventory { get; private set; }
        public StatsEquipment Equipment { get; private set; }
        public ActionStore ActionStore { get; private set; }
        public Experience Experience { get; private set; }
        public BaseStats BaseStats { get; private set; }
        public ItemDropper ItemDropper { get; private set; }

        private void Awake()
        {
            Inventory = new Inventory(inventorySize);
            Equipment = new StatsEquipment();
            ActionStore = new ActionStore();
            Experience = new Experience(startExperiencePoint);
            BaseStats = new BaseStats(Experience, characterClass, progression, Equipment);
            ItemDropper = new ItemDropper(transform);
        }
    }
}
