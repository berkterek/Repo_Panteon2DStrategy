using Panteon2DStrategy.Abstracts.ScriptableObjects;
using Panteon2DStrategy.Controllers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Panteon2DStrategy.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Soldier Building Data Container", menuName = "Panteon/Data Containers/Soldier Building Data Container")]
    public class SoldierBuildingDataContainerSO : ProductionDataContainerSO
    {
        [BoxGroup("Soldier Object Info")] [SerializeField]
        GameObject _soldierObjectPrefab;

        public GameObject SoldierObjectPrefab => _soldierObjectPrefab;
    }
}