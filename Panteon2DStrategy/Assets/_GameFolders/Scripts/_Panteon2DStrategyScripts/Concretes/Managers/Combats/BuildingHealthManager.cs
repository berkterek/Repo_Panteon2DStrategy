using Panteon2DStrategy.Abstracts.Combats;
using Panteon2DStrategy.Abstracts.Controllers;
using UnityEngine;

namespace Panteon2DStrategy.Managers.Combats
{
    public class BuildingHealthManager : IHealthService
    {
        readonly IHealthDal _healthDal;
        readonly int _maxHealth;
        
        public event System.Action<int, int> OnTookDamage;
        public event System.Action OnDead;
        
        public BuildingHealthManager(IBaseTileBuildingController buildingController, IHealthDal healthDal)
        {
            _healthDal = healthDal;
            _maxHealth = buildingController.Stats.MaxHealth;
            _healthDal.SetMaxHealth(_maxHealth);
        }
        
        public void TakeDamage(IAttackerService attackerManager)
        {
            if (_healthDal.IsDead) return;
            
            _healthDal.TakeDamage(attackerManager.Damage);
            OnTookDamage?.Invoke(_maxHealth,_healthDal.CurrentHealth);
            Debug.Log(_healthDal.CurrentHealth);

            if (_healthDal.IsDead)
            {
                OnDead?.Invoke();
            }
        }
    }
}