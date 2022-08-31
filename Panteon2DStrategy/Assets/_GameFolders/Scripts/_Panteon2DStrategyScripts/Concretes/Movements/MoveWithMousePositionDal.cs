using Panteon2DStrategy.Abstracts.Movements;
using Panteon2DStrategy.Enums;
using UnityEngine;

namespace Panteon2DStrategy.Managers
{
    public class MoveWithMousePositionDal : IMoverDal
    {
        readonly Transform _transform;
        Vector3 _direction;

        public MoveType Type => MoveType.MousePosition;

        public MoveWithMousePositionDal(Transform transform)
        {
            _transform = transform;
        }

        public void Tick(Vector2 direction)
        {
            _direction = new Vector3(direction.x, direction.y, -10f);
        }

        public void FixedTick()
        {
            //TODO This code speed value will be refactor
            _transform.position = Vector3.Lerp(_transform.position, _direction, Time.deltaTime * 1.1f);
        }
    }
}