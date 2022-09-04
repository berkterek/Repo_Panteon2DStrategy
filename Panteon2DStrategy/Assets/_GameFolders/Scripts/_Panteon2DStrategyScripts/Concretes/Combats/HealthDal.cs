using Panteon2DStrategy.Abstracts.Combats;

namespace Panteon2DStrategy.Combats
{
    public class HealthDal : IHealthDal 
    {
        public int CurrentHealth { get; private set; }
        public bool IsDead => CurrentHealth <= 0;

        public void TakeDamage(int damage)
        {
            CurrentHealth -= damage;
        }

        public void SetMaxHealth(int maxHealth)
        {
            CurrentHealth = maxHealth;
        }
    }
}