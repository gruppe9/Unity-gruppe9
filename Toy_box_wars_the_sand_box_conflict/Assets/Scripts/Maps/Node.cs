﻿using UnityEngine;
using System.Collections.Generic;

public class Node
{
    public List<Node> neighbours;

    public int x;
    public int z;

    public Node()
    {
        neighbours = new List<Node>();
    }


    public float DistanceTo(Node n)
    {
        return Vector3.Distance(
            new Vector3(x, 0, z),
            new Vector3(n.x, 0, n.z)
            );
    }
}
