using Panteon2DStrategy.Abstracts.Combats;
using Panteon2DStrategy.Abstracts.Controllers;

namespace Panteon2DStrategy.Managers
{
    public class SoldierHealthManager : IHealthService
    {
        readonly IHealthDal _healthDal;
        readonly int _maxHealth;

        public event System.Action<int, int> OnTookDamage;
        public event System.Action OnDead;

        public SoldierHealthManager(ISoldierController soldierController, IHealthDal healthDal)
        {
            _maxHealth = soldierController.Stats.MaxHealth;
            _healthDal = healthDal;
            _healthDal.SetMaxHealth(_maxHealth);
        }

        public void TakeDamage(IAttackerService attackerManager)
        {
            if (_healthDal.IsDead) return;
            
            _healthDal.TakeDamage(attackerManager.Damage);
            OnTookDamage?.Invoke(_maxHealth,_healthDal.CurrentHealth);

            if (_healthDal.IsDead)
            {
                OnDead?.Invoke();
            }
        }
    }
}