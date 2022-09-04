using Panteon2DStrategy.Abstracts.Uis;
using Panteon2DStrategy.Systems;

namespace Panteon2DStrategy.Uis
{
    public class TogglePlayerButton : BaseButton
    {
        protected override void HandleOnButtonClicked()
        {
            ControlSystem.Instance.TogglePlayer();
        }
    }    
}