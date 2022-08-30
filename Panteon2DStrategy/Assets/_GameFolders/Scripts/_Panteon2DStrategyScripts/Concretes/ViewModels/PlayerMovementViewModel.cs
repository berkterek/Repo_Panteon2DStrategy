using Panteon2DStrategy.Abstracts.Controllers;
using Panteon2DStrategy.Abstracts.Movements;

namespace Panteon2DStrategy.ViewModels
{
    public struct PlayerMovementViewModel
    {
        public IPlayerController PlayerController { get; set; }
        public IMoverDal[] MoverDalArray { get; set; }
    }
}