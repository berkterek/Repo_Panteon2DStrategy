using UnityEngine;

namespace Panteon2DStrategy.Controllers
{
    public class TileMapChildController : MonoBehaviour,ITileMapChildController
    {
        [SerializeField] Transform _transform;

        public Transform Transform => _transform;

        void Awake()
        {

        }

        void OnValidate()
        {
            
        }
    }

    public interface ITileMapChildController
    {
        Transform Transform { get; }
    }
}

