using Panteon2DStrategy.Abstracts.Animations;
using Panteon2DStrategy.Abstracts.Controllers;
using Panteon2DStrategy.Helpers;
using UnityEngine;

namespace Panteon2DStrategy.Animations
{
    public class SoldierAnimatorDal : IAnimationDal
    {
        readonly Animator _animator;

        public bool IsMoving => _animator.GetBool(AnimationCacheHelper.IsRunning);
        public bool IsAttacking => _animator.GetBool(AnimationCacheHelper.IsAttacking);

        public SoldierAnimatorDal(IEntityController entityController)
        {
            _animator = entityController.Transform.GetComponentInChildren<Animator>();
        }

        public void DirectionSetterAnimation(Vector3 value)
        {
            _animator.SetFloat(AnimationCacheHelper.DirectionY, value.y, 0.3f, Time.deltaTime);
            _animator.SetFloat(AnimationCacheHelper.DirectionX, value.x, 0.3f, Time.deltaTime);
        }

        public void IsMovingAnimation(bool value)
        {
            _animator.SetBool(AnimationCacheHelper.IsRunning, value);
        }

        public void IsAttackingAnimation(bool value)
        {
            _animator.SetBool(AnimationCacheHelper.IsAttacking, value);
        }

        public void DyingAnimation()
        {
            _animator.SetTrigger(AnimationCacheHelper.Dying);
        }
    }
}