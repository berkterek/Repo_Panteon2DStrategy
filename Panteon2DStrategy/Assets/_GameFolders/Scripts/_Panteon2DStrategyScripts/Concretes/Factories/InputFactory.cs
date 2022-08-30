using Panteon2DStrategy.Abstracts.Inputs;
using Panteon2DStrategy.Enums;
using Panteon2DStrategy.Inputs;

namespace Panteon2DStrategy.Factories
{
    public static class InputFactory
    {
        public static IInputDal Create(InputType inputType)
        {
            IInputDal inputDal;
            switch (inputType)
            {
                case InputType.OldInput:
                    inputDal = new OldInputDal();
                    break;
                case InputType.NewInput:
                    inputDal = new NewInputDal();
                    break;
                default:
                    inputDal = null;
                    break;
            }

            return inputDal;
        }
    }
}