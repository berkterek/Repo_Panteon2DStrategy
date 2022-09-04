using Sirenix.OdinInspector;
using UnityEngine;

namespace Panteon2DStrategy.Abstracts.ScriptableObjects
{
    public abstract class ProductionDataContainerSO : BaseSelectDataContainerSO
    {
        [BoxGroup("Base Info")]
        [SerializeField] BoundsInt _area;

        public BoundsInt Area => _area;
    }
}