using Sirenix.OdinInspector;
using UnityEngine;

namespace Panteon2DStrategy.Abstracts.ScriptableObjects
{
    public abstract class ProductionDataContainerSO : ScriptableObject
    {
        [BoxGroup("Basic Info")]
        [SerializeField] string _name;
        
        [BoxGroup("Basic Info")]
        [Tooltip("This field for game designers not for in game process")]
        [TextArea(5,5)]
        [SerializeField] string _description;
        
        [BoxGroup("Base Info")]
        [SerializeField] BoundsInt _area;
        
        [BoxGroup("Sprite Info")]
        [PreviewField(75f)]
        [SerializeField] Sprite _icon;
        
        [BoxGroup("Game Object Info")]
        [SerializeField] GameObject _prefab;

        public string Name => _name;
        public Sprite Icon => _icon;
        public BoundsInt Area => _area;
        public GameObject Prefab => _prefab;
    }
}