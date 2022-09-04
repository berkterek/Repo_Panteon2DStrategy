using System.Collections;
using Panteon2DStrategy.Abstracts.Animations;
using Panteon2DStrategy.Abstracts.Controllers;
using Panteon2DStrategy.Abstracts.Movements;
using Panteon2DStrategy.Animations;
using Panteon2DStrategy.Enums;
using Panteon2DStrategy.Managers;
using Panteon2DStrategy.Managers.Movements;
using Panteon2DStrategy.ScriptableObjects;
using Panteon2DStrategy.ViewModels;
using Panteon2DStrategyScripts.Helpers;
using UnityEngine;

namespace Panteon2DStrategy.Controllers
{
    public class SoldierController : MonoBehaviour,ISoldierController
    {
        [SerializeField] GameEvent _gameEvent;
        [SerializeField] SoldierDataContainerSO _soldierDataContainer;
        [SerializeField] SoldierStatsSO _soldierStats;
        [SerializeField] PlayerType _playerType;
        [SerializeField] Transform _transform;
        [SerializeField] Transform _destinationTarget;
        [SerializeField] bool _isSelected = false;
 
        public Transform Transform => _transform;
        public Transform Target { get; set; }
        public bool IsSelected => _isSelected;
        public ISoldierStats Stats => _soldierStats;
        public ISoldierAnimationService AnimationManager { get; private set; }
        public IMovementService MovementManager { get; private set; }
        public Vector3 TargetPosition { get; set; }
        public PlayerType PlayerType => _playerType;

        public event System.Action<bool> OnToggleValueChanged;

        void Awake()
        {
            this.GetReference(ref _transform);
            AnimationManager = new SoldierAnimationAiPathManager(this, new SoldierAnimatorDal(this));
            MovementManager = new SoldierMovementAiPathManager(this, new MoveWithDirectPositionDal(_destinationTarget));
        }

        void OnValidate()
        {
            this.GetReference(ref _transform);
        }

        IEnumerator Start()
        {
            if(_playerType != PlayerType.NotSelected) yield break;

            while (SoldierManager.Instance == null)
            {
                yield return null;
            }
            
            SoldierManager.Instance.SetSoldierToPlayer(this);
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

            if (_isSelected)
            {
                InfoViewModel model = new InfoViewModel
                {
                    Soldier = _soldierDataContainer
                };
                
                _gameEvent.InvokeEventsWithObject(model);
            }
        }
        
        public void Unselected()
        {
            _isSelected = false;
            OnToggleValueChanged?.Invoke(_isSelected);
        }
    }
}