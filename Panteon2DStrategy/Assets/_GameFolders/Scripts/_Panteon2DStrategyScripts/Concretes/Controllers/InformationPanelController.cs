using Panteon2DStrategy.Enums;
using UnityEngine;

namespace Panteon2DStrategy.Controllers
{
    public class InformationPanelController : MonoBehaviour
    {
        [SerializeField] InformationInspector[] _informationInspectors;
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