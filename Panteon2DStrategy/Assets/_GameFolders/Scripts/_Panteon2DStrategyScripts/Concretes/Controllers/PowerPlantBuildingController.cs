using Panteon2DStrategy.Abstracts.Controllers;
using Panteon2DStrategy.ScriptableObjects;
using UnityEngine;

namespace Panteon2DStrategy.Controllers
{
    public class PowerPlantBuildingController : BaseTileBuildingController
    {
        [SerializeField] PowerPlantDataContainerSO _powerPlantDataContainer;

        protected override void OnValidate()
        {
            base.OnValidate();
            if (_powerPlantDataContainer == null) return;
            _area.size = _powerPlantDataContainer.Area.size;
        }

        protected override void Bind()
        {
            //TODO this bind code will refactor
            _worldCanvasController.Bind(_powerPlantDataContainer.Name);
        }
    }
}