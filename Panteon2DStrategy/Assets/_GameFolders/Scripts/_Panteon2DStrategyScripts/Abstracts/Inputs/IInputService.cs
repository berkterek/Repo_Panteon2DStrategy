using UnityEngine;

namespace Panteon2DStrategy.Abstracts.Inputs
{
    public interface IInputService
    {
        Vector2 MousePosition { get; }
        Vector2 KeyboardDirection { get; }
        bool IsLeftButton { get; }
        bool IsLeftButtonDown { get; }
        bool IsRightButtonDown { get; }
        bool CenterButtonDown { get; }
    }
}