using System.Linq;
using Panteon2DStrategy.Abstracts.Helpers;
using Panteon2DStrategy.Controllers;
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

        public void SetBuildingToPlayer(TileBuildingController building)
        {
            _buildingInspectors.FirstOrDefault(x => x.PlayerType == ControlSystem.Instance.CurrentPlayerData.PlayerType).ValuesList.Add(building);
        }
    }    
}