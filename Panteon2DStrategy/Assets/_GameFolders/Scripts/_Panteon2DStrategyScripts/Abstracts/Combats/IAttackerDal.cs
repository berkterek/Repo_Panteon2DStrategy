namespace Panteon2DStrategy.Abstracts.Combats
{
    public interface IAttackerDal
    {
        int GetRandomDamage();
        void SetMinMaxDamage(int minDamage, int maxDamage);
    }
}