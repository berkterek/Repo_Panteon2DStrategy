using Sirenix.OdinInspector;
using UnityEngine;

namespace Panteon2DStrategy.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Soldier Stats", menuName = "Panteon/Stats/Soldier Stats")]
    public class SoldierStatsSO : ScriptableObject,ISoldierStats
    {
        [BoxGroup("Movement Infos")]
        [Range(1,1000)]
        [SerializeField] float _moveSpeed;
        [BoxGroup("Movement Infos")]
        [Range(0.1f,2)]
        [SerializeField] float _stopDistance;
        
        [BoxGroup("Health Infos")]
        [Range(1,1000)]
        [SerializeField] int _maxHealth;
        
        [BoxGroup("Damage Infos")]
        [Range(1,10)]
        [SerializeField] int _minDamage;
        [Range(1,10)]
        [BoxGroup("Damage Infos")]
        [SerializeField] int _maxDamage;
        [BoxGroup("Damage Infos")]
        [Range(1,10)]
        [SerializeField] int _minAttackRate;
        [Range(1,10)]
        [BoxGroup("Damage Infos")]
        [SerializeField] int _maxAttackRate;
        
        public float MoveSpeed => _moveSpeed;
        public float StopDistance => _stopDistance;
        public int MaxHealth => _maxHealth;
        public int MinDamage => _minDamage;
        public int MaxDamage => _maxDamage;
        public int MinAttackRate => _minAttackRate;
        public int MaxAttackRate => _maxAttackRate;
        
        void OnValidate()
        {
            SetDamage();
            
            SetAttackRage();
        }

        private void SetAttackRage()
        {
            if (_minAttackRate > _maxAttackRate)
            {
                _maxAttackRate = _minAttackRate;
                _minAttackRate--;
            }
            else if (_maxAttackRate < _minAttackRate)
            {
                _minAttackRate = _maxAttackRate;
                _maxAttackRate++;
            }
        }

        private void SetDamage()
        {
            if (_minDamage > _maxDamage)
            {
                _maxDamage = _minDamage;
                _minDamage--;
            }
            else if (_maxDamage < _minDamage)
            {
                _minDamage = _maxDamage;
                _maxDamage++;
            }
        }
    }

    public interface ISoldierStats
    {
        public float MoveSpeed { get; }
        public float StopDistance { get; }
        public int MaxHealth { get; }
        public int MinDamage { get; }
        public int MaxDamage { get; }
        public int MinAttackRate { get; }
        public int MaxAttackRate { get; }
    }
}