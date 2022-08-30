using NUnit.Framework;
using Panteon2DStrategy.Managers;
using NSubstitute;
using Panteon2DStrategy.Abstracts.Controllers;
using Panteon2DStrategy.Abstracts.Inputs;
using Panteon2DStrategy.Abstracts.Movements;
using Panteon2DStrategy.Enums;
using Panteon2DStrategy.Helpers;
using Panteon2DStrategy.ViewModels;
using UnityEngine;

namespace Movements
{
    public class player_movement_manager_edit_mode
    {
        PlayerMovementViewModel _viewModel;

        [SetUp]
        public void Setup()
        {
            _viewModel = new PlayerMovementViewModel
            {
                MoverDalArray = new IMoverDal[] { Substitute.For<IMoverDal>(), Substitute.For<IMoverDal>() },
                PlayerController = Substitute.For<IPlayerController>()
            };

            _viewModel.MoverDalArray[0].Type.Returns(MoveType.Transform);
            _viewModel.MoverDalArray[1].Type.Returns(MoveType.MousePosition);
            GameObject gameObject = new GameObject();
            var camera = gameObject.AddComponent<Camera>();
            _viewModel.PlayerController.MainCamera.Returns(camera);
            _viewModel.PlayerController.InputManager.Returns(Substitute.For<IInputService>());
        }

        [Test]
        public void movement_manager_can_tick_movement_dal_transform_move_tick()
        {
            //Arrange
            IMovementService movementManager = new PlayerMovementManager(_viewModel);

            //Act
            _viewModel.PlayerController.InputManager.KeyboardDirection.Returns(CacheHelper.Left);
            movementManager.Tick();

            //Assert
            _viewModel.MoverDalArray[0].Received().Tick(movementManager.Speed * CacheHelper.Left);
        }

        [Test]
        public void movement_manager_can_tick_movement_dal_mouse_move_tick()
        {
            //Arrange
            IMovementService movementManager = new PlayerMovementManager(_viewModel);

            //Act
            _viewModel.PlayerController.InputManager.KeyboardDirection.Returns(CacheHelper.Zero);
            movementManager.Tick();

            //Assert
            _viewModel.MoverDalArray[1].Received().Tick(CacheHelper.Zero);
        }
        
        [Test]
        public void movement_manager_can_fixed_tick_movement_dal_transform_move_tick()
        {
            //Arrange
            IMovementService movementManager = new PlayerMovementManager(_viewModel);

            //Act
            _viewModel.PlayerController.InputManager.KeyboardDirection.Returns(CacheHelper.Left);
            movementManager.Tick();
            movementManager.FixedTick();

            //Assert
            _viewModel.MoverDalArray[0].Received().FixedTick();
        }
        
        [Test]
        public void movement_manager_can_fixed_tick_movement_dal_mouse_move_tick()
        {
            //Arrange
            IMovementService movementManager = new PlayerMovementManager(_viewModel);

            //Act
            _viewModel.PlayerController.InputManager.KeyboardDirection.Returns(CacheHelper.Zero);
            movementManager.Tick();
            movementManager.FixedTick();

            //Assert
            _viewModel.MoverDalArray[1].Received().FixedTick();
        }
    }
}