using Panteon2DStrategy.Abstracts.StateMachineObjects;
using Panteon2DStrategy.Managers;
using Panteon2DStrategy.Systems;
using UnityEngine;

namespace Panteon2DStrategy.StateMachineObjects.ControlSystemStates
{
    public class RightButtonDownState : IState
    {
        readonly IControlSystem _controlSystem;
        bool _isButtonDown;

        public RightButtonDownState(IControlSystem controlSystem)
        {
            _controlSystem = controlSystem;
        }
        
        public void StartState()
        {
            _isButtonDown = true;
            Debug.Log($"{nameof(RightButtonDownState) }{nameof(StartState)}");
        }

        public void Tick()
        {
            if (_isButtonDown)
            {
                var worldPosition = _controlSystem.CurrentPlayerData.PlayerController.MainCamera.ScreenToWorldPoint(_controlSystem.CurrentPlayerData
                    .PlayerController.InputManager.MousePosition);
                
                var soldiers = SoldierManager.Instance.GetSoldiers(_controlSystem.CurrentPlayerData.PlayerType);

                foreach (var soldier in soldiers)
                {
                    if (!soldier.IsSelected) continue;

                    soldier.TargetPosition = worldPosition;
                }

                _isButtonDown = false;
            }

            _isButtonDown = _controlSystem.CurrentPlayerData.PlayerController.InputManager.IsRightButtonDown;
        }

        public void FixedTick()
        {
        }

        public void LateTick()
        {
        }
        
        public void EndState()
        {
            _isButtonDown = false;
            Debug.Log($"{nameof(RightButtonDownState) }{nameof(EndState)}");
        }
    }
}