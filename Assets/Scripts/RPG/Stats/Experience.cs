using System;

namespace RPG.Stats
{
    public sealed class Experience
    {
        public event Action OnExperienceGained;
        private float _experiencePoints;

        public Experience(float experiencePoints)
        {
            _experiencePoints = experiencePoints;
        }

        public void GainExperience(float experience)
        {
            _experiencePoints += experience;
            OnExperienceGained?.Invoke();
        }

        public float GetPoints()
        {
            return _experiencePoints;
        }
    }
}