
namespace Panteon2DStrategy.Abstracts.Movements
{
    public interface IMovementService
    {
        void Tick();
        void FixedTick();
        float Speed { get; }
    }
}