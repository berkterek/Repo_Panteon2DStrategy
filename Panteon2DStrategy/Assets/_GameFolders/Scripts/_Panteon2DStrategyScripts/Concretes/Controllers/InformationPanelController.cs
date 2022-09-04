using System.Linq;
using Panteon2DStrategy.Abstracts.ScriptableObjects;
using Panteon2DStrategy.Enums;
using Panteon2DStrategy.ScriptableObjects.GameEventListeners;
using Panteon2DStrategy.ViewModels;
using Panteon2DStrategyScripts.Helpers;
using UnityEngine;

namespace Panteon2DStrategy.Controllers
{
    public class InformationPanelController : MonoBehaviour
    {
        [SerializeField] NormalGameEventListener _gameEventListener;
        [SerializeField] InformationInspector[] _informationInspectors;

        void Awake()
        {
            this.GetReference(ref _gameEventListener);
        }

        void OnValidate()
        {
            this.GetReference(ref _gameEventListener);
        }

        void OnEnable()
        {
            _gameEventListener.ParameterEventWithObject += HandleOnEventTriggered;
        }

        void OnDisable()
        {
            _gameEventListener.ParameterEventWithObject -= HandleOnEventTriggered;
        }
        
        void HandleOnEventTriggered(object value)
        {
            InfoViewModel model = (InfoViewModel)value;
            SetActiveOrDeactivate(model.Building, InfoType.Build, () => model.Building != null);
            SetActiveOrDeactivate(model.Soldier, InfoType.Soldier, () => model.Soldier != null);
        }

        private void SetActiveOrDeactivate(BaseSelectDataContainerSO selectionDataContainer, InfoType infoType ,System.Func<bool> condition)
        {
            if (condition.Invoke())
            {
                var infoPanel = _informationInspectors.FirstOrDefault(x => x.InfoType == infoType);
                var buildingInfo = selectionDataContainer;
                infoPanel.InfoPanel.Bind(buildingInfo.Icon,buildingInfo.Name);
                infoPanel.InfoPanel.gameObject.SetActive(true);
            }
            else
            {
                var infoPanel = _informationInspectors.FirstOrDefault(x => x.InfoType ==infoType);
                infoPanel.InfoPanel.gameObject.SetActive(false);
            }
        }
    }

    [System.Serializable]
    public struct InformationInspector
    {
        [SerializeField] InfoType _infoType;
        [SerializeField] BuildingAndSoldierInfoPanelController _infoPanel;

        public InfoType InfoType => _infoType;
        public BuildingAndSoldierInfoPanelController InfoPanel => _infoPanel;
    }
}