using Panteon2DStrategy.Controllers;
using Panteon2DStrategy.Enums;
using Panteon2DStrategy.Helpers;
using Panteon2DStrategy.Managers;
using Panteon2DStrategyScripts.Helpers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Panteon2DStrategy.Abstracts.Controllers
{
    public abstract class BaseTileBuildingController : MonoBehaviour,ICanSelectableController
    {
        [BoxGroup("Base Info")]
        [SerializeField] PlayerType _playerType = PlayerType.NotSelected;
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
        
        public PlayerType PlayerType => _playerType;
        public bool IsSelected => _isSelected;
        public bool IsPlaced => _isPlaced;

        public Transform Transform => _transform;
        public BoundsInt Area => _area;
        
        public event System.Action<bool> OnToggleValueChanged;

        protected virtual void Awake()
        {
            this.GetReference(ref _transform);
            this.GetReference(ref _worldCanvasController);
        }

        protected virtual void OnValidate()
        {
            this.GetReference(ref _transform);
            this.GetReference(ref _worldCanvasController);
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
        }

        public void Unselected()
        {
            _isSelected = false;
            OnToggleValueChanged?.Invoke(_isSelected);
        }

        private void SetAreaSize()
        {
            if (_area.size != DirectionCacheHelper.Vector3IntZero)
            {
                SetSize();
            }
        }

        protected abstract void SetSize();

        protected abstract void Bind();
    }
}