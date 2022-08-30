using Panteon2DStrategy.Managers;
using Panteon2DStrategyScripts.Helpers;
using UnityEngine;

namespace Panteon2DStrategy.Controllers
{
    public class TileBuildingController : MonoBehaviour
    {
        [SerializeField] Transform _transform;
        [SerializeField] BoundsInt _area;
        [SerializeField] bool _isPlaced = false;

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
            
            gridManager.TakeArea(area);
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