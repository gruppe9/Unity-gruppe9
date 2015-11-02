using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TileMap : MonoBehaviour
{
    public GameObject selectedUnit;


    //List<Node> currentPath = null;    //might not be needed
    
    public TileType[] tileTypes;
    
    int[,] tiles;
    Node[,] graph;



    int mapSizeX = 10;
    int mapSizeY = 10;

    void Start()
    {
        //setup the selectedUnit´s variables
        selectedUnit.GetComponent<Unit>().tileX = (int)selectedUnit.transform.position.x;
        selectedUnit.GetComponent<Unit>().tileY = (int)selectedUnit.transform.position.y;
        selectedUnit.GetComponent<Unit>().map = this;
      
        GeneraMapData();
        GeneratePathfindingGraph();
        GenerateMapVisual();

    }
    void GeneraMapData()
    {
        //allocate our map tiles
        tiles = new int[mapSizeX, mapSizeY];

        //initialize our map tiles to be grass
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                tiles[x, y] = 0;
            }
        }
        //swampy area
        for (int x = 3; x < 5; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                tiles[x, y] = 1;
            }
        }
        //lets make a u shaped mountain range
        tiles[4, 4] = 2;
        tiles[5, 4] = 2;
        tiles[6, 4] = 2;
        tiles[7, 4] = 2;
        tiles[8, 4] = 2;


        tiles[4, 5] = 2;
        tiles[4, 6] = 2;
        tiles[8, 5] = 2;
        tiles[8, 6] = 2;

    }
    public float CostToEnterTile(int sourceX, int sourceY, int targetX, int targetY)
    {
        TileType tt = tileTypes[tiles[targetX, targetY]];
        if (UnitCanEnterTile(targetX, targetY) == false)
        {
            return Mathf.Infinity;
        }
        float cost = tt.movementCost;
        if (sourceX != targetX && sourceY != targetY)
        {
            //we are moving diagonally, fudge the cost for tie-breaking
            //purely cosmetix reason
            cost += 0.001f;
        }
        return cost;
    }

    void GeneratePathfindingGraph()
    {
        //initialize the array
        graph = new Node[mapSizeX, mapSizeY];

        //initialize a node for each spot in the array
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                graph[x, y] = new Node();

                graph[x, y].x = x;
                graph[x, y].y = y;
            }
        }
        //now that all the nodes exist, calculate their neighbours
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                // four way connected map
                        //if (x > 0)
                        //{
                        //    graph[x, y].neighbours.Add(graph[x - 1, y]);
                        //}
                        //if (x < mapSizeX - 1)
                        //{
                        //    graph[x, y].neighbours.Add(graph[x + 1, y]);
                        //}
                        //if (y > 0)
                        //{
                        //    graph[x, y].neighbours.Add(graph[x, y - 1]);
                        //}
                        //if (y < mapSizeY - 1)
                        //{
                        //    graph[x, y].neighbours.Add(graph[x, y + 1]);
                        //}
                //8 way allows diagonal movement
                //try left
                if (x > 0)
                {
                    graph[x, y].neighbours.Add(graph[x - 1, y]);
                    if (y > 0)
                    {
                        graph[x, y].neighbours.Add(graph[x - 1, y - 1]);
                    }
                    if (y < mapSizeY - 1)
                    {
                        graph[x, y].neighbours.Add(graph[x - 1, y + 1]);
                    }
                }
                //try right
                if (x < mapSizeX - 1)
                {
                    graph[x, y].neighbours.Add(graph[x + 1, y]);
                    if (y > 0)
                    {
                        graph[x, y].neighbours.Add(graph[x + 1, y - 1]);
                    }
                    if (y < mapSizeY - 1)
                    {
                        graph[x, y].neighbours.Add(graph[x + 1, y + 1]);
                    }
                }
                if (y > 0)
                {
                    graph[x, y].neighbours.Add(graph[x, y - 1]);
                }
                if (y < mapSizeY - 1)
                {
                    graph[x, y].neighbours.Add(graph[x, y + 1]);
                }
                //this can also works with six way hexes, and 8 way tiles and ^n way tiles with some extra code

            }
        }
    }
    void GenerateMapVisual()
    {
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                TileType tt = tileTypes[tiles[x, y]];
                GameObject go = (GameObject)Instantiate(tt.tileVisualPrefab, new Vector3(x, y, 0), Quaternion.identity);

                ClickableTile ct = go.GetComponent<ClickableTile>();
                ct.tileX = x;
                ct.tileY = y;
                ct.map = this;
            }
        }
    }

    public Vector3 TileCoordToWorldCoord(int x, int y)
    {
        return new Vector3(x, y, 0);
    }

    public bool UnitCanEnterTile(int x, int y)
    {
        // we could test the units walk/hover/ type against various terrain flags here
        //to see if they are allowed to enter the tile
        return tileTypes[tiles[x, y]].isWalkable;
    }
    public void GeneratePathTo(int x, int y)
    {
        //clear out our units old path
        selectedUnit.GetComponent<Unit>().currentPath = null;

        if (UnitCanEnterTile(x, y) == false)
        {
            //we probably clicked ona mountain or something so just quit out
            return;
        }
        //selectedUnit.GetComponent<Unit>().tileX = x;
        //selectedUnit.GetComponent<Unit>().tileY = y;
        //selectedUnit.transform.position = TileCoordToWorldCoord(x, y);

        Dictionary<Node, float> dist = new Dictionary<Node, float>();
        Dictionary<Node, Node> prev = new Dictionary<Node, Node>();

        // setup the Q -- the list of unchecked nodes
        List<Node> unvisited = new List<Node>();

        Node source = graph[
                            selectedUnit.GetComponent<Unit>().tileX,
                            selectedUnit.GetComponent<Unit>().tileY
                            ];

        Node target = graph[
                             x,
                             y
                             ];
        dist[source] = 0;
        prev[source] = null;

        //initialize everything to have infinity distance
        foreach (Node v in graph)
        {
            if (v != source)
            {
                dist[v] = Mathf.Infinity;
                prev[v] = null;
            }
            unvisited.Add(v);
        }
        while (unvisited.Count > 0)
        {
            //quick and dirty version, slow but short
            //consider having unvisited be priority queue or some other self sorting , 
            //optimized data structure
            //Node u = unvisited.OrderBy(n => dist[n]).First();

            //little faster
            //u is going to be the invisited node with the smallest distance
            Node u = null;
            foreach (Node possibleU in unvisited)
            {
                if (u == null || dist[possibleU] < dist[u])
                {
                    u = possibleU;
                }

            }

            if (u == target)
            {
                break; // exit the while loop
            }
            unvisited.Remove(u);

            foreach (Node v in u.neighbours)
            {
                //float alt = dist[u] + u.DistanceTo(v);
                float alt = dist[u] + CostToEnterTile(u.x, u.y, v.x, v.y);
                if (alt < dist[v])
                {
                    dist[v] = alt;
                    prev[v] = u;
                }
            }
        }
        // if we get here, then either we found the shortest route
        // to our target, or there is no route to our target
        if (prev[target] == null)
        {
            //no route to our between target and source
            return;
        }
        List<Node> currentPath = new List<Node>();

        Node curr = target;

        //step through the "prev" chain and add it to our path
        while (curr != null)
        {
            currentPath.Add(curr);
            curr = prev[curr];
        }
        //right now the currentPath describes a route from our target to our source
        //so we need to invert it!
        currentPath.Reverse(); // REVERSE!
        selectedUnit.GetComponent<Unit>().currentPath = currentPath;
    }
}
