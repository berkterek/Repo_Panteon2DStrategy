using System.Collections.Generic;
using Panteon2DStrategy.Controllers;
using Panteon2DStrategy.ScriptableObjects;
using Panteon2DStrategyScripts.Helpers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Panteon2DStrategy.Managers
{
    public class MapCreatorManagerWithNormalObject : MonoBehaviour
    {
        [SerializeField] float _startYPosition = -30f;
        [SerializeField] float _difference = 0.35f;
        [SerializeField] int _creationCount = 100;
        [SerializeField] Transform _transform;
        [SerializeField] TilemapParentController _tilemapParentPrefab;
        [SerializeField] TilemapParentDataContainerSO[] _parentDataContainers;

        Dictionary<ITilemapParentController, List<ITilemapChildController>> _tilemaps;

        public static MapCreatorManagerWithNormalObject Instance { get; private set; }

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            
            this.GetReference(ref _transform);
        }

        void OnValidate()
        {
            this.GetReference(ref _transform);
        }

        void OnDestroy()
        {
            Instance = null;
        }
        
        [BoxGroup("Buttons")]
        [Button(ButtonSizes.Medium)]
        private void CreateTilemaps()
        {
            CleanChildren();
            
            Vector3 position = new Vector3(0f, _startYPosition, 0f);

            for (int i = 0; i < _creationCount; i++)
            {
                var tileParent = Instantiate(_tilemapParentPrefab, _transform);
                int index = i % 2;
                tileParent.Bind(_parentDataContainers[index]);
                tileParent.Transform.localPosition = position;
                tileParent.CleanAndGetChildren();
                position += _difference * Vector3.up;
            }
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
    }    
}