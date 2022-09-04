using Panteon2DStrategy.Abstracts.ScriptableObjects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Panteon2DStrategy.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Power Building Data Container", menuName = "Panteon/Data Containers/Power Building Data Container")]
    public class PowerPlantDataContainerSO : ProductionDataContainerSO
    {
        [BoxGroup("Soldier Object Info")] [SerializeField]
        float _value = 0.1f;

        [BoxGroup("Soldier Object Info")] [SerializeField]
        float _distance = 5f;

        public float Value => _value;
        public float Distance => _distance;
    }
}