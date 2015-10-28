using UnityEngine;
using System.Collections;
using UnityEditor;

public class SelectTest : MonoBehaviour
{
    Vector3 placeClicked;
    private bool isSelected = false;  
    private float targetDistance;
    GameObject selectedUnit;
    GameObject otherSelectedUnit;
    string otherTeam;
    static SelectTest instance;
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
    public bool IsSelected
    {
        get
        {
            return isSelected;
        }

        set
        {
            isSelected = value;
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
        if (Input.GetMouseButtonUp(0))
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
                        IsSelected = false;
                    }

                    selectedUnit = hit.collider.gameObject;
                    IsSelected = true;               
                    selectedUnit.GetComponent<Renderer>().material.color = Color.green;
                }

                if (hit.collider.gameObject.tag == otherTeam && selectedUnit != null) // select unit from other team
                {
                    if (otherSelectedUnit != null)
                    {
                        otherSelectedUnit.GetComponent<Renderer>().material.color = Color.white;
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

    public void DrawAttackRange()
    {
        //Handles.DrawSolidArc(selectedUnit.transform.position, selectedUnit.transform.up, -selectedUnit.transform.right, 180, selectedUnit.GetComponent<Stats>().AttackRange);
    }

}
