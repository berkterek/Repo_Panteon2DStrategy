using Panteon2DStrategy.Abstracts.Movements;
using Panteon2DStrategy.Enums;
using UnityEngine;

namespace Panteon2DStrategy.Managers
{
    public class MoveWithDirectPositionDal : IMoverDal
    {
        readonly Transform _transform;
        
        Vector3 _direction;

        public MoveType Type => MoveType.DirectionPosition;

        public MoveWithDirectPositionDal(Transform transform)
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