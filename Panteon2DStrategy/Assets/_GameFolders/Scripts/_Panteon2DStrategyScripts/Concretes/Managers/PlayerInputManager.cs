using Panteon2DStrategy.Abstracts.Inputs;
using Panteon2DStrategy.Enums;
using Panteon2DStrategy.Factories;
using UnityEngine;

namespace Panteon2DStrategy.Managers
{
    public class PlayerInputManager : IInputService
    {
        static PlayerInputManager _instance;
        static readonly object _lockedObject = new object();
        readonly IInputDal _inputDal;

        public Vector2 MousePosition => _inputDal.MousePosition;
        public Vector2 KeyboardDirection => _inputDal.KeyboardDirection;
        public bool IsLeftButton => _inputDal.IsLeftButton;
        public bool IsRightButtonDown => _inputDal.IsRightButtonDown;
        public bool CenterButtonDown => _inputDal.IsCenterButtonDown;

        private PlayerInputManager(IInputDal inputDal)
        {
            _inputDal = inputDal;
        }

        public static PlayerInputManager CreateSingleton(InputType inputType)
        {
            lock (_lockedObject)
            {
                if (_instance == null)
                {
                    _instance = new PlayerInputManager(InputFactory.Create(inputType));
                }
            }

            return _instance;
        }
    }
}