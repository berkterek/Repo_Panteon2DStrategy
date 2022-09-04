using UnityEngine;

namespace Panteon2DStrategy.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Building Stats", menuName = "Panteon/Stats/Building Stats")]
    public class BuildingStatsSO : ScriptableObject, IBuildingStats
    {
        [SerializeField] int _maxHealth;

        public int MaxHealth => _maxHealth;
    }

    public interface IBuildingStats
    {
        int MaxHealth { get; }
    }
}

