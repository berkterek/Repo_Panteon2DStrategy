using Panteon2DStrategy.Abstracts.Movements;
using Panteon2DStrategy.Controllers;
using Pathfinding;
using UnityEngine;

namespace Panteon2DStrategy.Managers.Movements
{
    public class SoldierMovementAiPathManager : IMovementService
    {
        readonly IMoverDal _moverDal;
        readonly ISoldierController _soldierController;
        readonly AIPath _aiPath;
        readonly float _stopDistance = 0f;
        
        Vector3 _lastPosition;
        bool _isSetOneTime = false;
        
        public float Speed { get; }

        public SoldierMovementAiPathManager(ISoldierController soldierController, IMoverDal moverDal)
        {
            _moverDal = moverDal;
            _soldierController = soldierController;
            _aiPath = soldierController.Transform.GetComponent<AIPath>();
            _aiPath.speed = soldierController.Stats.MoveSpeed;
            _stopDistance = soldierController.Stats.StopDistance;
        }
        
        public void Tick()
        {
            if (!_soldierController.IsSelected) return;

            _aiPath.isStopped = _aiPath.remainingDistance < _stopDistance;

            if (_lastPosition == _soldierController.TargetPosition) return;
            _lastPosition = _soldierController.TargetPosition;

            _moverDal.Tick(_lastPosition);
            _isSetOneTime = true;
        }

        public void FixedTick()
        {
            if (!_isSetOneTime) return;
            
            _moverDal.FixedTick();

            _isSetOneTime = false;
        }
    }
}