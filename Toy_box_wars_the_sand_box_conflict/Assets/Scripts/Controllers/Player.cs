using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{

    private GameObject selectedUnit;
    private GameObject selectedOther;
    private Teams currentTeam;
    private Teams otherTeam;
    private ButtonAction btnAction;
    private int team1Army;
    private int team2Army;
    private Vector3 moveDestination;
    private PlayerAction playerMode;
    private bool performingAction;

    private RaycastHit hit;


    #region Button refs
    [SerializeField]
    private GameObject moveButton;
    [SerializeField]
    private GameObject attackButton;
    [SerializeField]
    private GameObject endTurnButton;
    [SerializeField]
    private GameObject confirmButton;
    [SerializeField]
    private GameObject cancelButton;
    #endregion

    #region Properties

    /// <summary>
    /// Get/Set property for selected unit
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
    /// Get/Set property for another selected unit
    /// </summary>
    public GameObject SelectedOther
    {
        get
        {
            return selectedOther;
        }

        set
        {
            selectedOther = value;
        }
    }

    /// <summary>
    /// Get/Set property for which team has the current team
    /// </summary>
    public Teams CurrentTeam
    {
        get
        {
            return currentTeam;
        }

        set
        {
            currentTeam = value;
        }
    }

    public int Team1Army
    {
        get
        {
            return team1Army;
        }

        set
        {
            team1Army = value;
        }
    }

    public int Team2Army
    {
        get
        {
            return team2Army;
        }

        set
        {
            team2Army = value;
        }
    }

    public PlayerAction PlayerMode
    {
        get
        {
            return playerMode;
        }
    }

    public Teams OtherTeam
    {
        get
        {
            return otherTeam;
        }

        set
        {
            otherTeam = value;
        }
    }

    public GameObject MoveButton
    {
        get
        {
            return moveButton;
        }

        set
        {
            moveButton = value;
        }
    }

    public GameObject AttackButton
    {
        get
        {
            return attackButton;
        }

        set
        {
            attackButton = value;
        }
    }

    public GameObject ConfirmButton
    {
        get
        {
            return confirmButton;
        }

        set
        {
            confirmButton = value;
        }
    }

    public Vector3 MoveDestination
    {
        get
        {
            return moveDestination;
        }

        set
        {
            moveDestination = value;
        }
    }

    public bool PerformingAction
    {
        get
        {
            return performingAction;
        }

        set
        {
            performingAction = value;
        }
    }

    #endregion

    // Use this for initialization
    void Start()
    {
        btnAction = ButtonAction.none;
        cancelButton.SetActive(false);
        ConfirmButton.SetActive(false);
        currentTeam = Teams.team1;
        otherTeam = Teams.team2;
        team1Army = 0;
        team2Army = 1;
        playerMode = PlayerAction.normal;
        attackButton.SetActive(false);
        moveButton.SetActive(false);
        hit = new RaycastHit();
    }

    /// <summary>
    /// baiscally ends the current team's turn.
    /// </summary>
    private void TurnController()
    {
        switch (currentTeam)
        {
            case Teams.team1:
                foreach (GameObject item in ArmySaves.Armies[ArmySaves.team[team1Army]])
                {
                    // reset ap til initialAP
                    item.GetComponent<UnitProperties>().ActionPoints = item.GetComponent<UnitProperties>().InitialAP;
                }
                currentTeam = Teams.team2;
                otherTeam = Teams.team1;
                break;
            case Teams.team2:
                foreach (GameObject item in ArmySaves.Armies[ArmySaves.team[team2Army]])
                {
                    // reset ap til initialAP
                    item.GetComponent<UnitProperties>().ActionPoints = item.GetComponent<UnitProperties>().InitialAP;
                }
                currentTeam = Teams.team1;
                otherTeam = Teams.team2;
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// needed Movement code stuff
    /// </summary>
    private void MovementHandler()
    {
        PerformingAction = true;
        UnitProperties unitProp = selectedUnit.GetComponent<UnitProperties>();
        unitProp.currentPath = MapStuff.Instance.GeneratePath(moveDestination, unitProp);
        moveDestination = Vector3.zero;
        // we need to remove action points depending on the distance traveled
        // this code used to work. However, ripped from UnitSelector, moveDistance doesn't exist.
        //unitProp.ActionPoints -= (int)moveDistance * unitProp.MovementCost;
    }

    /// <summary>
    /// needed Attack code stuff
    /// </summary>
    private void AttackHandler()
    {
        PerformingAction = true;
        UnitProperties sProp = selectedUnit.GetComponent<UnitProperties>();
        UnitProperties osProp = selectedOther.GetComponent<UnitProperties>();
        //The distance between selectedunit and selectedother
        float targetDistance = Vector3.Distance(selectedUnit.transform.position, selectedOther.transform.position);

        //When player has selected a friendly unit and it's range is less than the distance between the target and the unit. 
        //And the unit's actionpoints is greater than the cost of attacking - then an attack is possible.
        if (selectedUnit != null && targetDistance < sProp.AttackRange && sProp.ActionPoints >= sProp.AttackCost)
        {
            //Makes the selected unit turn to look at the target before attacking. Instant execution, no rotation time atm.
            Vector3 direction = selectedOther.transform.position - selectedUnit.transform.position;
            selectedUnit.transform.rotation = Quaternion.LookRotation(direction);

            if (Physics.Raycast(sProp.transform.position, Vector3.forward, out hit))
            {
                Debug.Log(hit.collider.gameObject.ToString());
                Debug.DrawRay(sProp.transform.position, Vector3.forward, Color.red);

                if (hit.collider.tag == osProp.tag)
                {
                    //Selectedunit attackting target/selectedOther
                    sProp.Attack(osProp);
                }
                else
                {
                    Debug.Log("Hit something else");
                }
            }
        }


        //If the enemy unit has 0 or less health the unit is destroyed from the game. 
        if (osProp.Health <= 0)
        {
            Destroy(selectedOther);
        }

    }

    #region Buttons
    /// <summary>
    /// What happens when the MoveButton is pressed
    /// </summary>
    public void MoveButtonAction()
    {
        btnAction = ButtonAction.move;
        playerMode = PlayerAction.move;
        cancelButton.SetActive(true);
        attackButton.SetActive(false);
    }

    /// <summary>
    /// What happens when the AttackButton is pressed
    /// </summary>
    public void AttackButtonAction()
    {
        btnAction = ButtonAction.attack;
        moveButton.SetActive(false);
        cancelButton.SetActive(true);
        playerMode = PlayerAction.attack;
    }

    /// <summary>
    /// What happens when the CancelButton is pressed
    /// </summary>
    public void CancelButtonAction()
    {
        btnAction = ButtonAction.none;
        playerMode = PlayerAction.normal;
        cancelButton.SetActive(false);
        ConfirmButton.SetActive(false);
        attackButton.SetActive(true);
        moveButton.SetActive(true);
        MoveDestination = Vector3.zero;
    }

    /// <summary>
    /// What happens when the ConfirmButton is pressed
    /// </summary>
    public void ConfirmButtonAction()
    {
        switch (btnAction)
        {
            case ButtonAction.move:
                MovementHandler();
                break;
            case ButtonAction.attack:
                AttackHandler();
                break;
            default:
                break;
        }

        btnAction = ButtonAction.none;
        cancelButton.SetActive(false);
        ConfirmButton.SetActive(false);
        playerMode = PlayerAction.normal;
        attackButton.SetActive(true);
        moveButton.SetActive(true);
    }

    /// <summary>
    /// What happens when the EndTurnButton is pressed
    /// </summary>
    public void EndTurnButtonAction()
    {
        btnAction = ButtonAction.none;
        playerMode = PlayerAction.normal;
        cancelButton.SetActive(false);
        ConfirmButton.SetActive(false);
        moveButton.SetActive(false);
        attackButton.SetActive(false);
        GetComponent<UnitSelector>().DeselectUnitAll();
        TurnController();
    }
    #endregion

}
