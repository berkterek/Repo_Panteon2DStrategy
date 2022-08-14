using UnityEngine;
using Panteon2DStrategyScripts.ExtensionMethods;

namespace Panteon2DStrategy.Controllers
{
    public class TileMapChildController : MonoBehaviour,ITileMapChildController
    {
        [SerializeField] Transform _transform;

        public Transform Transform => _transform;

        void Awake()
        {
            this.GetReference(ref _transform);
        }

        void OnValidate()
        {
            this.GetReference(ref _transform);   
        }
    }

    public interface ITileMapChildController
    {
        Transform Transform { get; }
    }
}

