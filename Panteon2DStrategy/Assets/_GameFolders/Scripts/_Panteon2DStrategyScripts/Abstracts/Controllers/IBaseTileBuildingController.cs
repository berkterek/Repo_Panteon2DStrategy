using Panteon2DStrategy.ScriptableObjects;

namespace Panteon2DStrategy.Abstracts.Controllers
{
    public interface IBaseTileBuildingController : ICanSelectableController,IHealthController
    {
        IBuildingStats Stats { get; }
        
    }
}