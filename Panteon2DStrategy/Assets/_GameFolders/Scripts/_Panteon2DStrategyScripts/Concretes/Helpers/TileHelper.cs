using System.Collections.Generic;
using Panteon2DStrategy.Enums;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Panteon2DStrategy.Helpers
{
    public static class TileHelper
    {
        public static TileBase[] GetTileBlock(BoundsInt area, Tilemap tilemap)
        {
            TileBase[] tileBaseArray = new TileBase[area.size.x * area.size.y * area.size.z];
            int counter = 0;

            foreach (var allPositionWithin in area.allPositionsWithin)
            {
                var position = new Vector3Int(allPositionWithin.x, allPositionWithin.y, 0);
                tileBaseArray[counter] = tilemap.GetTile(position);
                counter++;
            }

            return tileBaseArray;
        }

        public static void SetTilesBlocks(BoundsInt area, TileType tileType, Tilemap tilemap,
            Dictionary<TileType, TileBase> tileBases)
        {
            int size = area.size.x * area.size.y * area.size.z;
            TileBase[] tileArray = new TileBase[size];
            FillTiles(tileArray, tileType, tileBases);
            tilemap.SetTilesBlock(area, tileArray);
        }

        public static void FillTiles(TileBase[] tileBaseArray, TileType tileType,
            Dictionary<TileType, TileBase> tileBases)
        {
            for (int i = 0; i < tileBaseArray.Length; i++)
            {
                tileBaseArray[i] = tileBases[tileType];
            }
        }
    }
}