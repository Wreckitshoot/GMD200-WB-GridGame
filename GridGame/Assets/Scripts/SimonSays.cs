using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonSays : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;
    private List<Vector2Int> _correctPositions = new List<Vector2Int>();
    private bool patternPlaying;
    private int playerPatternIndex;
    private void OnEnable()
    {
        gridManager.TileSelected += OnTileSelected;
    }
    private void OnDisable()
    {
        gridManager.TileSelected -= OnTileSelected;
    }
    private void OnTileSelected(GridTile obj)
    {
        if(patternPlaying)
        {
            return;
        }
        if(obj.gridCoords == _correctPositions[playerPatternIndex])
        {
            Debug.Log("Correct");
            StartCoroutine(Co_FlashTile(obj, Color.green, 0.25f));
            playerPatternIndex++;
            if(playerPatternIndex == _correctPositions.Count)
            {
                NextPattern();
            }
        }
        else
        {
            Debug.Log("Wrong");
            StartCoroutine(Co_FlashTile(obj, Color.red, 0.25f));
            _correctPositions.Clear();
            NextPattern();
        }
    }
    public void Start()
    {
        NextPattern();
    }
    [ContextMenu("Next Pattern")] 
    public void NextPattern()
    {
        playerPatternIndex = 0;
        _correctPositions.Add(new Vector2Int(Random.Range(0,gridManager.numCols),Random.Range(0,gridManager.numRows)));
        StartCoroutine(Co_PlayPattern(_correctPositions));
    }
    private IEnumerator Co_PlayPattern(List<Vector2Int> positions)
    {
        patternPlaying = true;
        yield return new WaitForSeconds(1);
        foreach(var pos in positions)
        {
            GridTile tile = gridManager.GetTile(pos);
            yield return Co_FlashTile(tile, Color.green, 0.25f) ;
            yield return new WaitForSeconds(0.5f);
        }
        patternPlaying = false;
    }
    private IEnumerator Co_FlashTile(GridTile tile, Color colour, float duration)
    {
        tile.setColour(colour);
        yield return new WaitForSeconds(duration);
        tile.resetColour();
    }
}
