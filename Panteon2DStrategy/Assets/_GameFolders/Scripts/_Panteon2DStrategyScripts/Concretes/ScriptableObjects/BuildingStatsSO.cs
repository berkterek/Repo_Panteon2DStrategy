using UnityEngine;

namespace Panteon2DStrategy.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Building Stats", menuName = "Panteon/Stats/Building Stats")]
    public class BuildingStatsSO : ScriptableObject
    {
        [SerializeField] int _maxHealth;

        public int MaxHealth => _maxHealth;
    }    
}

