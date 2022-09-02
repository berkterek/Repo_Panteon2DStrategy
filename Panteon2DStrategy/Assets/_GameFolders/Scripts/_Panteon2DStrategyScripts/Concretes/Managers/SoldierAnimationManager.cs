using Panteon2DStrategy.Abstracts.Animations;
using Panteon2DStrategy.Controllers;
using Pathfinding;
using UnityEngine;

namespace Panteon2DStrategy.Managers
{
    public class SoldierAnimationManager : ISoldierAnimationService
    {
        readonly IAnimationDal _animationDal;
        readonly AIPath _aiPath;
        
        Vector3 _velocity;
        
        public SoldierAnimationManager(ISoldierController soldierController, IAnimationDal animationDal)
        {
            _animationDal = animationDal;
            _aiPath = soldierController.Transform.GetComponent<AIPath>();
        }

        public void Tick()
        {
            _velocity = _aiPath.velocity;
        }

        public void LateTick()
        {
            _animationDal.DirectionSetterAnimation(_velocity);
            _animationDal.IsMovingAnimation(_velocity.magnitude > 0f);
        }

        public void Dying()
        {
            _animationDal.DyingAnimation();
        }

        public void IsAttacking(bool value)
        {
            _animationDal.IsAttackingAnimation(value);
        }
    }
}
