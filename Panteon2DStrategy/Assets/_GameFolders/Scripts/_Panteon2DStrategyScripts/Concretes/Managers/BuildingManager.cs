using System.Collections.Generic;
using System.Linq;
using Panteon2DStrategy.Abstracts.Controllers;
using Panteon2DStrategy.Abstracts.Helpers;
using Panteon2DStrategy.Enums;
using Panteon2DStrategy.Serializables;
using Panteon2DStrategy.Systems;
using UnityEngine;

namespace Panteon2DStrategy.Managers
{
    public class BuildingManager : SingletonDestroyObject<BuildingManager>
    {
        [SerializeField] BuildingInspector[] _buildingInspectors;
        
        void Awake()
        {
            SetSingleton(this);
        }

        public void SetBuildingToPlayer(BaseTileBuildingController building)
        {
            _buildingInspectors.FirstOrDefault(x => x.PlayerType == ControlSystem.Instance.CurrentPlayerData.PlayerType).ValuesList.Add(building);
            AstarPath.active.Scan();
        }

        public List<BaseTileBuildingController> GetBuildings(PlayerType playerType)
        {
            return _buildingInspectors.FirstOrDefault(x => x.PlayerType == playerType).ValuesList;
        }

        public void SetAllBuildingInPlaceWhenGameStart(GridBuildingManager gridManager)
        {
            foreach (BuildingInspector buildingInspector in _buildingInspectors)
            {
                foreach (var baseTileBuildingController in buildingInspector.ValuesList)
                {
                    baseTileBuildingController.Place(gridManager);
                }
            }
        }
    }    
}