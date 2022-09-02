using Panteon2DStrategy.Abstracts.Animations;
using Panteon2DStrategy.Abstracts.Controllers;
using Panteon2DStrategy.Animations;
using Panteon2DStrategy.Managers;
using Panteon2DStrategyScripts.Helpers;
using UnityEngine;

namespace Panteon2DStrategy.Controllers
{
    public class SoldierController : MonoBehaviour,ISoldierController
    {
        [SerializeField] Transform _transform;

        public Transform Transform => _transform;
        public Transform Target { get; set; }
        public ISoldierAnimationService AnimationManager { get; private set; }

        void Awake()
        {
            this.GetReference(ref _transform);
            AnimationManager = new SoldierAnimationManager(this, new SoldierAnimatorDal(this));
        }

        void OnValidate()
        {
            this.GetReference(ref _transform);
        }

        void Update()
        {
            AnimationManager.Tick();
        }

        void LateUpdate()
        {
            AnimationManager.LateTick();
        }
    }

    public interface ISoldierController : IEntityController
    {
        Transform Target { get; set; }
        ISoldierAnimationService AnimationManager { get; }
    }
}