using Panteon2DStrategy.Abstracts.Combats;
using Panteon2DStrategy.Abstracts.Controllers;
using Panteon2DStrategy.Helpers;
using Panteon2DStrategy.Managers;
using Panteon2DStrategy.Managers.Combats;
using Panteon2DStrategy.ScriptableObjects;
using Panteon2DStrategy.ViewModels;
using Panteon2DStrategyScripts.Helpers;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Panteon2DStrategy.Controllers
{
    public class SoldierBuilding : BaseTileBuildingController
    {
        [BoxGroup("Soldier Info")] [SerializeField]
        SoldierBuildingDataContainerSO _buildingDataContainer;

        [BoxGroup("Soldier Info")] [SerializeField]
        Transform _firstTarget;

        [BoxGroup("Soldier Info")] [SerializeField]
        Button _createButton;
        
        public IHealthService HealthManager { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            this.GetReference(ref _createButton);
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            this.GetReference(ref _createButton);
        }

        protected override void OnEnable()
        {
            _createButton.onClick.AddListener(HandleOnButtonClicked);
        }

        protected override void OnDisable()
        {
            _createButton.onClick.RemoveListener(HandleOnButtonClicked);
        }

        protected override void SetSize()
        {
            _area.size = _buildingDataContainer.Area.size;
        }

        protected override void Bind()
        {
            //TODO this bind code will refactor
            _worldCanvasController.Bind(_buildingDataContainer.Name, this);
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

        void HandleOnButtonClicked()
        {
            var soldierObject = Instantiate(_buildingDataContainer.SoldierDataContainer.Prefab, Transform.position,
                DirectionCacheHelper.Identity);

            var soldierController = soldierObject.GetComponentInChildren<ISoldierController>();
            soldierController.SetDestination(_firstTarget.position);
            soldierController.SetPlayer(_playerType);
            SoldierManager.Instance.SetSoldierToPlayer(soldierController);
        }
    }
}