using System.Collections.Generic;
using Panteon2DStrategy.Controllers;
using Panteon2DStrategy.Enums;
using UnityEngine;

namespace Panteon2DStrategy.Serializables
{
    [System.Serializable]
    public class SoldierInspector : GenericInspector<SoldierController>
    {
        
    }

    [System.Serializable]
    public abstract class GenericInspector<T> where T : MonoBehaviour,ICanSelectableController
    {
        [SerializeField] PlayerType _playerType;
        [SerializeField] List<T> _valuesList;

        public PlayerType PlayerType => _playerType;
        public List<T> ValuesList => _valuesList;
    }
}