using Panteon2DStrategy.Abstracts.Inputs;
using UnityEngine;

namespace Panteon2DStrategy.Inputs
{
    public class OldInputDal : IInputDal
    {
        public Vector2 MousePosition => Input.mousePosition;

        public Vector2 KeyboardDirection
        {
            get
            {
                float horizontal = Input.GetAxis("Horizontal");
                float vertical = Input.GetAxis("Vertical");

                return new Vector2(horizontal, vertical);
            }
        }

        public bool IsLeftButton => Input.GetMouseButton(0);
        public bool IsRightButtonDown => Input.GetMouseButtonDown(1);
        public bool IsCenterButtonDown => Input.GetMouseButtonDown(2);
        public bool IsLeftButtonDown => Input.GetMouseButtonDown(0);
    }
}