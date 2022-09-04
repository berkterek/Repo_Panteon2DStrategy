using System.Collections.Generic;
using Panteon2DStrategy.Abstracts.StateMachineObjects;

namespace Panteon2DStrategy.StateMachineObjects
{
    public class StateMachine : IStateMachine
    {
        readonly List<StateTransformer> _orderedStates;
        readonly List<StateTransformer> _anyStates;

        IState _currentState;

        public StateMachine(IState firstState)
        {
            _orderedStates = new List<StateTransformer>();
            _anyStates = new List<StateTransformer>();
            SetState(firstState);
        }
        
        public void Tick()
        {
            if (_currentState == null) return;
            
            CheckCurrentStateIsChange();
            
            _currentState.Tick();
        }

        public void FixedTick()
        {
            if (_currentState == null) return;
            
            _currentState.FixedTick();
        }
        
        public void LateTick()
        {
            if (_currentState == null) return;
            
            _currentState.LateTick();
        }
        
        void CheckCurrentStateIsChange()
        {
            foreach (var anyState in _anyStates)
            {
                if (anyState.Condition.Invoke())
                {
                    SetState(anyState.To);
                    return;
                }
            }
            
            foreach (StateTransformer orderedState in _orderedStates)
            {
                if (orderedState.From == _currentState && orderedState.Condition.Invoke())
                {
                    SetState(orderedState.To);
                    break;
                }
            }
        }

        void SetState(IState state)
        {
            if (_currentState != null)
            {
                _currentState.EndState();
            }

            _currentState = state;
            _currentState.StartState();
        }

        public void SetOrderedState(IState from, IState to, System.Func<bool> condition)
        {
            _orderedStates.Add(new StateTransformer(from,to,condition));
        }

        public void SetAnyState(IState to, System.Func<bool> condition)
        {
            _anyStates.Add(new StateTransformer(null,to,condition));
        }
    }
}