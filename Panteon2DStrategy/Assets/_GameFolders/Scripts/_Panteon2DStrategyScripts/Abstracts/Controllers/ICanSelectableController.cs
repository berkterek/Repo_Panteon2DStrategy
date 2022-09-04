using Panteon2DStrategy.Enums;

namespace Panteon2DStrategy.Abstracts.Controllers
{
    public interface ICanSelectableController
    {
        bool IsSelected { get; }
        void Toggle();
        void Unselected();
        event System.Action<bool> OnToggleValueChanged;
        PlayerType PlayerType { get; }
    }
}