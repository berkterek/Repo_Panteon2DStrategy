using Sirenix.OdinInspector;
using UnityEngine;
using Panteon2DStrategyScripts.ExtensionMethods;

namespace Panteon2DStrategy.Controllers
{
    public class TileMapParentController : MonoBehaviour,ITileMapParentController
    {
        [SerializeField] TileMapChildController _tileMapChildPrefab;
        [SerializeField] float _tileStartXPosition = -30f;
        [SerializeField] float _difference = 0.25f;
        [SerializeField] int _creationCount = 50;
        [SerializeField] Transform _transform;
        [SerializeField] TileMapChildController[] _children;

        public ITileMapChildController[] TileMapChildren => _children;

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
        }

        private void CreateAndSetChildren()
        {
            Vector3 position = new Vector3(_tileStartXPosition, 0f, 0f);

            for (int i = 0; i < _creationCount; i++)
            {
                var tile = Instantiate(_tileMapChildPrefab, _transform);
                tile.Transform.localPosition = position;
                position += Vector3.right * _difference;
            }
            
            _children = GetComponentsInChildren<TileMapChildController>();
        }
    }

    public interface ITileMapParentController
    {
        ITileMapChildController[] TileMapChildren { get; }
    }
}