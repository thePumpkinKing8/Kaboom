using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BreakableTile : MonoBehaviour
{
    private Tilemap _breakableTilemap;

    private void Awake()
    {
        _breakableTilemap = GetComponent<Tilemap>();

        if(_breakableTilemap == null)
        {
            Debug.LogError("Tilemap not found");
        }
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
           // _breakableTilemap.SetTile((, null);
        }
    }


    // This function should be called by an event
    public void BreakTile(Vector2 contactPoint)
    {

        Vector3 hitPosition = Vector3.zero;

        HashSet<Vector3Int> brokenTiles = new HashSet<Vector3Int>(); // Used to avoid processing issues from duplicate hits

        hitPosition.x = contactPoint.x; //- _compensationNumber * contactPoint.normalized.x;
        hitPosition.y = contactPoint.y; //- _compensationNumber * contactPoint.normalized.y;
        hitPosition.z = 0;

        Vector3Int currentCellPosition = _breakableTilemap.WorldToCell(hitPosition);

    
        _breakableTilemap.SetTile(currentCellPosition, null);

    }
}
