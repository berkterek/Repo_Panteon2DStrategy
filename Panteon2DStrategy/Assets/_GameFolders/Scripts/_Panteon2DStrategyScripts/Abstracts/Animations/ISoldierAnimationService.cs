namespace Panteon2DStrategy.Abstracts.Animations
{
    public interface ISoldierAnimationService
    {
        void Tick();
        void LateTick();
        void Dying();
        void IsAttacking(bool value);
    }
}