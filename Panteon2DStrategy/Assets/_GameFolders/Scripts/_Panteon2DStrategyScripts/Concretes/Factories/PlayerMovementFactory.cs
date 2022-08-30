using Panteon2DStrategy.Abstracts.Controllers;
using Panteon2DStrategy.Abstracts.Movements;
using Panteon2DStrategy.Managers;
using Panteon2DStrategy.Movements;
using Panteon2DStrategy.ViewModels;

namespace Panteon2DStrategy.Factories
{
    public static class PlayerMovementFactory
    {
        public static PlayerMovementViewModel Create(IPlayerController playerController)
        {
            var moveViewModel = new PlayerMovementViewModel();
            moveViewModel.PlayerController = playerController;
            moveViewModel.MoverDalArray = new IMoverDal[]
                { new MoveWithMousePosition(playerController.TargetMover), new MoveWithTransformDal(playerController.TargetMover)};

            return moveViewModel;
        }
    }
}