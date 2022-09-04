using Panteon2DStrategy.Abstracts.Combats;

namespace Panteon2DStrategy.Abstracts.Controllers
{
    public interface IAttackerController
    {
        IAttackerService AttackerManager { get; }
    }
}