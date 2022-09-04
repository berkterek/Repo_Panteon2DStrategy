using Panteon2DStrategyScripts.Helpers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Panteon2DStrategy.Controllers
{
    public class BuildingAndSoldierInfoPanelController : MonoBehaviour
    {
        [SerializeField] Image _image;
        [SerializeField] TMP_Text _text;

        void Awake()
        {
            this.GetReference(ref _image);
            this.GetReference(ref _text);
        }

        void OnValidate()
        {
            this.GetReference(ref _image);
            this.GetReference(ref _text);
        }
    }
}