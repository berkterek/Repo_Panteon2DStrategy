using Panteon2DStrategy.Abstracts.Helpers;
using Panteon2DStrategy.Controllers;
using Panteon2DStrategy.Enums;
using Panteon2DStrategy.Managers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Panteon2DStrategy.Systems
{
    public class ControlSystem : SingletonDestroyObject<ControlSystem>
    {
        [SerializeField] LayerMask _layerMask;
        [SerializeField] PlayerType _currentPlayerType = PlayerType.PlayerA;

        PlayerManagerInspector _currentPlayerData;
        int _toggleIndex = 1;

        void Start()
        {
            _currentPlayerData = PlayerManager.Instance.GetPlayer(_currentPlayerType);
        }

        void Update()
        {
            if (_currentPlayerData == null) return;
            
            if (_currentPlayerData.PlayerController.InputManager.IsLeftButtonDown)
            {
                var worldPosition = _currentPlayerData.PlayerController.MainCamera.ScreenToWorldPoint(_currentPlayerData
                    .PlayerController.InputManager.MousePosition);

                var raycastHit = Physics2D.Raycast(_currentPlayerData.PlayerController.MainCamera.transform.position,
                    worldPosition, Mathf.Infinity, _layerMask);

                if (raycastHit.collider != null)
                {
                    if (raycastHit.collider.TryGetComponent(out ICanSelectableController selectableController))
                    {
                        if (selectableController.PlayerType == _currentPlayerType)
                        {
                            selectableController.Toggle();
                        } 
                    }    
                }
                else
                {
                    var soldiers = SoldierManager.Instance.GetSoldiers(_currentPlayerData.PlayerType);
                    foreach (var soldier in soldiers)
                    {
                        if (!soldier.IsSelected) continue;

                        soldier.Unselected();
                    }
                }
            }
            else if (_currentPlayerData.PlayerController.InputManager.IsRightButtonDown)
            {
                var worldPosition = _currentPlayerData.PlayerController.MainCamera.ScreenToWorldPoint(_currentPlayerData
                    .PlayerController.InputManager.MousePosition);
                
                var soldiers = SoldierManager.Instance.GetSoldiers(_currentPlayerData.PlayerType);

                foreach (var soldier in soldiers)
                {
                    if (!soldier.IsSelected) continue;

                    soldier.TargetPosition = worldPosition;
                }
            }
        }

        [Button]
        public void TogglePlayer()
        {
            _toggleIndex++;

            if (_toggleIndex > 2)
            {
                _toggleIndex = 1;
            }

            _currentPlayerData = PlayerManager.Instance.GetPlayer((PlayerType)_toggleIndex);
            _currentPlayerType = _currentPlayerData.PlayerType;
        }
    }
}