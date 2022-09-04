using System;
using Panteon2DStrategyScripts.Helpers;
using UnityEngine;

namespace Panteon2DStrategy.Controllers
{
    public class SelectionSpriteController : MonoBehaviour
    {
        [SerializeField] SpriteRenderer _spriteRenderer;
        
        ICanSelectableController _canSelectableController;
        
        void Awake()
        {
            _canSelectableController = GetComponentInParent<ICanSelectableController>();
            this.GetReference(ref _spriteRenderer);
        }

        void Start()
        {
            _spriteRenderer.enabled = false;
        }

        void OnValidate()
        {
            this.GetReference(ref _spriteRenderer);
        }

        void OnEnable()
        {
            _canSelectableController.OnToggleValueChanged += HandleOnToggleValueChanged;
        }

        void OnDisable()
        {
            _canSelectableController.OnToggleValueChanged -= HandleOnToggleValueChanged;
        }
        
        void HandleOnToggleValueChanged(bool value)
        {
            _spriteRenderer.enabled = value;
        }
    }    
}

