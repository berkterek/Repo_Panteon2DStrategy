namespace Panteon2DStrategy.Abstracts.StateMachineObjects
{
    public interface IStateMachine
    {
        void Tick();
        void FixedTick();
        void LateTick();
        void SetOrderedState(IState from, IState to, System.Func<bool> condition);
        void SetAnyState(IState to, System.Func<bool> condition);
    }
}