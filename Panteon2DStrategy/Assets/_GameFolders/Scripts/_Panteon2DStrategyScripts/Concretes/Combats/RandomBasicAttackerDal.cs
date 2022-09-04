using Panteon2DStrategy.Abstracts.Combats;
using UnityEngine;

namespace Panteon2DStrategy.Combats
{
    public class RandomBasicAttackerDal : IAttackerDal
    {
        int _minDamage;
        int _maxDamage; 
        
        public void SetMinMaxDamage(int minDamage, int maxDamage)
        {
            _minDamage = minDamage;
            _maxDamage = maxDamage;
        }
        
        public int GetRandomDamage()
        {
            return Random.Range(_minDamage, _maxDamage);
        }
    }
}