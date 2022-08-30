using Panteon2DStrategy.Abstracts.Movements;
using Panteon2DStrategy.Enums;
using UnityEngine;

namespace Panteon2DStrategy.Movements
{
    public class MoveWithTransformDal : IMoverDal
    {
        readonly Transform _transform;
        Vector2 _direction;

        public MoveType Type => MoveType.Transform;

        public MoveWithTransformDal(Transform transform)
        {
            _transform = transform;
        }

        public void Tick(Vector2 direction)
        {
            _direction = direction;
        }

        public void FixedTick()
        {
            _transform.Translate(_direction);
        }
    }
}