using Panteon2DStrategy.Abstracts.Helpers;
using Panteon2DStrategy.Enums;
using Panteon2DStrategy.Managers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Panteon2DStrategy.Systems
{
    public class ControlSystem : SingletonDestroyObject<ControlSystem>
    {
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
                var soldiers = SoldierManager.Instance.GetSoldiers(_currentPlayerData.PlayerType);

                foreach (var soldier in soldiers)
                {
                    if (!soldier.IsSelected) continue;

                    var worldPosition =
                        _currentPlayerData.PlayerController.MainCamera.ScreenToWorldPoint(_currentPlayerData.PlayerController
                            .InputManager.MousePosition);
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