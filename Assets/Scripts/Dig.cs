using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;

public class Dig : MonoBehaviour
{
    private float movementX;
    private float movementY;

    public RuleTile ruleTile;
    public Tilemap map;
    public Tilemap obstacles;

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void LateUpdate()
    {
        //Vector3Int currentMapCell = map.WorldToCell(transform.position);
        //if(currentMapCell.y <= 2) map.SetTile(currentMapCell, ruleTile);

        Vector3Int currentCell = obstacles.WorldToCell(transform.position);
        Vector3Int original = currentCell;

        AstarPath.active.Scan();

        if (movementX > 0)
        {
            currentCell.x += 1;
            obstacles.SetTile(currentCell, null);
            if (currentCell.y <= 2) map.SetTile(currentCell, ruleTile);
            currentCell = original;
        }
        if (movementX < 0)
        {
            currentCell.x -= 1;
            obstacles.SetTile(currentCell, null);
            if (currentCell.y <= 2) map.SetTile(currentCell, ruleTile);
            currentCell = original;
        }
        if (movementY > 0)
        {
            currentCell.y += 1;
            obstacles.SetTile(currentCell, null);
            if (currentCell.y <= 2) map.SetTile(currentCell, ruleTile);
            currentCell = original;
        }
        if (movementY < 0)
        {
            currentCell.y -= 1;
            obstacles.SetTile(currentCell, null);
            if (currentCell.y <= 2) map.SetTile(currentCell, ruleTile);
            currentCell = original;
        }
    }
}
