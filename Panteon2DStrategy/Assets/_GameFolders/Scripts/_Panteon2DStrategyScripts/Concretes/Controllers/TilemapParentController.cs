using Panteon2DStrategy.ScriptableObjects;
using Sirenix.OdinInspector;
using UnityEngine;
using Panteon2DStrategyScripts.ExtensionMethods;

namespace Panteon2DStrategy.Controllers
{
    public class TilemapParentController : MonoBehaviour,ITilemapParentController
    {
        [SerializeField] TilemapChildController tilemapChildPrefab;
        [SerializeField] TilemapParentDataContainer _tilemapParentDataContainer;
        [SerializeField] Transform _transform;
        [SerializeField] TilemapChildController[] _children;

        public ITilemapChildController[] TileMapChildren => _children;

        void Awake()
        {
            this.GetReference(ref _transform);
        }

        void OnValidate()
        {
            this.GetReference(ref _transform);
        }

        [BoxGroup("Buttons")]
        [Button(ButtonSizes.Medium)]
        private void CleanAndGetChildren()
        {
            CleanChildren();
            
            CreateAndSetChildren();
        }

        [BoxGroup("Buttons")]
        [Button(ButtonSizes.Medium)]
        private void CleanChildren()
        {
            if (_transform.childCount > 0)
            {
                int childCount = _transform.childCount;
                for (int i = 0; i < childCount; i++)
                {
                    DestroyImmediate(_transform.GetChild(0).gameObject);
                }
            }

            _children = null;
        }

        private void CreateAndSetChildren()
        {
            Vector3 position = new Vector3(_tilemapParentDataContainer.TileStartXPosition, 0f, 0f);

            for (int i = 0; i < _tilemapParentDataContainer.CreationCount; i++)
            {
                var tile = Instantiate(tilemapChildPrefab, _transform);
                tile.Transform.localPosition = position;
                tile.BindScale(_tilemapParentDataContainer.ChildScale);
                position += Vector3.right * _tilemapParentDataContainer.Difference;
            }
            
            _children = GetComponentsInChildren<TilemapChildController>();
        }
    }

    public interface ITilemapParentController
    {
        ITilemapChildController[] TileMapChildren { get; }
    }
}