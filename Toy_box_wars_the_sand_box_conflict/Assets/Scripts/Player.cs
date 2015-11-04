using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{

    private GameObject selectedUnit;
    private GameObject selectedOther;
    private Teams currentTeam;
    private ButtonAction btnAction;
    private int team1Army;
    private int team2Army;
    public Vector3 tempDestination; // temporary variable for movement testing. Should be removed later.


    #region button refs
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
    internal Teams CurrentTeam
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

    #endregion


    // Use this for initialization
    void Start()
    {
        btnAction = ButtonAction.none;
        cancelButton.SetActive(false);
        confirmButton.SetActive(false);
        currentTeam = Teams.team1;
        team1Army = 0;
        team2Army = 1;
    }

    // Update is called once per frame
    void Update()
    {

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
                break;
            case Teams.team2:
                foreach (GameObject item in ArmySaves.Armies[ArmySaves.team[team2Army]])
                {
                    // reset ap til initialAP
                    item.GetComponent<UnitProperties>().ActionPoints = item.GetComponent<UnitProperties>().InitialAP;
                }
                currentTeam = Teams.team2;
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// needed Movement code stuff
    /// </summary>
    private void Movement()
    {

    }

    /// <summary>
    /// needed Attack code stuff
    /// </summary>
    private void Attack()
    {
        UnitProperties sProp = selectedUnit.GetComponent<UnitProperties>();
        UnitProperties osProp = selectedOther.GetComponent<UnitProperties>();
        if (selectedUnit != null && Vector3.Distance(selectedUnit.transform.position, selectedOther.transform.position) < sProp.AttackRange)
        {
            // attack stuff when in attack range
            osProp.Health -= sProp.Damage;
            sProp.ActionPoints -= sProp.AttackCost;
        }
    }

    #region Buttons
    public void MoveButton()
    {
        btnAction = ButtonAction.move;
        cancelButton.SetActive(true);
        confirmButton.SetActive(true);
    }

    public void AttackButton()
    {
        btnAction = ButtonAction.attack;
        cancelButton.SetActive(true);
        confirmButton.SetActive(true);
    }

    public void CancelButton()
    {
        btnAction = ButtonAction.none;
        cancelButton.SetActive(false);
        confirmButton.SetActive(false);
    }

    public void ConfirmButton()
    {
        switch (btnAction)
        {
            case ButtonAction.move:
                Movement();
                break;
            case ButtonAction.attack:
                Attack();
                break;
            default:
                break;
        }

        btnAction = ButtonAction.none;
        cancelButton.SetActive(false);
        confirmButton.SetActive(false);
    }

    public void EndTurnButton()
    {
        btnAction = ButtonAction.none;
        cancelButton.SetActive(false);
        confirmButton.SetActive(false);
        TurnController();
    }
    #endregion

}
