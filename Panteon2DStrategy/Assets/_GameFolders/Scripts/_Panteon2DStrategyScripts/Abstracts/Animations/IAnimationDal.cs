using UnityEngine;

namespace Panteon2DStrategy.Abstracts.Animations
{
    public interface IAnimationDal
    {
        void DirectionSetterAnimation(Vector3 value);
        void IsMovingAnimation(bool value);
        void IsAttackingAnimation(bool value);
        void DyingAnimation();

        public bool IsMoving { get; }
        public bool IsAttacking { get; }
    }
}