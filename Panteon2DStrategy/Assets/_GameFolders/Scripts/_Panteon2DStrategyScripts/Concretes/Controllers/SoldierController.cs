using System;
using Panteon2DStrategy.Abstracts.Animations;
using Panteon2DStrategy.Abstracts.Controllers;
using Panteon2DStrategy.Abstracts.Movements;
using Panteon2DStrategy.Animations;
using Panteon2DStrategy.Managers;
using Panteon2DStrategy.Managers.Movements;
using Panteon2DStrategyScripts.Helpers;
using UnityEngine;

namespace Panteon2DStrategy.Controllers
{
    public class SoldierController : MonoBehaviour,ISoldierController
    {
        [SerializeField] Transform _transform;
        [SerializeField] Transform _destinationTarget;
        [SerializeField] bool _isSelected = false;
 
        public Transform Transform => _transform;
        public Transform Target { get; set; }
        public bool IsSelected => _isSelected;
        public ISoldierAnimationService AnimationManager { get; private set; }
        public IMovementService MovementManager { get; private set; }
        public Vector3 TargetPosition { get; set; }
        
        public event System.Action<bool> OnToggleValueChanged;

        void Awake()
        {
            this.GetReference(ref _transform);
            AnimationManager = new SoldierAnimationManager(this, new SoldierAnimatorDal(this));
            MovementManager = new SoldierMovementManager(this, new MoveWithDirectPositionDal(_destinationTarget));
        }

        void OnValidate()
        {
            this.GetReference(ref _transform);
        }

        void Update()
        {
            AnimationManager.Tick();
            MovementManager.Tick();
        }

        void FixedUpdate()
        {
            MovementManager.FixedTick();
        }

        void LateUpdate()
        {
            AnimationManager.LateTick();
        }

        public void Toggle()
        {
            _isSelected = !_isSelected;
            OnToggleValueChanged?.Invoke(_isSelected);
        }
    }

    public interface ISoldierController : IEntityController,ICanSelectableController
    {
        Transform Target { get; set; }
        Vector3 TargetPosition { get; set; }
        ISoldierAnimationService AnimationManager { get; }
        IMovementService MovementManager { get; }
    }

    public interface ICanSelectableController
    {
        bool IsSelected { get; }
        void Toggle();
        event System.Action<bool> OnToggleValueChanged;
    }
}