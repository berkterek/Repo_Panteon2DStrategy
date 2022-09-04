using Panteon2DStrategy.Abstracts.Controllers;

namespace Panteon2DStrategy.Abstracts.Combats
{
    public interface IAttackerService
    {
        int Damage { get; }
        void Attack(IHealthController healthController);
    }
}