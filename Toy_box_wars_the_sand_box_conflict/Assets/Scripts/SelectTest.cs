using UnityEngine;
using System.Collections;
using UnityEditor;

public class SelectTest : MonoBehaviour
{
    private Vector3 placeClicked;
    private float targetDistance;
    private GameObject selectedUnit;
    private GameObject otherSelectedUnit;
    private GameObject previousSelectedUnit;
    private string otherTeam;
    private static SelectTest instance;
    public string team;
    private float distination;

    /// <summary>
    /// property for what allied unit is selected
    /// </summary>
    public GameObject SelectedUnit
    {
        get
        {
            return selectedUnit;
        }

        set
        {
            selectedUnit = value;
        }
    }
    /// <summary>
    /// property for what enemy unit is selected
    /// </summary>
    public GameObject OtherSelectedUnit
    {
        get
        {
            return otherSelectedUnit;
        }

        set
        {
            otherSelectedUnit = value;
        }
    }
    public static SelectTest Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SelectTest>();
            }
            return instance;
        }

        set
        {
            instance = value;
        }
    }
    public float Distination
    {
        get
        {
            return distination;
        }

        set
        {
            distination = value;
        }
    }
    public float TargetDistance
    {
        get
        {
            return targetDistance;
        }

        set
        {
            targetDistance = value;
        }
    }
    public GameObject PreviousSelectedUnit
    {
        get
        {
            return previousSelectedUnit;
        }

        set
        {
            previousSelectedUnit = value;
        }
    }

    // Use this for initialization
    void Start()
    {
        team = "team 1";
        if (team == "team 1")
        {
            otherTeam = "team 2";
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray destPoint = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(destPoint, out hit))
            {

                if (hit.collider.gameObject.tag == team) // select unit from own team
                {
                    if (selectedUnit != null)
                    {
                        selectedUnit.GetComponent<Renderer>().material.color = Color.white;
                        selectedUnit.GetComponent<UnitStats>().IsSeleccted = false;
                    }
                    
                    selectedUnit = hit.collider.gameObject;
                    selectedUnit.GetComponent<UnitStats>().IsSeleccted = true;
                    selectedUnit.GetComponent<Renderer>().material.color = Color.green;
                }

                if (hit.collider.gameObject.tag == otherTeam && selectedUnit != null) // select unit from other team
                {
                    if (otherSelectedUnit != null)
                    {
                        otherSelectedUnit.GetComponent<Renderer>().material.color = Color.cyan;
                    }

                    otherSelectedUnit = hit.collider.gameObject;
                    TargetDistance = Vector3.Distance(selectedUnit.transform.position, otherSelectedUnit.transform.position);
                    otherSelectedUnit.GetComponent<Renderer>().material.color = Color.red;
                }
            }
        }
        if (selectedUnit != null && Input.GetMouseButtonUp(0))
        {
        }
    }
}
