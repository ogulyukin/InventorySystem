using RPG.Stats;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Inventories
{
    public class RandomDropper : ItemDropper
    {
        private readonly float _scatterDistance;
        private readonly DropLibrary _dropLibrary;
        private readonly BaseStats _stats;
     
        public RandomDropper(Transform transform, float scatterDistance, DropLibrary dropLibrary, BaseStats stats) : base(transform)
        {
            _scatterDistance = scatterDistance;
            _dropLibrary = dropLibrary;
            _stats = stats;
        }
        
        private const int Attempts = 30;
        
        public void RandomDrop()
        {
            if(_dropLibrary == null)
                return;
            var drops = _dropLibrary.GetRandomDrops(_stats.GetLevel());
            foreach (var drop in drops)
            {
                DropItem(drop.item, drop.number);
            }
        }
        protected override Vector3 GetDropLocation()
        {
            for (var i = 0; i < Attempts; i++)
            {
                var randomPoint = transform.position + Random.insideUnitSphere * _scatterDistance;
                if (NavMesh.SamplePosition(randomPoint, out var hit, 0.1f, NavMesh.AllAreas))
                {
                    return hit.position;
                }    
            }
            return transform.position;
        }
    }
}
