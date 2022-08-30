using Panteon2DStrategy.Abstracts.Inputs;
using UnityEngine;

namespace Panteon2DStrategy.Inputs
{
    public class NewInputDal : IInputDal
    {
        public Vector2 MousePosition { get; }
        public Vector2 KeyboardDirection { get; }
        public bool IsLeftButton { get; }
        public bool IsRightButtonDown { get; }
        public bool IsCenterButtonDown { get; }
    }
}