using Panteon2DStrategyScripts.Helpers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Panteon2DStrategy.Controllers
{
    public class WorldCanvasController : MonoBehaviour
    {
        [SerializeField] Canvas _canvas;
        [SerializeField] TMP_Text _nameText;
        [SerializeField] Image _healthImage;
        
        void Awake()
        {
            this.GetReference(ref _canvas);
        }

        void OnValidate()
        {
            this.GetReference(ref _canvas);
        }

        void Start()
        {
            _canvas.worldCamera = Camera.main;
        }

        public void Bind(string name)
        {
            _nameText.SetText(name);
        }
    }    
}

