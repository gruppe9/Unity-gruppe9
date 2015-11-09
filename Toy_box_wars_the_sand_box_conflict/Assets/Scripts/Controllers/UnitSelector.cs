using UnityEngine;
using System.Collections;
using System.Threading;

public class UnitSelector : MonoBehaviour
{
    private Player playerComponent;
    private RaycastHit hit;
    private Ray mouseClickPosition;
    private Ray touchPosition;

    // Use this for initialization
    void Start()
    {
        playerComponent = GetComponent<Player>();
    }
    // Update is called once per frame
    void Update()
    {
        if (playerComponent.SelectedOther != null && playerComponent.PlayerMode == PlayerAction.normal)
        {
            DeselectUnit();
        }

        PlayerActionSwitchCase();
    }
    private void PlayerActionSwitchCase()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //For testing on pc
            mouseClickPosition = Camera.main.ScreenPointToRay(Input.mousePosition);

            //For build on android. Also copy in if statements below instead of mouseClickPosition
            //touchPosition = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

            switch (playerComponent.PlayerMode)
            {
                case PlayerAction.normal:
                    if (Physics.Raycast(mouseClickPosition, out hit))
                    {
                        SelectUnit();
                    }
                    break;
                case PlayerAction.attack:
                    if (Physics.Raycast(mouseClickPosition, out hit))
                    {
                        if (playerComponent.SelectedOther != null)
                        {
                            playerComponent.ConfirmButton.SetActive(true);
                        }
                        AttackMode();
                    }
                    break;
                case PlayerAction.move:
                    if (Physics.Raycast(mouseClickPosition, out hit))
                    {
                        if (playerComponent.MoveDestination == Vector3.zero)
                        {
                            MoveMode();
                        }
                        if (playerComponent.MoveDestination != Vector3.zero)
                        {
                            playerComponent.ConfirmButton.SetActive(true);
                        }
                        //MoveMode();
                        //playerComponent.ConfirmButton.SetActive(true);
                    }
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// Highlights/Selects a unit if the player touches the unit on the screen.
    /// </summary>
    private void SelectUnit()
    {
        //Selects the unit as the players selected unit, if the given unit isn't already selected.
        if (hit.collider.gameObject != playerComponent.SelectedUnit)
        {
            //If the currentteam value equals the tag on the selected unit it is a friendly unit. 
            //Else is it a enemy unit
            if (playerComponent.CurrentTeam.ToString() == hit.collider.gameObject.tag)
            {
                //Sets the color back to white if a new player unit is selected
                if (playerComponent.SelectedUnit != null)
                    playerComponent.SelectedUnit.GetComponent<Renderer>().material.color = Color.white;

                playerComponent.SelectedUnit = hit.collider.gameObject; // the selectedunit is the on the that the player pressed on.
                hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.green;
                playerComponent.AttackButton.SetActive(true);
                playerComponent.MoveButton.SetActive(true);
            }
            else if (playerComponent.OtherTeam.ToString() == hit.collider.gameObject.tag)
            {
                if (playerComponent.SelectedUnit != null)
                    //Sets the color back to white if a new player unit is selected
                    playerComponent.SelectedUnit.GetComponent<Renderer>().material.color = Color.white;

                playerComponent.SelectedUnit = hit.collider.gameObject; // the selectedunit is the on the that the player pressed on.
                hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.red;
                playerComponent.AttackButton.SetActive(false);
                playerComponent.MoveButton.SetActive(false);
            }
        }
    }
    /// <summary>
    /// Used to deselect enemy unit after attacking them.
    /// </summary>
    public void DeselectUnit()
    {
        if (playerComponent.SelectedOther != null)
            playerComponent.SelectedOther.GetComponent<Renderer>().material.color = Color.white;

        playerComponent.SelectedOther = null;
    }
    /// <summary>
    /// Used to deselect all selected units since the only call to the method if after checking if the endturnbutton is pressed 
    /// Which would mean the team1 becomes team2 and team2 becomes team1. 
    /// </summary>
    public void DeselectUnitAll()
    {
        if (playerComponent.SelectedOther != null)
        {
            playerComponent.SelectedOther.GetComponent<Renderer>().material.color = Color.white;
            playerComponent.SelectedOther = null;
        }
        if (playerComponent.SelectedUnit != null)
        {
            playerComponent.SelectedUnit.GetComponent<Renderer>().material.color = Color.white;
            playerComponent.SelectedUnit = null;
        }
    }
    /// <summary>
    /// Makes sure that only one player unit and one enemy unit can be selected and only when the attack button is pressed. 
    /// See AttackButtonAction method in Player class.
    /// </summary>
    private void AttackMode()
    {
        if (hit.collider.gameObject != playerComponent.SelectedOther && playerComponent.OtherTeam.ToString() == hit.collider.gameObject.tag)
        {
            if (playerComponent.SelectedOther != null)
            {
                playerComponent.SelectedOther.GetComponent<Renderer>().material.color = Color.white;
            }

            playerComponent.SelectedOther = hit.collider.gameObject;

            //Deselects targeted unit if that unit if futher away than the selected unit's attack range
            if (Vector3.Distance(playerComponent.SelectedUnit.transform.position, playerComponent.SelectedOther.transform.position) <= playerComponent.SelectedUnit.GetComponent<UnitProperties>().AttackRange)
            {
                hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.red;
                playerComponent.ConfirmButton.SetActive(true);
            }
            else
            {
                DeselectUnit();
            }

        }
    }
    /// <summary>
    /// Makes sure that no other unit can be selected when pressed on Move button. 
    /// See Move>ButtonAction in Player class.
    /// </summary>
    private void MoveMode()
    {
        UnitProperties unitProp = playerComponent.SelectedUnit.GetComponent<UnitProperties>();
        Vector3 positionTouched = hit.point; //Saving the position to move towards.

        float moveDistance = Vector3.Distance(playerComponent.SelectedUnit.transform.position, positionTouched);

        //Is the movedistance less than the number of actionpoints for the selected unit 
        //And is the movementcost less than the actionpoints for the selected unit
        if (moveDistance < unitProp.ActionPoints && unitProp.MovementCost < unitProp.ActionPoints)
        {
            playerComponent.MoveDestination = positionTouched;
        }
    }
}

