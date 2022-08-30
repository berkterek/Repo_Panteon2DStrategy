using Panteon2DStrategy.Abstracts.Inputs;
using UnityEngine;

namespace Panteon2DStrategy.Abstracts.Controllers
{
    public interface IPlayerController : IEntityController
    {
        IInputService InputManager { get; set; }
        Camera MainCamera { get; }
    }
}