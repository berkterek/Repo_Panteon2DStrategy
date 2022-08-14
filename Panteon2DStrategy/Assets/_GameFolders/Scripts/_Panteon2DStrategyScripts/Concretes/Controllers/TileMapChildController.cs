using UnityEngine;
using Panteon2DStrategyScripts.ExtensionMethods;

namespace Panteon2DStrategy.Controllers
{
    public class TileMapChildController : MonoBehaviour,ITileMapChildController
    {
        [SerializeField] Transform _transform;
        [SerializeField] Transform _body;

        public Transform Transform => _transform;

        void Awake()
        {
            this.GetReference(ref _transform);
        }

        void OnValidate()
        {
            this.GetReference(ref _transform);   
        }
        
        public void BindScale(Vector2 value)
        {
            _body.localScale = value;
        }
    }

    public interface ITileMapChildController
    {
        Transform Transform { get; }
        void BindScale(Vector2 value);
    }
}