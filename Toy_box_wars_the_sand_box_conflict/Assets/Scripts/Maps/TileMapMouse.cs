using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TileMap))]
public class TileMapMouse : MonoBehaviour
{

    TileMap _tileMap;

    Vector3 currentTileCoord;

    public GameObject selectionCube;

    void Start()
    {
        _tileMap = GetComponent<TileMap>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            Debug.Log(_tileMap.tileSize);
            Debug.Log(_tileMap.tileSize);
            int x = Mathf.FloorToInt(hitInfo.point.x / _tileMap.tileSize);
            int z = Mathf.FloorToInt(hitInfo.point.z / _tileMap.tileSize);
            Debug.Log ("Tile: " + x + ", " + z);

            currentTileCoord.x = x;
            currentTileCoord.z = z;

            selectionCube.transform.position = currentTileCoord * _tileMap.tileSize;
        }
        else
        {
            // Hide selection cube?
        }

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Click!");
        }
    }
}





//movement hint
//if (Input.GetMouseButton(1) && selectedUnit != null)
//{
//    selectedUnit.MoveToCoord(currentTileCoord.x, currentTileCoord.z);
//}
