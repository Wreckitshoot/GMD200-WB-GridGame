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
    private void Awake()
    {
        InitGrid();

    }
    public void InitGrid()
    {
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
}
