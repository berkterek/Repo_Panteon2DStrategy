using Panteon2DStrategy.Abstracts.Controllers;
using UnityEngine;
using System.Collections.Generic;
using Panteon2DStrategy.Helpers;

namespace Panteon2DStrategy.Managers
{
    public class PlayerMovementManager : IMovementService
    {
        readonly IPlayerController _playerController;
        readonly Dictionary<MoveType, IMoverDal> _moveDals;

        public float Speed => Time.deltaTime * 50f;

        IMoverDal _currentDal;

        public PlayerMovementManager(PlayerMovementViewModel viewModel)
        {
            _playerController = viewModel.PlayerController;
            _moveDals = new Dictionary<MoveType, IMoverDal>();

            foreach (IMoverDal moverDal in viewModel.MoverDals)
            {
                _moveDals.Add(moverDal.Type, moverDal);
            }
        }

        public void Tick()
        {
            Vector2 keyboardDirection = _playerController.InputManager.KeyboardDirection;
            if (keyboardDirection != CacheHelper.Zero)
            {
                _currentDal = _moveDals[MoveType.Transform];
                _currentDal.Tick(Speed * keyboardDirection);
                return;
            }

            Vector2 mousePosition = _playerController.InputManager.MousePosition;
            Vector2 worldPosition = _playerController.MainCamera.ScreenToWorldPoint(mousePosition);
            _currentDal = _moveDals[MoveType.MousePosition];
            _currentDal.Tick(worldPosition);
        }

        public void FixedTick()
        {
            if (_currentDal == null) return;
            _currentDal.FixedTick();
        }
    }

    public interface IMovementService
    {
        void Tick();
        void FixedTick();
        float Speed { get; }
    }

    public interface IMoverDal
    {
        public MoveType Type { get; }
        void Tick(Vector2 direction);
        void FixedTick();
    }

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
            _transform.position = Vector2.MoveTowards(_transform.position, _direction, Time.deltaTime*50f);
        }
    }

    public struct PlayerMovementViewModel
    {
        public IPlayerController PlayerController { get; set; }
        public IMoverDal[] MoverDals { get; set; }
    }

    public enum MoveType : byte
    {
        Transform,
        MousePosition
    }
}