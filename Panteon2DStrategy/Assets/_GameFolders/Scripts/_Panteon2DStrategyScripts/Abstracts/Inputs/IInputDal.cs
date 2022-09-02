using UnityEngine;

namespace Panteon2DStrategy.Abstracts.Inputs
{
    public interface IInputDal
    {
        Vector2 MousePosition { get; }
        Vector2 KeyboardDirection { get; }
        bool IsLeftButton { get; }
        bool IsRightButtonDown { get; }
        bool IsCenterButtonDown { get; }
        bool IsLeftButtonDown { get; }
    }
}