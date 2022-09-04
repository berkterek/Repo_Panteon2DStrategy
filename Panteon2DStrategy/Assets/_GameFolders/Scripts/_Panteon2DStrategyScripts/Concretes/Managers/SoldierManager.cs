using System.Collections.Generic;
using System.Linq;
using Panteon2DStrategy.Abstracts.Helpers;
using Panteon2DStrategy.Controllers;
using Panteon2DStrategy.Enums;
using Panteon2DStrategy.Serializables;
using Panteon2DStrategy.Systems;
using UnityEngine;

namespace Panteon2DStrategy.Managers
{
    public class SoldierManager : SingletonDestroyObject<SoldierManager>
    {
        [SerializeField] SoldierInspector[] _soldiers;

        public List<SoldierController> GetSoldiers(PlayerType playerType)
        {
            return _soldiers.FirstOrDefault(x => x.PlayerType == playerType).ValuesList;
        }

        void Awake()
        {
            SetSingleton(this);
        }
        
        public void SetSoldierToPlayer(SoldierController soldier)
        {
            _soldiers.FirstOrDefault(x => x.PlayerType == ControlSystem.Instance.CurrentPlayerData.PlayerType).ValuesList.Add(soldier);
            AstarPath.active.Scan();
        }
    }
}