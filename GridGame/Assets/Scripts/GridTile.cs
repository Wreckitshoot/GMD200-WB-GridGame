using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile : MonoBehaviour
{
    public GridManager GridManager;
    public Vector2Int gridCoords;
    private SpriteRenderer _spriteRenderer;
    private Color _defaultColour;
    // Start is called before the first frame update
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultColour = _spriteRenderer.color;
    }
    private void OnMouseOver()
    {
        GridManager.OnTileHoverEnter(this);
        setColour(Color.black);
    }
    private void OnMouseExit()
    {
        GridManager.OnTileHoverExit(this);
        resetColour();
    }
    private void OnMouseDown()
    {
        GridManager.OnTileSelected(this);
    }
    public void setColour(Color colour)
    {
        _spriteRenderer.color = colour;
    }
    public void resetColour()
    {
        _spriteRenderer.color = _defaultColour;
    }
}
