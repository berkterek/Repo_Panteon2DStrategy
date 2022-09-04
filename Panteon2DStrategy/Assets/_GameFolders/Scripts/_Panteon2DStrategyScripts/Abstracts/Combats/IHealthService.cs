namespace Panteon2DStrategy.Abstracts.Combats
{
    public interface IHealthService
    {
        void TakeDamage(IAttackerService attackerManager);
        event System.Action<int, int> OnTookDamage;
        event System.Action OnDead;
    }
}