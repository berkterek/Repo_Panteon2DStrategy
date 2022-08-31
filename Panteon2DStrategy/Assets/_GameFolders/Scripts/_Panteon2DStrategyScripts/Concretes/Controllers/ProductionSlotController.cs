using Panteon2DStrategy.Managers;
using Panteon2DStrategy.ScriptableObjects;
using Panteon2DStrategyScripts.Helpers;
using UnityEngine;
using UnityEngine.UI;

namespace Panteon2DStrategy.Controllers
{
    public class ProductionSlotController : MonoBehaviour
    {
        [SerializeField] ProductionDataContainerSO _productionDataContainer;
        [SerializeField] Image _iconImage;
        [SerializeField] Button _button;

        void Awake()
        {
            this.GetReference(ref _button);
        }

        void OnValidate()
        {
            this.GetReference(ref _button);
        }

        void Start()
        {
            _iconImage.sprite = _productionDataContainer.Icon;
        }

        void OnEnable()
        {
            _button.onClick.AddListener(HandleOnButtonClicked);
        }

        void OnDisable()
        {
            _button.onClick.RemoveListener(HandleOnButtonClicked);
        }
        
        void HandleOnButtonClicked()
        {
            GridBuildingManager.Instance.InitializeWithBuilding(_productionDataContainer.Prefab);
        }
    }    
}