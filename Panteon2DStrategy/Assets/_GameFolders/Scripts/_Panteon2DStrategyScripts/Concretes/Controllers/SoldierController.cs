using System.Collections;
using Panteon2DStrategy.Abstracts.Animations;
using Panteon2DStrategy.Abstracts.Combats;
using Panteon2DStrategy.Abstracts.Controllers;
using Panteon2DStrategy.Abstracts.Movements;
using Panteon2DStrategy.Animations;
using Panteon2DStrategy.Combats;
using Panteon2DStrategy.Enums;
using Panteon2DStrategy.Managers;
using Panteon2DStrategy.Managers.Combats;
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
        [SerializeField] Collider2D _collider2D;
        [SerializeField] GameObject _parent;
 
        public Transform Transform => _transform;
        public Transform Target { get; set; }
        public bool IsSelected => _isSelected;
        public ISoldierStats Stats => _soldierStats;
        public ISoldierAnimationService AnimationManager { get; private set; }
        public IMovementService MovementManager { get; private set; }
        public Vector3 TargetPosition { get; set; }
        public PlayerType PlayerType => _playerType;
        public IHealthService HealthManager { get; private set; }
        public IAttackerService AttackerManager { get; private set; }
        public GameObject Parent => _parent;

        public event System.Action<bool> OnToggleValueChanged;

        void Awake()
        {
            this.GetReference(ref _transform);
            this.GetReference(ref _collider2D);
            AnimationManager = new SoldierAnimationAiPathManager(this, new SoldierAnimatorDal(this));
            MovementManager = new SoldierMovementAiPathManager(this, new MoveWithDirectPositionDal(_destinationTarget));
            HealthManager = new SoldierHealthManager(this, new BasicHealthDal());
            AttackerManager = new SoldierAttackerManager(this, new RandomBasicAttackerDal());
        }

        void OnValidate()
        {
            this.GetReference(ref _transform);
            this.GetReference(ref _collider2D);
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

        void OnEnable()
        {
            HealthManager.OnDead += Dying;
        }

        void OnDisable()
        {
            HealthManager.OnDead -= Dying;
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

        void OnTriggerStay2D(Collider2D other)
        {
            if (other.TryGetComponent(out IHealthController healthController))
            {
                if (healthController.PlayerType == _playerType) return;
                
                AttackerManager.Attack(healthController);
                AnimationManager.IsAttacking(true);
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            AnimationManager.IsAttacking(false);
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

        public void SetDestination(Vector3 position)
        {
            _destinationTarget.position = position;
        }

        public void SetPlayer(PlayerType playerType)
        {
            _playerType = playerType;
        }

        public void Dying()
        {
            StartCoroutine(DyingProcessAsync());
        }

        public IEnumerator DyingProcessAsync()
        {
            _collider2D.enabled = false;
            AnimationManager.Dying();
            yield return new WaitForSeconds(5f);
            SoldierManager.Instance.RemoveThisSoldier(this);
        }
    }
}