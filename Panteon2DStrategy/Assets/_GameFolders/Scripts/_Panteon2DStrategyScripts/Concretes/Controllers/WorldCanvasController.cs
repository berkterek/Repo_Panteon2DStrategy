using System;
using Panteon2DStrategy.Abstracts.Controllers;
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

        IBaseTileBuildingController _baseTileBuildingController;
        
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

        void OnDisable()
        {
            _baseTileBuildingController.HealthManager.OnTookDamage -= HandleOnTookDamage;
        }

        public void Bind(string name, IBaseTileBuildingController baseTileBuildingController)
        {
            _nameText.SetText(name);
            _baseTileBuildingController = baseTileBuildingController;
            _baseTileBuildingController.HealthManager.OnTookDamage += HandleOnTookDamage;
        }

        void HandleOnTookDamage(int maxHealth, int currentHealth)
        {
            _healthImage.fillAmount = (float)currentHealth / (float)maxHealth;
        }
    }    
}

