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

    // This function should be called by an event
    public void BreakTile(Vector2 contactPoint)
    {
        contactPoint = new Vector2(contactPoint.x, contactPoint.y);
        TileBreak(contactPoint);
    }

    void TileBreak(Vector2 contactPoint)
    {
        Vector3 hitPosition = Vector3.zero;
        
        HashSet<Vector3Int> brokenTiles = new HashSet<Vector3Int>(); // Used to avoid processing issues from duplicate hits

        hitPosition.x = contactPoint.x;
        hitPosition.y = contactPoint.y;
        hitPosition.z = 0;

        Vector3Int currentCellPosition = _breakableTilemap.WorldToCell(hitPosition);

        Debug.Log(hitPosition);
        _breakableTilemap.SetTile(currentCellPosition, null);
        PoolObject effect = PoolManager.Instance.Spawn("Explosion1");
        effect.transform.position = hitPosition;
    }
}