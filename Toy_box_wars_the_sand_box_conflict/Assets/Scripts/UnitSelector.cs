using UnityEngine;
using System.Collections;

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
            // raycasting stuff
            touchPosition = Camera.main.ScreenPointToRay(Input.mousePosition);

            switch (playerComponent.PlayerMode)
            {
                case PlayerAction.normal:
                    if (Physics.Raycast(touchPosition, out hit))
                    {
                        SelectUnit();
                    }
                    break;
                case PlayerAction.attack:
                    if (Physics.Raycast(touchPosition, out hit))
                    {
                        AttackMode();
                        playerComponent.ConfirmButton.SetActive(true);
                    }
                    break;
                case PlayerAction.move:
                    if (Physics.Raycast(touchPosition, out hit))
                    {
                        MoveMode();
                    }
                    break;
                default:
                    break;
            }
        }
    }

    private void SelectUnit()
    {
        //Selects the unit as the players selected unit, if the given unit isn't already selected.
        if (hit.collider.gameObject != playerComponent.SelectedUnit)
        {
            if (playerComponent.SelectedUnit != null)
                playerComponent.SelectedUnit.GetComponent<Renderer>().material.color = Color.white;

            playerComponent.SelectedUnit = hit.collider.gameObject;
            if (playerComponent.CurrentTeam.ToString() == hit.collider.gameObject.tag)
            {
                hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.green;
                playerComponent.AttackButton.SetActive(true);
                playerComponent.MoveButton.SetActive(true);
            }
            else
            {
                hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.red;
                playerComponent.AttackButton.SetActive(false);
                playerComponent.MoveButton.SetActive(false);
            }
        }
    }

    public void DeselectUnit()
    {
        if (playerComponent.SelectedOther != null)
            playerComponent.SelectedOther.GetComponent<Renderer>().material.color = Color.white;

        playerComponent.SelectedOther = null;
    }
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
        if (hit.collider.gameObject != playerComponent.SelectedOther && playerComponent.CurrentTeam.ToString() != hit.collider.gameObject.tag)
        {
            if (playerComponent.SelectedOther != null)
            {
                playerComponent.SelectedOther.GetComponent<Renderer>().material.color = Color.white;
            }
            playerComponent.SelectedOther = hit.collider.gameObject;
            hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.red;
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
            unitProp.ActionPoints -= unitProp.MovementCost;
        }
    }
}

