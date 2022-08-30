using Panteon2DStrategy.Abstracts.Controllers;
using UnityEngine;
using System.Collections.Generic;
using Panteon2DStrategy.Abstracts.Movements;
using Panteon2DStrategy.Enums;
using Panteon2DStrategy.Helpers;
using Panteon2DStrategy.ViewModels;

namespace Panteon2DStrategy.Managers
{
    public class PlayerMovementManager : IMovementService
    {
        readonly IPlayerController _playerController;
        readonly Dictionary<MoveType, IMoverDal> _moveDalDictionary;

        //TODO this code will refactor
        public float Speed => Time.deltaTime * 50f;

        IMoverDal _currentDal;

        public PlayerMovementManager(PlayerMovementViewModel viewModel)
        {
            _playerController = viewModel.PlayerController;
            _moveDalDictionary = new Dictionary<MoveType, IMoverDal>();

            foreach (IMoverDal moverDal in viewModel.MoverDalArray)
            {
                _moveDalDictionary.Add(moverDal.Type, moverDal);
            }
        }

        public void Tick()
        {
            Vector2 keyboardDirection = _playerController.InputManager.KeyboardDirection;
            if (keyboardDirection != CacheHelper.Zero)
            {
                _currentDal = _moveDalDictionary[MoveType.Transform];
                _currentDal.Tick(Speed * keyboardDirection);
                return;
            }

            Vector2 mousePosition = _playerController.InputManager.MousePosition;
            Vector2 worldPosition = _playerController.MainCamera.ScreenToWorldPoint(mousePosition);
            _currentDal = _moveDalDictionary[MoveType.MousePosition];
            _currentDal.Tick(worldPosition);
        }

        public void FixedTick()
        {
            if (_currentDal == null) return;
            _currentDal.FixedTick();
        }
    }
}