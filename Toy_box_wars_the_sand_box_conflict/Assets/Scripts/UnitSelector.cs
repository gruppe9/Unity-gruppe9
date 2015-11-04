using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class UnitSelector : MonoBehaviour
{
    Player playerComponent;
    RaycastHit hit;
    Ray mouseClickPosition;

    // Use this for initialization
    void Start()
    {
        playerComponent = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // raycasting stuff
            mouseClickPosition = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(mouseClickPosition, out hit))
            {
                //Selects the unit as the players selected unit, if the given unit isn't already selected.
                if (hit.collider.tag == playerComponent.CurrentTeam.ToString() && hit.collider.gameObject != playerComponent.SelectedUnit)
                {
                    playerComponent.SelectedUnit = hit.collider.gameObject;
                }
                //Selects the unit as the selected enemy unit, if the given unit isn't already selected.
                else if (hit.collider.tag != playerComponent.CurrentTeam.ToString() && hit.collider.gameObject != playerComponent.SelectedOther)
                {
                    playerComponent.SelectedOther = hit.collider.gameObject;
                }
                //Deselects the current enemy unit, if it is already selected when clicked upon.
                else if (hit.collider.tag != playerComponent.CurrentTeam.ToString() && hit.collider.gameObject == playerComponent.SelectedOther)
                {
                    playerComponent.SelectedOther = null;
                }
                //Deselects the current player unit, if it is already selected when clicked upon.
                else if (hit.collider.tag == playerComponent.CurrentTeam.ToString() && hit.collider.gameObject == playerComponent.SelectedUnit)
                {
                    playerComponent.SelectedUnit = null;
                }
            }
        }
    }
}
