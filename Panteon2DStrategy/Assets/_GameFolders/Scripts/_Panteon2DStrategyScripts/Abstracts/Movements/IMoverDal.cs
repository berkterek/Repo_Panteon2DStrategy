using Panteon2DStrategy.Enums;
using UnityEngine;

namespace Panteon2DStrategy.Abstracts.Movements
{
    public interface IMoverDal
    {
        public MoveType Type { get; }
        void Tick(Vector2 direction);
        void FixedTick();
    }
}