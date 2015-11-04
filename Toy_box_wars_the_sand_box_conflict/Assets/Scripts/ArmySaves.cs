using System.Collections.Generic;
using UnityEngine;

static class ArmySaves
{
    private static List<GameObject>[] armies;
    public static int[] team = new int[2];

    public static List<GameObject>[] Armies
    {
        get
        {
            if (armies == null)
            {
                armies = new List<GameObject>[4];
                for (int i = 0; i < 4; i++)
                {
                    armies[i] = new List<GameObject>();
                }
            }
            return armies;
        }

        set
        {
            armies = value;
        }
    }
}
