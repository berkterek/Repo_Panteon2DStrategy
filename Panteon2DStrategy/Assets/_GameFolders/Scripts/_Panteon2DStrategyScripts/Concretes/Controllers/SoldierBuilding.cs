using Panteon2DStrategy.Abstracts.Controllers;
using Panteon2DStrategy.ScriptableObjects;
using Panteon2DStrategy.ViewModels;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Panteon2DStrategy.Controllers
{
    public class SoldierBuilding : BaseTileBuildingController
    {
        [BoxGroup("Soldier Info")] [SerializeField]
        SoldierBuildingDataContainerSO _buildingDataContainer;

        protected override void SetSize()
        {
            _area.size = _buildingDataContainer.Area.size;
        }

        protected override void Bind()
        {
            //TODO this bind code will refactor
            _worldCanvasController.Bind(_buildingDataContainer.Name);
        }

        protected override void InvokeEvent()
        {
            InfoViewModel model = new InfoViewModel
            {
                Building = _buildingDataContainer,
                Soldier = _buildingDataContainer.SoldierDataContainer
            };
            
            _gameEvent.InvokeEventsWithObject(model);
        }
    }
}