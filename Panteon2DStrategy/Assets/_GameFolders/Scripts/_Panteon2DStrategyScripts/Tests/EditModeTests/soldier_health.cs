using NUnit.Framework;
using NSubstitute;
using Panteon2DStrategy.Abstracts.Combats;
using Panteon2DStrategy.Abstracts.Controllers;
using Panteon2DStrategy.Managers;
using Panteon2DStrategy.ScriptableObjects;

namespace Combats
{
    public class soldier_health
    {
        IHealthDal _healthDal;
        IHealthService _soldierHealthManager;
        
        [SetUp]
        public void Setup()
        {
            var soldierController = Substitute.For<ISoldierController>();
            soldierController.Stats.Returns(Substitute.For<ISoldierStats>());
            soldierController.Stats.MaxHealth.Returns(100);
            _healthDal = Substitute.For<IHealthDal>();
            
            _soldierHealthManager = new SoldierHealthManager(soldierController, _healthDal);
        }
        
        [Test]
        public void soldier_take_damage()
        {
            var attackerManager = Substitute.For<IAttackerService>();
            int damageValue = 1;
            attackerManager.Damage.Returns(damageValue);
            
            _soldierHealthManager.TakeDamage(attackerManager);
            
            _healthDal.Received().TakeDamage(damageValue);
        }
        
        [Test]
        public void if_is_dead_return_true_soldier_can_not_take_damage()
        {
            _healthDal.IsDead.Returns(true);
            
            var attackerManager = Substitute.For<IAttackerService>();
            int damageValue = 1;
            attackerManager.Damage.Returns(damageValue);
            
            _soldierHealthManager.TakeDamage(attackerManager);
            
            _healthDal.DidNotReceive().TakeDamage(damageValue);
        }

        [Test]
        public void if_is__take_one_fatal_damage_and_dead_soldier_can_take_another_damage()
        {
            _healthDal.IsDead.Returns(false);
            
            var attackerManager = Substitute.For<IAttackerService>();
            int damageValue = 1000;
            attackerManager.Damage.Returns(damageValue);

            int counter = 0;
            _soldierHealthManager.OnTookDamage += (max, current) => counter++;  
            _soldierHealthManager.TakeDamage(attackerManager);
            _healthDal.IsDead.Returns(true);

            for (int i = 0; i < 100; i++)
            {
                _soldierHealthManager.TakeDamage(attackerManager);
            }
            
            Assert.AreEqual(1,counter);
        }
    }    
}

