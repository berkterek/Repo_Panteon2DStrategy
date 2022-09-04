namespace Panteon2DStrategy.Abstracts.Combats
{
    public interface IHealthDal
    {
        int CurrentHealth { get; }
        bool IsDead { get; }
        void TakeDamage(int damage);
        void SetMaxHealth(int maxHealth);
    }
}