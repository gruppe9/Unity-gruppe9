using UnityEngine;
using System.Collections;

public class SelectTest : MonoBehaviour
{
    Vector3 PlaceClicked;
    GameObject selectedUnit;
    GameObject otherSelectedUnit;
    string otherTeam;
    public string team;
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
                        selectedUnit.GetComponent<Renderer>().material.color = Color.white;
                    selectedUnit = hit.collider.gameObject;
                    selectedUnit.GetComponent<Renderer>().material.color = Color.green;
                }

                if (hit.collider.gameObject.tag == otherTeam) // select unit from other team
                {
                    if (otherSelectedUnit != null)
                        otherSelectedUnit.GetComponent<Renderer>().material.color = Color.white;
                    otherSelectedUnit = hit.collider.gameObject;
                    otherSelectedUnit.GetComponent<Renderer>().material.color = Color.red;
                }
            }
        }
        if (selectedUnit != null && Input.GetMouseButtonUp(0))
        {
        }
    }
}
