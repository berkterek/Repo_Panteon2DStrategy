using Panteon2DStrategy.Abstracts.Combats;
using Panteon2DStrategy.Enums;

namespace Panteon2DStrategy.Abstracts.Controllers
{
    public interface IHealthController
    {
        IHealthService HealthManager { get; }
        public PlayerType PlayerType { get; }
    }
}