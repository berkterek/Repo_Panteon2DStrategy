using Panteon2DStrategy.Abstracts.Helpers;
using Panteon2DStrategy.Abstracts.StateMachineObjects;
using Panteon2DStrategy.Enums;
using Panteon2DStrategy.Managers;
using Panteon2DStrategy.StateMachineObjects;
using Panteon2DStrategy.StateMachineObjects.ControlSystemStates;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Panteon2DStrategy.Systems
{
    public class ControlSystem : SingletonDestroyObject<ControlSystem>, IControlSystem
    {
        [SerializeField] LayerMask _layerMask;
        [SerializeField] PlayerType _currentPlayerType = PlayerType.PlayerA;

        StateMachine _stateMachine;

        public PlayerManagerInspector CurrentPlayerData { get; private set; }
        public LayerMask LayerMask => _layerMask;
        int _toggleIndex = 1;

        void Awake()
        {
            SetSingleton(this);
        }

        void Start()
        {
            CurrentPlayerData = PlayerManager.Instance.GetPlayer(_currentPlayerType);

            IState leftButtonDownState = new LeftButtonDownState(this);
            IState rightButtonDownState = new RightButtonDownState(this);

            _stateMachine = new StateMachine(leftButtonDownState);
            _stateMachine.SetOrderedState(leftButtonDownState,rightButtonDownState ,() => CurrentPlayerData.PlayerController.InputManager.IsRightButtonDown);
            _stateMachine.SetOrderedState(rightButtonDownState,leftButtonDownState, () => CurrentPlayerData.PlayerController.InputManager.IsLeftButtonDown);
        }

        void Update()
        {
            if (CurrentPlayerData == null) return;
            
            _stateMachine.Tick();
        }

        [Button]
        public void TogglePlayer()
        {
            _toggleIndex++;

            if (_toggleIndex > 2)
            {
                _toggleIndex = 1;
            }

            CurrentPlayerData = PlayerManager.Instance.GetPlayer((PlayerType)_toggleIndex);
            _currentPlayerType = CurrentPlayerData.PlayerType;
        }
    }

    public interface IControlSystem
    {
        PlayerManagerInspector CurrentPlayerData { get; }
        LayerMask LayerMask { get; }
    }
}