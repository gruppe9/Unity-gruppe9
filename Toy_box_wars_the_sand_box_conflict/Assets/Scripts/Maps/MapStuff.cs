using UnityEngine;
using System.Collections.Generic;

public class MapStuff : MonoBehaviour
{

    Node[,] graph;
    public int mapSizeX;
    public int mapSizeZ;
    public int tileSize = 1;

    public GameObject node;

    private static MapStuff instance;

    public static MapStuff Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MapStuff>();
            }
            return instance;
        }

        set
        {
            instance = value;
        }
    }

    // Use this for initialization
    void Start()
    {
        GenerateNodes();
    }

    void GenerateNodes()
    {
        graph = new Node[mapSizeX, mapSizeZ];

        // generate all the Nodes
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int z = 0; z < mapSizeZ; z++)
            {
                graph[x, z] = new Node();
                graph[x, z].x = x * tileSize;
                graph[x, z].z = z * tileSize;

                //// Placing a pillar at each node \\\\
                Instantiate(node, new Vector3(x * tileSize, 0, z * tileSize), Quaternion.identity);
                //// Placing a pillar at each node \\\\
            }
        }
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int z = 0; z < mapSizeZ - 1; z++)
            {
                /*
                if (x > 0)
                    graph[x, z].neighbours.Add(graph[x - 1, z]);
                if (x < mapSizeX - 1)
                    graph[x, z].neighbours.Add(graph[x + 1, z]);
                if (z > 0)
                    graph[x, z].neighbours.Add(graph[x, z - 1]);
                if (z < mapSizeZ - 1)
                    graph[x, z].neighbours.Add(graph[x, z + 1]);

                */

                //try left
                if (x > 0)
                {
                    graph[x, z].neighbours.Add(graph[x - 1, z]);
                    if (z > 0)
                        graph[x, z].neighbours.Add(graph[x - 1, z - 1]);
                    if (z < mapSizeZ - 1)
                        graph[x, z].neighbours.Add(graph[x - 1, z + 1]);
                }

                // try right
                if (x < mapSizeX - 1)
                {
                    graph[x, z].neighbours.Add(graph[x + 1, z]);
                    if (z > 0)
                        graph[x, z].neighbours.Add(graph[x + 1, z - 1]);
                    if (z < mapSizeZ - 1)
                        graph[x, z].neighbours.Add(graph[x + 1, z + 1]);
                }

                // up and down
                if (z > 0)
                    graph[x, z].neighbours.Add(graph[x, z - 1]);
                if (z < mapSizeZ - 1)
                    graph[x, z].neighbours.Add(graph[x, z + 1]);

            }
        }
    }

    /// <summary>
    /// generate a path to a destination
    /// </summary>
    /// <param name="destination"></param>
    /// <returns></returns>
    public List<Node> GeneratePath(Vector3 destination, UnitProperties unit)
    {
        List<Node> currentPath = null;

        int x = (int)destination.x / tileSize;
        int z = (int)destination.z / tileSize;
        Debug.Log("started pathfinding with input: " + x + ", " + z);

        #region invalid input checks

        if (x < 0)
        {
            Debug.Log("invalid corrdinats: x is less than 0");
            return new List<Node>();
        }
        if (x > mapSizeX)
        {
            Debug.Log("invalid corrdinats: x is larger than map size");
            return new List<Node>();
        }
        if (z < 0)
        {
            Debug.Log("invalid corrdinats: z is less than 0");
            return new List<Node>();
        }
        if (z > mapSizeZ)
        {
            Debug.Log("invalid corrdinats: z is larger than map size");
            return new List<Node>();
        }

        #endregion
        Debug.Log("input is valid");


        Dictionary<Node, float> dist = new Dictionary<Node, float>();
        Dictionary<Node, Node> prev = new Dictionary<Node, Node>();
        List<Node> unvisited = new List<Node>();


        Node source = graph[
            (int)unit.transform.position.x / tileSize,
            (int)unit.transform.position.z / tileSize
            ];

        Node target = graph[
            x,
            z
            ];

        dist[source] = 0;
        prev[source] = null;


        // initialize nodes to have infinity distance
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
                break;
            }

            unvisited.Remove(u);

            foreach (Node v in u.neighbours)
            {
                float alt = dist[u] + u.DistanceTo(v);
                if (alt < dist[v])
                {
                    dist[v] = alt;
                    prev[v] = u;
                }
            }
        }

        if (prev[target] == null)
        {
            Debug.Log("wtf I can't find my target");
            return new List<Node>();
        }

        currentPath = new List<Node>();

        Node curr = target;
        while (curr != null)
        {
            currentPath.Add(curr);
            curr = prev[curr];
        }

        currentPath.Reverse();

        return currentPath;
    }
}
