using Panteon2DStrategy.Abstracts.Inputs;
using UnityEngine;

namespace Panteon2DStrategy.Managers
{
    public class InputManager : IInputService
    {
        static InputManager _instance;
        static readonly object _lockedObject = new object();
        readonly IInputDal _inputDal;

        public Vector2 MousePosition => _inputDal.MousePosition;
        public Vector2 KeyboardDirection => _inputDal.KeyboardDirection;
        public bool IsLeftButton => _inputDal.IsLeftButton;
        public bool IsRightButtonDown => _inputDal.IsRightButtonDown;
        public bool CenterButtonDown => _inputDal.IsCenterButtonDown;

        private InputManager(IInputDal inputDal)
        {
            _inputDal = inputDal;
        }

        public static InputManager CreateSingleton(IInputDal inputDal)
        {
            lock (_lockedObject)
            {
                if (_instance == null)
                {
                    _instance = new InputManager(inputDal);
                }
            }

            return _instance;
        }
    }
}