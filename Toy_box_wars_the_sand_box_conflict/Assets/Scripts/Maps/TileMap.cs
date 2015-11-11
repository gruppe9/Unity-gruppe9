using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class TileMap : MonoBehaviour
{
    public int offset = 1;
    //east to west
    public int size_x;
    //north to south
    public int size_z;

    Node[,] graph;

    public float tileSize;
    // Use this for initialization

    private static TileMap instance;

    public static TileMap Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<TileMap>();
            }
            return instance;
        }

        set
        {
            instance = value;
        }
    }

    void Start()
    {
        BuildMesh();
        GeneratePathfindingGraph();

    }

    public void BuildMesh()
    {
        int numTiles = size_x * size_z;
        int numTris = numTiles * 2;

        int vsize_x = size_x + 1;
        int vsize_z = size_z + 1;
        int numVerts = vsize_x * vsize_z;

        //generer mesh data
		Vector3[] vertices = new Vector3[ numVerts ];
		Vector3[] normals = new Vector3[numVerts];
		Vector2[] uv = new Vector2[numVerts];
		
		int[] triangles = new int[ numTris * 3 ];

        int x, z;
        for (z = 0; z < vsize_z; z++)
        {
            for (x = 0; x < vsize_x; x++)
            {
                vertices[z * vsize_x + x] = new Vector3(x * tileSize - offset, 0, z * tileSize -offset);
                normals[z * vsize_x + x] = Vector3.up;
                uv[z * vsize_x + x] = new Vector2((float)x / size_x, (float)z / size_z);
            }
        }
        Debug.Log("Done Verts!");

        for (z = 0; z < size_z; z++)
        {
            for (x = 0; x < size_x; x++)
            {
                int squareIndex = z * size_x + x;
                int triOffset = squareIndex * 6;
                triangles[triOffset + 0] = z * vsize_x + x + 0;
                triangles[triOffset + 1] = z * vsize_x + x + vsize_x + 0;
                triangles[triOffset + 2] = z * vsize_x + x + vsize_x + 1;

                triangles[triOffset + 3] = z * vsize_x + x + 0;
                triangles[triOffset + 4] = z * vsize_x + x + vsize_x + 1;
                triangles[triOffset + 5] = z * vsize_x + x + 1;
            }
        }
        //lav et ny mesh og fyld det med dataen
        Mesh mesh = new Mesh();
        //punkter meshet består af - hver trekant meshet består af har 3 vertices
        mesh.vertices = vertices;
        //
        mesh.triangles = triangles;
        mesh.normals = normals;

        mesh.uv = uv;

        //evt mf = meshfilter;
        //sæt vores mesh til vores filter/renderer/collider
        MeshFilter mesh_filter = GetComponent<MeshFilter>();
        MeshRenderer mesh_renderer = GetComponent<MeshRenderer>();
        MeshCollider mesh_collider = GetComponent<MeshCollider>();

        //mesh_filter.mesh = mesh;
        mesh_collider.sharedMesh = mesh;
        Debug.Log("Done Mesh!");
    }
    
    void GeneratePathfindingGraph()
    {
        //initialize the array
        graph = new Node[size_x, size_z];

        //initialize a node for each spot in the array
        for (int x = 0; x < size_x; x++)
        {
            for (int z = 0; z < size_z; z++)
            {
                graph[x, z] = new Node();

                graph[x, z].x = x;
                graph[x, z].z = z;
            }
        }
        //now that all the nodes exist, calculate their neighbours
        for (int x = 0; x < size_x; x++)
        {
            for (int z = 0; z < size_z; z++)
            {
                //8 way allows diagonal movement
                //try left
                if (x > 0)
                {
                    graph[x, z].neighbours.Add(graph[x - 1, z]);
                    if (z > 0)
                    {
                        graph[x, z].neighbours.Add(graph[x - 1, z - 1]);
                    }
                    if (z < size_z - 1)
                    {
                        graph[x, z].neighbours.Add(graph[x - 1, z + 1]);
                    }
                }
                //try right
                if (x < size_x - 1)
                {
                    graph[x, z].neighbours.Add(graph[x + 1, z]);
                    if (z > 0)
                    {
                        graph[x, z].neighbours.Add(graph[x + 1, z - 1]);
                    }
                    if (z < size_z - 1)
                    {
                        graph[x, z].neighbours.Add(graph[x + 1, z + 1]);
                    }
                }
                if (z > 0)
                {
                    graph[x, z].neighbours.Add(graph[x, z - 1]);
                }
                if (z < size_z - 1)
                {
                    graph[x, z].neighbours.Add(graph[x, z + 1]);
                }
                //this can also works with six way hexes, and 8 way tiles and ^n way tiles with some extra code

            }
        }
    }

    public void GeneratePathTo(Vector3 point, UnitProperties unit)
    {
        int x = (int)(point.x / tileSize);
        int z = (int)(point.z / tileSize);
        Debug.Log(x +", " + z);

        //clear out our units old path
        unit.currentPath = null;

        Dictionary<Node, float> dist = new Dictionary<Node, float>();
        Dictionary<Node, Node> prev = new Dictionary<Node, Node>();

        // setup the Q -- the list of unchecked nodes
        List<Node> unvisited = new List<Node>();

        Node source = graph[
                            unit.tileX,
                            unit.tileZ
                            ];

        Node target = graph[
                             x,
                             z
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
                float alt = dist[u] + CostToEnterTile(u.x, u.z, v.x, v.z);
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
        unit.currentPath = currentPath;
    }

    public float CostToEnterTile(int sourceX, int sourceY, int targetX, int targetY)
    {
        float cost = 1;
        if (sourceX != targetX && sourceY != targetY)
        {
            //we are moving diagonally, fudge the cost for tie-breaking
            //purely cosmetix reason
            cost += 0.001f;
        }
        return cost;
    }

    public Vector3 TileCoordToWorldCoord(int x, int y)
    {
        return new Vector3(x, y, 0);
    }

}
