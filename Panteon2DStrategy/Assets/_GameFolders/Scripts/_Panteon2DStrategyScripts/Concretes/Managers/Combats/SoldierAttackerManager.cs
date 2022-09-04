using Panteon2DStrategy.Abstracts.Combats;
using Panteon2DStrategy.Abstracts.Controllers;

namespace Panteon2DStrategy.Managers.Combats
{
    public class SoldierAttackerManager : IAttackerService
    {
        readonly IAttackerDal _attackerDal;

        public int Damage => _attackerDal.GetRandomDamage();

        public SoldierAttackerManager(ISoldierController soldierController, IAttackerDal attackerDal)
        {
            _attackerDal = attackerDal;
            var stats = soldierController.Stats;
            _attackerDal.SetMinMaxDamage(stats.MinDamage,stats.MaxDamage);
        }
    }
}