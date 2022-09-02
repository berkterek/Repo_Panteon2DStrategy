using Panteon2DStrategy.Abstracts.Movements;
using Panteon2DStrategy.Controllers;
using UnityEngine;

namespace Panteon2DStrategy.Managers.Movements
{
    public class SoldierMovementManager : IMovementService
    {
        readonly IMoverDal _moverDal;
        readonly ISoldierController _soldierController;
        
        Vector3 _lastPosition;
        bool _isSetOneTime = false;
        
        public float Speed { get; }

        public SoldierMovementManager(ISoldierController soldierController, IMoverDal moverDal)
        {
            _moverDal = moverDal;
            _soldierController = soldierController;
        }
        
        public void Tick()
        {
            if (!_soldierController.IsSelected) return;

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