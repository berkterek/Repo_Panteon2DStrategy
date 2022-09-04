using Panteon2DStrategy.Abstracts.Animations;
using Panteon2DStrategy.Abstracts.Movements;
using Panteon2DStrategy.ScriptableObjects;
using UnityEngine;

namespace Panteon2DStrategy.Abstracts.Controllers
{
    public interface ISoldierController : IEntityController,ICanSelectableController
    {
        Transform Target { get; set; }
        Vector3 TargetPosition { get; set; }
        ISoldierStats Stats { get; }
        ISoldierAnimationService AnimationManager { get; }
        IMovementService MovementManager { get; }
    }
}