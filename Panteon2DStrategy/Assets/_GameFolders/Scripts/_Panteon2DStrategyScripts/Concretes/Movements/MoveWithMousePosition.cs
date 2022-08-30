using Panteon2DStrategy.Abstracts.Movements;
using Panteon2DStrategy.Enums;
using UnityEngine;

namespace Panteon2DStrategy.Managers
{
    public class MoveWithMousePosition : IMoverDal
    {
        readonly Transform _transform;
        Vector2 _direction;

        public MoveType Type => MoveType.MousePosition;

        public MoveWithMousePosition(Transform transform)
        {
            _transform = transform;
        }

        public void Tick(Vector2 direction)
        {
            _direction = direction;
        }

        public void FixedTick()
        {
            _transform.position = _direction;
        }
    }
}