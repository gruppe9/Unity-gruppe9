using UnityEngine;
using System.Collections;

[System.Serializable]
public class TileType
{
    public string name;

    public GameObject tileVisualPrefab;

    public float movementCost = 1;
    //if we need to make it more clear that certain terrain is impacable (right now the movement cost is just 9999999999999999999999)
    //we might want to add a bool or some such
    public bool isWalkable = true;

}
