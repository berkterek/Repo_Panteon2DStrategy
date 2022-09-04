namespace Panteon2DStrategy.Abstracts.StateMachineObjects
{
    public interface IState
    {
        void Tick();
        void FixedTick();
        void LateTick();
        void EndState();
        void StartState();
    }
}