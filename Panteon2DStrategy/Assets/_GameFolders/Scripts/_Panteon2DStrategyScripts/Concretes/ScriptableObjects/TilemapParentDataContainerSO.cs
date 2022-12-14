using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Panteon2DStrategy.ScriptableObjects
{
    public class TilemapParentDataContainerSO : ScriptableObject
    {
        [SerializeField] float _tileStartXPosition = -30f;
        [SerializeField] float _difference = 0.25f;
        [SerializeField] int _creationCount = 50;
        [SerializeField] Vector2 _childScale;

        public Vector2 ChildScale => _childScale;
        public int CreationCount => _creationCount;
        public float Difference => _difference;
        public float TileStartXPosition => _tileStartXPosition;
    }
}