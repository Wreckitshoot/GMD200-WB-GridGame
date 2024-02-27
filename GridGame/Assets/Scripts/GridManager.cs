using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public event Action<GridTile> TileSelected;
    public int numRows = 5;

    public int numCols = 6;

    public float padding = 0.1f;

    [SerializeField] private GridTile tilePrefab;
    [SerializeField] private TextMeshProUGUI text;
    private GridTile[] tiles;
    private void Awake()
    {
        InitGrid();

    }
    public void InitGrid()
    {
        tiles = new GridTile[numRows*numCols];
        for (int y = 0; y < numRows; y++)
        {
            for(int x = 0; x < numCols; x++)
            {
                GridTile tile = Instantiate(tilePrefab, transform);
                Vector2 tilePos = new Vector2(x + padding*x, y+padding*y);
                tile.transform.localPosition = tilePos;
                tile.name = $"Tile_{x}_{y}";
                tile.GridManager = this;
                tile.gridCoords = new Vector2Int(x, y);
                tiles[y*numCols + x] = tile;
            }
        }
    }
    public void OnTileHoverEnter(GridTile gridTile)
    {
        text.text = gridTile.gridCoords.ToString();
    }
    public void OnTileHoverExit(GridTile gridTile)
    {
        text.text = "";
    }
    public void OnTileSelected(GridTile gridTile)
    {
        TileSelected?.Invoke(gridTile);
    }

    internal GridTile GetTile(Vector2Int pos)
    {
        if(pos.x < 0 || pos.x >= numCols || pos.y < 0 || pos.y >= numRows)
        {
            Debug.LogError($"Invalid Coordinate{pos}");
            return null;
        }
        return tiles[pos.y * numCols + pos.x];
    }
}
