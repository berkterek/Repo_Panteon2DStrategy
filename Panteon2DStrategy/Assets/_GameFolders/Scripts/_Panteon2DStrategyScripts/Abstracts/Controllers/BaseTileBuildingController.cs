using System.Collections;
using Panteon2DStrategy.Abstracts.Combats;
using Panteon2DStrategy.Combats;
using Panteon2DStrategy.Controllers;
using Panteon2DStrategy.Enums;
using Panteon2DStrategy.Helpers;
using Panteon2DStrategy.Managers;
using Panteon2DStrategy.Managers.Combats;
using Panteon2DStrategy.ScriptableObjects;
using Panteon2DStrategyScripts.Helpers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Panteon2DStrategy.Abstracts.Controllers
{
    public abstract class BaseTileBuildingController : MonoBehaviour,IBaseTileBuildingController
    {
        [BoxGroup("Game Event")] [SerializeField]
        protected GameEvent _gameEvent;
        [BoxGroup("Base Info")]
        [SerializeField] protected PlayerType _playerType = PlayerType.NotSelected;
        [BoxGroup("Base Info")]
        [SerializeField] Transform _transform;
        [BoxGroup("Base Info")]
        [SerializeField] protected BoundsInt _area;
        [BoxGroup("Base Info")]
        [SerializeField] bool _isPlaced = false;
        [BoxGroup("Base Info")]
        [SerializeField] bool _isSelected;
        [BoxGroup("Base Info")]
        [SerializeField] protected WorldCanvasController _worldCanvasController;
        [BoxGroup("Base Info")]
        [SerializeField] Collider2D _collider2D;

        [BoxGroup("Base Info")] [SerializeField]
        BuildingStatsSO _stats;

        public IHealthService HealthManager { get; private set; }
        public PlayerType PlayerType => _playerType;
        public bool IsSelected => _isSelected;
        public bool IsPlaced => _isPlaced;
        public IBuildingStats Stats => _stats;

        public Transform Transform => _transform;
        public BoundsInt Area => _area;
        
        public event System.Action<bool> OnToggleValueChanged;

        protected virtual void Awake()
        {
            this.GetReference(ref _transform);
            this.GetReference(ref _worldCanvasController);
            this.GetReference(ref _collider2D);
            SetAreaSize();
            HealthManager = new BuildingHealthManager(this, new BasicHealthDal());
        }

        protected virtual void OnValidate()
        {
            this.GetReference(ref _transform);
            this.GetReference(ref _worldCanvasController);
            this.GetReference(ref _collider2D);
            SetAreaSize();
        }

        protected virtual void OnEnable()
        {
            HealthManager.OnDead += Dying;
        }

        protected virtual void OnDisable()
        {
            HealthManager.OnDead -= Dying;
        }

        public bool CanBePlaced(GridBuildingManager gridManager)
        {
            var area = GetArea(gridManager);

            return gridManager.CanTakeArea(area);
        }

        public void Place(GridBuildingManager gridManager)
        {
            var area = GetArea(gridManager);

            _worldCanvasController.gameObject.SetActive(true);
            
            Bind();

            gridManager.TakeArea(area);
            _isPlaced = true;

            if (_playerType != PlayerType.NotSelected) return;
            
            BuildingManager.Instance.SetBuildingToPlayer(this);
        }

        private BoundsInt GetArea(GridBuildingManager gridManager)
        {
            Vector3Int positionInt = gridManager.GridLayout.LocalToCell(_transform.position);
            BoundsInt areaTemp = _area;
            areaTemp.position = positionInt;

            return areaTemp;
        }

        public void SetAreaPosition(Vector3Int worldToCell)
        {
            _area.position = worldToCell;
        }
        
        public void Toggle()
        {
            _isSelected = !_isSelected;
            OnToggleValueChanged?.Invoke(_isSelected);

            if (_isSelected)
            {
                InvokeEvent();
            }
        }

        public void Unselected()
        {
            _isSelected = false;
            OnToggleValueChanged?.Invoke(_isSelected);
        }

        private void SetAreaSize()
        {
            if (_area.size == DirectionCacheHelper.Vector3IntZero)
            {
                SetSize();
            }
        }
        
        public void SetPlayer(PlayerType playerType)
        {
            _playerType = playerType;
        }
        
        private void Dying()
        {
            _collider2D.enabled = false;
            BuildingManager.Instance.RemoveThisBuilding(this);
        }

        protected abstract void SetSize();

        protected abstract void Bind();
        
        protected abstract void InvokeEvent();
    }
}