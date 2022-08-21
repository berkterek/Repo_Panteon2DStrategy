using System.Collections.Generic;
using Panteon2DStrategy.Abstracts.Helpers;
using Panteon2DStrategy.Enums;
using Panteon2DStrategyScripts.Helpers;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Panteon2DStrategy.Managers
{
    public class GridBuildingManager : SingletonDestroyObject<GridBuildingManager>
    {
        [SerializeField] GridLayout _gridLayout;
        [SerializeField] Tilemap _mainTilemap;
        [SerializeField] Tilemap _tempTilemap;

        public Dictionary<TileType, TileBase> TileBases { get; private set; }
        
        void Awake()
        {
            SetSingleton(this);
            this.GetReference<GridLayout>(ref _gridLayout);
            TileBases = new Dictionary<TileType, TileBase>();
        }

        void OnValidate()
        {
            this.GetReference<GridLayout>(ref _gridLayout);
        }

        void Start()
        {
            string tilePath = @"Tiles\";
            TileBases.Add(TileType.Empty,null);
            TileBases.Add(TileType.White, Resources.Load<TileBase>(tilePath + "White"));
            TileBases.Add(TileType.Red, Resources.Load<TileBase>(tilePath + "Red"));
            TileBases.Add(TileType.Blue, Resources.Load<TileBase>(tilePath + "Blue"));
        }
    }    
}