using Panteon2DStrategy.Abstracts.Controllers;
using Panteon2DStrategy.Abstracts.StateMachineObjects;
using Panteon2DStrategy.Managers;
using Panteon2DStrategy.Systems;
using UnityEngine;

namespace Panteon2DStrategy.StateMachineObjects.ControlSystemStates
{
    public class LeftButtonDownState : IState
    {
        readonly IControlSystem _controlSystem;
        bool _isButtonDown;
        
        public LeftButtonDownState(IControlSystem controlSystem)
        {
            _controlSystem = controlSystem;
        }
        
        public void StartState()
        {
            _isButtonDown = true;
            Debug.Log($"{nameof(LeftButtonDownState) }{nameof(StartState)}");
        }

        public void Tick()
        {
            if (_isButtonDown)
            {
                var mousePosition = _controlSystem.CurrentPlayerData.PlayerController.InputManager.MousePosition;
                var worldPosition = _controlSystem.CurrentPlayerData.PlayerController.MainCamera.ScreenToWorldPoint(mousePosition);

                var raycastHit = Physics2D.Raycast(_controlSystem.CurrentPlayerData.PlayerController.MainCamera.transform.position,
                    worldPosition, Mathf.Infinity, _controlSystem.LayerMask);

                if (raycastHit.collider != null)
                {
                    if (raycastHit.collider.TryGetComponent(out ICanSelectableController selectableController))
                    {
                        if (selectableController.PlayerType == _controlSystem.CurrentPlayerData.PlayerType)
                        {
                            selectableController.Toggle();
                        } 
                    }    
                }
                else
                {
                    var soldiers = SoldierManager.Instance.GetSoldiers(_controlSystem.CurrentPlayerData.PlayerType);
                    foreach (var soldier in soldiers)
                    {
                        if (!soldier.IsSelected) continue;

                        soldier.Unselected();
                    }

                    var buildings = BuildingManager.Instance.GetBuildings(_controlSystem.CurrentPlayerData.PlayerType);
                    foreach (var building in buildings)
                    {
                        if (!building.IsSelected) continue;
                        building.Unselected();
                    }
                }

                _isButtonDown = false;
            }

            _isButtonDown = _controlSystem.CurrentPlayerData.PlayerController.InputManager.IsLeftButtonDown;
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
            Debug.Log($"{nameof(LeftButtonDownState) }{nameof(EndState)}");
        }
    }
}