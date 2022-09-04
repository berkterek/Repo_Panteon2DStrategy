using Panteon2DStrategy.Abstracts.Combats;
using Panteon2DStrategy.Abstracts.Controllers;
using Panteon2DStrategy.ScriptableObjects;
using UnityEngine;

namespace Panteon2DStrategy.Managers.Combats
{
    public class SoldierAttackerManager : IAttackerService
    {
        readonly IAttackerDal _attackerDal;
        readonly ISoldierStats _stats;
        float _currentRate = 0f;
        float _randomRate = 0f;

        public int Damage => _attackerDal.GetRandomDamage();

        public SoldierAttackerManager(ISoldierController soldierController, IAttackerDal attackerDal)
        {
            _attackerDal = attackerDal;
            _stats = soldierController.Stats;
            _attackerDal.SetMinMaxDamage(_stats.MinDamage, _stats.MaxDamage);
            _currentRate = Random.Range(_stats.MinAttackRate, _stats.MaxAttackRate);
            SetNewAttackRate();
        }

        public void Attack(IHealthController healthController)
        {
            _currentRate += Time.deltaTime;

            if (_currentRate > _randomRate)
            {
                healthController.HealthManager.TakeDamage(this);
                SetNewAttackRate();
            }
        }

        private void SetNewAttackRate()
        {
            _randomRate = Random.Range(_stats.MinAttackRate, _stats.MaxAttackRate);
            _currentRate = 0f;
        }
    }
}