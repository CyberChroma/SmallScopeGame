using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private static Dictionary<Vector2Int, List<GameObject>> gridMap = new Dictionary<Vector2Int, List<GameObject>>();

    public static void AddToGrid(GameObject thingToAdd) {
        Vector2Int gridPosition = GetGridPosition(thingToAdd);
        AddToGridCommon(thingToAdd, gridPosition);
    }

    public static void AddToGrid(GameObject thingToAdd, Vector2Int newPosition) {
        AddToGridCommon(thingToAdd, newPosition);
    }

    public static void AddToGridCommon(GameObject thingToAdd, Vector2Int newPosition) {
        Vector2Int gridPosition = newPosition;

        if (gridMap.ContainsKey(gridPosition)) {
            // Something in that spot, add to list in dictionary
            List<GameObject> thingsAtPoint = gridMap[gridPosition];
            thingsAtPoint.Add(thingToAdd);
            gridMap[gridPosition] = thingsAtPoint;
        } else {
            // Nothing in that spot, add new element to dictionary
            List<GameObject> thingsAtPoint = new List<GameObject> {
                thingToAdd
            };
            gridMap.Add(gridPosition, thingsAtPoint);
        }
    }

    public static List<GameObject> LookInGrid(Vector2Int position) {
        if (gridMap.ContainsKey(position)) {
            return gridMap[position];
        }
        else
        {
            return null;
        }
    }

    public static void DeleteFromGrid(GameObject thingToDelete, Vector2Int oldPosition) {
        if (gridMap.ContainsKey(oldPosition)) {
            List<GameObject> thingsAtPoint = gridMap[oldPosition];
            if (thingsAtPoint.Contains(thingToDelete)) {
                thingsAtPoint.Remove(thingToDelete);
                if (thingsAtPoint.Count == 0) {
                    gridMap.Remove(oldPosition);
                }
            } else {
                print("ERROR COULDN\'T FIND OBJECT IN GRID POSITION");
            }
        } else {
            print("ERROR COULDN\'T FIND OBJECT IN GRID POSITION");
        }
    }

    public static void MoveObjectInGrid(GameObject thingToDelete, Vector2Int newPosition, Vector2Int oldPosition) {
        DeleteFromGrid(thingToDelete, oldPosition);
        AddToGrid(thingToDelete, newPosition);
    }

    public static Vector2Int GetGridPosition(GameObject thingToFind) {
        Vector2Int gridPosition = Vector2Int.zero;
        gridPosition.x = (int)thingToFind.transform.position.x;
        gridPosition.y = (int)thingToFind.transform.position.z;
        return gridPosition;
    }

    public static void ResetGrid() {
        gridMap.Clear();
    }
}
