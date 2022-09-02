using Panteon2DStrategy.Abstracts.Helpers;
using Panteon2DStrategy.Managers;

namespace Panteon2DStrategy.Systems
{
    public class ControlSystem : SingletonDestroyObject<ControlSystem>
    {
        void Update()
        {
            var players = PlayerManager.Instance.Players;
            foreach (var playerData in players)
            {
                if (playerData.PlayerController.InputManager.IsLeftButtonDown)
                {
                    var soldiers = SoldierManager.Instance.GetSoldiers(playerData.PlayerType);

                    foreach (var soldier in soldiers)
                    {
                        if(!soldier.IsSelected) continue;

                        var worldPosition =
                            playerData.PlayerController.MainCamera.ScreenToWorldPoint(playerData.PlayerController
                                .InputManager.MousePosition);
                        soldier.TargetPosition = worldPosition;
                    }
                }    
            }
        }
    }
}