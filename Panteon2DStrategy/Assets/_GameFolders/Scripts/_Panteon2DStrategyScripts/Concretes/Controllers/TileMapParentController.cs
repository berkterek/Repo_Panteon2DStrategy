using Sirenix.OdinInspector;
using UnityEngine;

namespace Panteon2DStrategy.Controllers
{
    public class TileMapParentController : MonoBehaviour
    {
        [SerializeField] TileMapChildController _tileMapChildPrefab;
        [SerializeField] float _tileStartXPosition = -30f;
        [SerializeField] float _difference = 0.25f;
        [SerializeField] int _creationCount = 50;
        [SerializeField] int _childCount = 0;
        [SerializeField] TileMapChildController[] _children;

        [Button]
        private void CleanAndGetChildren()
        {
            CleanChildren();
            
            CreateAndSetChildren();
        }

        private void CleanChildren()
        {
            if (transform.childCount > 0)
            {
                int childCount = transform.childCount;
                for (int i = 0; i < childCount; i++)
                {
                    DestroyImmediate(transform.GetChild(0).gameObject);
                }
            }
        }

        private void CreateAndSetChildren()
        {
            Vector3 position = new Vector3(_tileStartXPosition, 0f, 0f);

            for (int i = 0; i < _childCount; i++)
            {
                var tile = Instantiate(_tileMapChildPrefab, this.transform);
                tile.transform.localPosition = position;
                position += Vector3.right * _difference;
            }
            
            _children = GetComponentsInChildren<TileMapChildController>();
        }
    }
}