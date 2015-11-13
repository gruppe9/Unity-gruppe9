using UnityEngine;
using System.Collections.Generic;

public class ArmySpawner : MonoBehaviour
{
    public GameObject barbie;
    public GameObject JackInABox;
    public GameObject RCCar;


    // Use this for initialization
    void Start()
    {
        CreateArmy1();
        CreateArmy2();
        SpawnArmies();
    }

    void CreateArmy1()
    {
        ArmySaves.Armies[0].Add(barbie);
        //modify barbie
        ArmySaves.Armies[0][0].transform.position = new Vector3(19.46428f, 1.04f, 10.46429f);
        ArmySaves.Armies[0][0].transform.rotation = new Quaternion(0, 73.11002f, 0, 0);
        ArmySaves.Armies[0][0].tag = "team1";

        ArmySaves.Armies[0].Add(JackInABox);
        ArmySaves.Armies[0][1].transform.position = new Vector3(16.46428f, 1.222f, 13.46429f);
        ArmySaves.Armies[0][1].transform.rotation = new Quaternion(0, 103.7f, 0, 0);
        ArmySaves.Armies[0][1].tag = "team1";

        ArmySaves.Armies[0].Add(RCCar);
        ArmySaves.Armies[0][2].transform.position = new Vector3(19.46428f, 0.48f, 7.464286f);
        ArmySaves.Armies[0][2].transform.rotation = new Quaternion(0, 53.16f, 0, 0);
        ArmySaves.Armies[0][2].tag = "team1";
    }


    void CreateArmy2()
    {
        ArmySaves.Armies[1].Add(barbie);
        //modify barbie
        ArmySaves.Armies[1][0].transform.position = new Vector3(31.46428f, 1.3f, 19.46428f);
        ArmySaves.Armies[1][0].transform.rotation = new Quaternion(0, 229.91f, 0, 0);
        ArmySaves.Armies[1][0].tag = "team2";

        ArmySaves.Armies[1].Add(JackInABox);
        ArmySaves.Armies[1][1].transform.position = new Vector3(31.46428f, 1.222f, 22.46428f);
        ArmySaves.Armies[1][1].transform.rotation = new Quaternion(0, 240.01f, 0, 0);
        ArmySaves.Armies[1][1].tag = "team2";

        ArmySaves.Armies[1].Add(RCCar);
        ArmySaves.Armies[1][2].transform.position = new Vector3(31.46428f, 1, 16.46428f);
        ArmySaves.Armies[1][2].transform.rotation = new Quaternion(0, 259.55f, 0, 0);
        ArmySaves.Armies[1][2].tag = "team2";
    }

    void SpawnArmies()
    {
        foreach (List<GameObject> list in ArmySaves.Armies)
        {
            foreach (GameObject item in list)
            {
                Instantiate(item);
            }
        }
    }
}