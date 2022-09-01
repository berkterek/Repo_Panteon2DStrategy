using Panteon2DStrategy.Managers;
using Panteon2DStrategy.ScriptableObjects;
using Panteon2DStrategyScripts.Helpers;
using UnityEngine;

namespace Panteon2DStrategy.Controllers
{
    public class TileBuildingController : MonoBehaviour
    {
        [SerializeField] Transform _transform;
        [SerializeField] BoundsInt _area;
        [SerializeField] bool _isPlaced = false;
        [SerializeField] WorldCanvasController _worldCanvasController;
        [SerializeField] ProductionDataContainerSO _productionDataContainer;

        public bool IsPlaced => _isPlaced;
        public BoundsInt Area
        {
            get => _area;
            set => _area = value;
        }

        public Transform Transform => _transform;

        void Awake()
        {
            this.GetReference(ref _transform);
        }

        void OnValidate()
        {
            this.GetReference(ref _transform);
        }

        public bool CanBePlaced(GridBuildingManager gridManager)
        {
            var area = GetArea(gridManager);

            return gridManager.CanTakeArea(area);
        }

        public void Place(GridBuildingManager gridManager)
        {
            var area = GetArea(gridManager);

            if (_productionDataContainer == null)
            {
                _productionDataContainer = gridManager.TempDataContainer;    
            }
            
            _worldCanvasController.gameObject.SetActive(true);
            _worldCanvasController.Bind(_productionDataContainer.Name);
            
            gridManager.TakeArea(area);
            
            //TODO set which player own this building
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
    }
}