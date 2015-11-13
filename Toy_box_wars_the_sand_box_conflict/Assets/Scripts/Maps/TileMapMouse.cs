using UnityEngine;
using System.Collections;

public class TileMapMouse : MonoBehaviour
{

    float tileSize = 1;

    public GameObject map;

    Vector3 currentTileCoord;

    public GameObject selectionCube;

    void Start()
    {
        tileSize = MapStuff.Instance.tileSize;
    }

    /*
    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity))
        {
            //Debug.Log(_tileMap.tileSize);
            //Debug.Log(_tileMap.tileSize);
            int x = Mathf.FloorToInt(hitInfo.point.x / tileSize);
            int z = Mathf.FloorToInt(hitInfo.point.z / tileSize);
            Debug.Log("Tile: " + x + ", " + z);

            currentTileCoord.x = x;
            currentTileCoord.z = z;

            selectionCube.transform.position = currentTileCoord * tileSize;
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
    */

    void Update()
    {

    }

    public Vector3 TileToMouse(Vector3 point)
    {
        int x = Mathf.FloorToInt(point.x / tileSize);
        int z = Mathf.FloorToInt(point.z / tileSize);
        Debug.Log("Tile: " + x + ", " + z);

        currentTileCoord.x = x * tileSize + MapStuff.Instance.tileSize / 1.75f - 0.25f;
        currentTileCoord.z = z * tileSize + MapStuff.Instance.tileSize / 1.75f - 0.25f;
        currentTileCoord.y = 2;

        selectionCube.SetActive(true);
        selectionCube.transform.position = currentTileCoord;
        selectionCube.GetComponent<Renderer>().material.color = Color.red;

        point.x = currentTileCoord.x;
        point.z = currentTileCoord.z;
        return point;
    }

    public void KillPointer()
    {
        selectionCube.SetActive(false);
    }
}
