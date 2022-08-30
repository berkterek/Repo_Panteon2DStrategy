using System.Collections.Generic;
using Panteon2DStrategy.Abstracts.Inputs;
using Panteon2DStrategy.Controllers;
using Panteon2DStrategy.Enums;
using Panteon2DStrategy.Helpers;
using Panteon2DStrategyScripts.Helpers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

namespace Panteon2DStrategy.Managers
{
    public class GridBuildingManager : MonoBehaviour
    {
        [SerializeField] GridLayout _gridLayout;
        [SerializeField] Tilemap _mainTilemap;
        [SerializeField] Tilemap _tempTilemap;
        [SerializeField] TileBuildingController _tempBuildingController;
        [SerializeField] Vector3 _prePosition;
        [SerializeField] BoundsInt _preArea;
        [SerializeField] Camera _mainCamera;

        IInputService _inputManager;
        Dictionary<TileType, TileBase> _tileBases;
        
        public GridLayout GridLayout => _gridLayout;
        
        void Awake()
        {
            this.GetReference<GridLayout>(ref _gridLayout);
            _tileBases = new Dictionary<TileType, TileBase>();
            _mainCamera = Camera.main;
            _inputManager = PlayerInputManager.CreateSingleton(InputType.OldInput);
        }

        void OnValidate()
        {
            this.GetReference<GridLayout>(ref _gridLayout);
        }

        void Start()
        {
            //TODO refactor this code
            string path = "Tiles/";
            _tileBases.Add(TileType.Empty,null);
            _tileBases.Add(TileType.White, Resources.Load<TileBase>(path +"White"));
            _tileBases.Add(TileType.Red, Resources.Load<TileBase>(path+"Red"));
            _tileBases.Add(TileType.Blue, Resources.Load<TileBase>(path+"Blue"));
        }

        void Update()
        {
            if (_tempBuildingController == null) return;

            if (_inputManager.IsLeftButton)
            {
                if (EventSystem.current.IsPointerOverGameObject(0)) return;

                if (!_tempBuildingController.IsPlaced)
                {
                    Vector2 touchPosition = _mainCamera.ScreenToWorldPoint(_inputManager.MousePosition);
                    Vector3Int cellPosition = _gridLayout.LocalToCell(touchPosition);

                    if (_prePosition != cellPosition)
                    {
                        _tempBuildingController.transform.localPosition = _gridLayout.CellToLocalInterpolated(
                            cellPosition + new Vector3(0.5f, 0.5f, 0f));
                        _prePosition = cellPosition;
                        FollowBuilding();
                    }
                }    
            }
            else if (_inputManager.IsRightButtonDown)
            {
                if (_tempBuildingController.CanBePlaced(this))
                {
                    _tempBuildingController.Place(this);
                }
            }
            else if (_inputManager.CenterButtonDown)
            {
                ClearArea();
                Destroy(_tempBuildingController.gameObject);
            }
        }

        private void ClearArea()
        {
            TileBase[] toClear = new TileBase[_preArea.size.x * _preArea.size.y * _preArea.size.z];
            TileHelper.FillTiles(toClear, TileType.Empty, _tileBases);
            _tempTilemap.SetTilesBlock(_preArea,toClear);
        }

        private void FollowBuilding()
        {
            ClearArea();

            _tempBuildingController.SetAreaPosition(_gridLayout.WorldToCell(_tempBuildingController.gameObject.transform.position));
            BoundsInt buildingArea = _tempBuildingController.Area;

            TileBase[] baseArray = TileHelper.GetTileBlock(buildingArea, _mainTilemap);

            int size = baseArray.Length;
            TileBase[] tileArray = new TileBase[size];

            for (int i = 0; i < baseArray.Length; i++)
            {
                if (baseArray[i] == _tileBases[TileType.White])
                {
                    tileArray[i] = _tileBases[TileType.Blue];
                }
                else
                {
                    TileHelper.FillTiles(tileArray,TileType.Red,_tileBases);
                    break;
                }
            }
            
            _tempTilemap.SetTilesBlock(buildingArea, tileArray);
            _preArea = buildingArea;
        }

        public void InitializeWithBuilding(GameObject building)
        {
            _tempBuildingController = Instantiate(building, CacheHelper.Zero, CacheHelper.Identity)
                .GetComponent<TileBuildingController>();
            FollowBuilding();
        }

        public bool CanTakeArea(BoundsInt area)
        {
            TileBase[] baseArray = TileHelper.GetTileBlock(area, _mainTilemap);
            foreach (var tileBase in baseArray)
            {
                if (tileBase != _tileBases[TileType.White])
                {
                    Debug.Log("<color=red>Can not place here</color>");
                    return false;
                }
            }

            return true;
        }
        
        public void TakeArea(BoundsInt area)
        {
            TileHelper.SetTilesBlocks(area, TileType.Empty, _tempTilemap, _tileBases);
            TileHelper.SetTilesBlocks(area, TileType.Blue, _mainTilemap, _tileBases);
            _tempBuildingController = null;
        }
    }    
}