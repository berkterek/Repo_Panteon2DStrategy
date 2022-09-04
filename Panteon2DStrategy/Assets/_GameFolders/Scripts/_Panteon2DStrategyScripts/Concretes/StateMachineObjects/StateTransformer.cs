using Panteon2DStrategy.Abstracts.StateMachineObjects;

namespace Panteon2DStrategy.StateMachineObjects
{
    public class StateTransformer
    {
        public IState From { get; }
        public IState To { get; }
        public System.Func<bool> Condition { get; }

        public StateTransformer(IState from, IState to, System.Func<bool> condition)
        {
            From = from;
            To = to;
            Condition = condition;
        }
    }
}