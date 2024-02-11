using System.Collections.Generic;
using NUnit.Framework;
using RPG.Inventories;
using RPG.Stats;
using UnityEngine;

namespace Tests
{
    [TestFixture]
    public class InventoryTests
    {
        private Inventory _inventory;
        private StatsEquipment _equipment;
        private readonly StatsEquipableItem[] _items = Resources.LoadAll<StatsEquipableItem>("");
        
        [Test]
        public void AddItemTest()
        {
            //Arrange
            _inventory = new Inventory(4);
            var item = _items[Random.Range(0, _items.Length - 1)];
            
            //Action
            _inventory.AddToFirstEmptySlot(item, 1);
            
            //Assert
            var emptySlots = 0;
            for (int i = 0; i < _inventory.GetSize(); i++)
            {
                if (_inventory.GetItemInSlot(i) == null)
                {
                    emptySlots++;
                }
            }
            Assert.AreEqual(3, emptySlots);
            Assert.AreNotEqual(0, _items.Length);
        }
        
        [Test]
        public void RemoveItemTest()
        {
            //Arrange
            //was done in previous test
            
            //Action
            var busySlot = -1;
            for (int i = 0; i < _inventory.GetSize(); i++)
            {
                if (_inventory.GetItemInSlot(i) != null)
                    busySlot = i;
            }
            
            _inventory.RemoveFromSlot(busySlot, 1);
            
            //Assert
            var emptySlots = 0;
            for (int i = 0; i < _inventory.GetSize(); i++)
            {
                if (_inventory.GetItemInSlot(i) == null)
                {
                    emptySlots++;
                }
            }
            Assert.AreNotEqual(-1, busySlot);
            Assert.AreEqual(4, emptySlots);
        }

        [Test]
        public void AddHelmetTest()
        {
            //Arrange
            _equipment = new StatsEquipment();
            var baseDamageModifier = GetModifiersResult(_equipment.GetAdditiveModifiers(Stat.Mana));
            var baseDefenceModifier = GetModifiersResult(_equipment.GetAdditiveModifiers(Stat.Defence));
            var helmetIndex = GetItemIndexByName("Bronze Helm");

            //Action
            _equipment.AddItem(EquipLocation.Helmet, _items[helmetIndex]);

            //Assert
            Assert.AreNotEqual(-1, helmetIndex);
            Assert.AreEqual(_equipment.GetItemInSlot(EquipLocation.Helmet), _items[helmetIndex]);
            Assert.AreEqual(baseDamageModifier + 30f, GetModifiersResult(_equipment.GetAdditiveModifiers(Stat.Damage)));
            Assert.AreEqual(baseDefenceModifier + 6f, GetModifiersResult(_equipment.GetAdditiveModifiers(Stat.Defence)));
        }
        
        
        [Test]
        public void AddShieldTest()
        {
            //Arrange
            var shieldIndex = GetItemIndexByName("WoodenOvalShield");
            var baseManaModifier = GetModifiersResult(_equipment.GetAdditiveModifiers(Stat.Mana));
            var baseDefenceModifier = GetModifiersResult(_equipment.GetAdditiveModifiers(Stat.Defence));
            
            //Action
            _equipment.AddItem(EquipLocation.Shield, _items[shieldIndex]);

            //Assert
            Assert.AreNotEqual(-1, shieldIndex);
            Assert.AreEqual(_equipment.GetItemInSlot(EquipLocation.Shield), _items[shieldIndex]); 
            Assert.AreEqual(baseManaModifier + 40f,GetModifiersResult(_equipment.GetAdditiveModifiers(Stat.Mana)));
            Assert.AreEqual(baseDefenceModifier + 3f,GetModifiersResult(_equipment.GetAdditiveModifiers(Stat.Defence)));
        }

        [Test]
        public void RemoveHelm()
        {
            //Arrange
            var baseDamageModifier = GetModifiersResult(_equipment.GetAdditiveModifiers(Stat.Damage));
            var baseDefenceModifier = GetModifiersResult(_equipment.GetAdditiveModifiers(Stat.Defence));
            
            //Action
            _equipment.RemoveItem(EquipLocation.Helmet);
            
            //Assert
            Assert.AreEqual(_equipment.GetItemInSlot(EquipLocation.Helmet), null);
            Assert.AreEqual(baseDamageModifier - 30f, GetModifiersResult(_equipment.GetAdditiveModifiers(Stat.Damage)));
            Assert.AreEqual(baseDefenceModifier - 6f, GetModifiersResult(_equipment.GetAdditiveModifiers(Stat.Defence)));
        }
        
        [Test]
        public void RemoveShieldTest()
        {
            //Arrange
            var baseManaModifier = GetModifiersResult(_equipment.GetAdditiveModifiers(Stat.Mana));
            var baseDefenceModifier = GetModifiersResult(_equipment.GetAdditiveModifiers(Stat.Defence));
            
            //Action
            _equipment.RemoveItem(EquipLocation.Shield);

            //Assert
            Assert.AreEqual(_equipment.GetItemInSlot(EquipLocation.Shield), null);
            Assert.AreEqual(baseManaModifier - 40f,GetModifiersResult(_equipment.GetAdditiveModifiers(Stat.Mana)));
            Assert.AreEqual(baseDefenceModifier - 3f,GetModifiersResult(_equipment.GetAdditiveModifiers(Stat.Defence)));
        }

        private int GetItemIndexByName(string name)
        {
            var index = -1;
            for(var i = 0; i <_items.Length; i++)
            {
                if (_items[i].name == name)
                {
                    index = i;
                    break;
                }
            }

            return index;
        }

        private float GetModifiersResult(IEnumerable<float> values)
        {
            var result = 0f;
            foreach (var value in values)
            {
                result += value;
            }

            return result;
        }
    }
}
