using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BreakableTile : MonoBehaviour
{
    private Tilemap _breakableTilemap;

    private float _compensationNumber = 0.01f;

    private void Awake()
    {
        _breakableTilemap = GetComponent<Tilemap>();

        if(_breakableTilemap == null)
        {
            Debug.LogError("Tilemap not found");
        }
    }

    // This function should be called by an event
    public void BreakTile(Collision2D collision)
    {
        Debug.Log("BreakTile function called");

        Vector3 hitPosition = Vector3.zero;

        HashSet<Vector3Int> brokenTiles = new HashSet<Vector3Int>(); // Used to avoid processing issues from duplicate hits

        foreach (ContactPoint2D hit in collision.contacts)
        {
            // Find exactly what tile was hit in the tilemap
            hitPosition.x = hit.point.x - _compensationNumber * hit.normal.x;
            hitPosition.y = hit.point.y - _compensationNumber * hit.normal.y;

            Vector3Int currentCellPosition = _breakableTilemap.WorldToCell(hitPosition);

            Debug.Log($"Hit position: {hitPosition}, Cell position: {currentCellPosition}");

            if (!brokenTiles.Contains(currentCellPosition))
            {
                // Adds the broken tile to the HashSet
                brokenTiles.Add(currentCellPosition);

                // Sets the affected tile to null
                _breakableTilemap.SetTile(currentCellPosition, null);

                Debug.Log($"Tile located at {currentCellPosition} set to null");
            }
        }
    }
}
