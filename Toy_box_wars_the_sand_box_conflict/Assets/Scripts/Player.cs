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
    private PlayerAction playerMode;
    private AudioSource sound;


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
    }

    public AudioSource Sound
    {
        get
        {
            return sound;
        }

        set
        {
            sound = value;
        }
    }


    #endregion


    // Use this for initialization
    void Start()
    {
        sound = GetComponent<AudioSource>();
        btnAction = ButtonAction.none;
        cancelButton.SetActive(false);
        confirmButton.SetActive(false);
        currentTeam = Teams.team1;
        team1Army = 0;
        team2Army = 1;
        playerMode = PlayerAction.normal;
        attackButton.SetActive(false);
        moveButton.SetActive(false);
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
                currentTeam = Teams.team1;
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
        float targetDistance = Vector3.Distance(selectedUnit.transform.position, selectedOther.transform.position);
        if (selectedUnit != null && targetDistance < sProp.AttackRange && sProp.ActionPoints >= sProp.AttackCost)
        {
            // attack stuff when in attack range
            //sound.Play();
            //Debug.Log("Sound just played!");

            osProp.Health -= sProp.Damage;
            sProp.ActionPoints -= sProp.AttackCost;

            if (osProp.Health <= 0)
            {
                Destroy(selectedOther);
            }
        }
    }

    #region Buttons
    public void MoveButtonAction()
    {
        btnAction = ButtonAction.move;
        playerMode = PlayerAction.move;
        cancelButton.SetActive(true);
        confirmButton.SetActive(true);
        attackButton.SetActive(false);
    }

    public void AttackButtonAction()
    {
        btnAction = ButtonAction.attack;
        moveButton.SetActive(false);
        cancelButton.SetActive(true);
        playerMode = PlayerAction.attack;
    }

    public void CancelButtonAction()
    {
        btnAction = ButtonAction.none;
        playerMode = PlayerAction.normal;
        cancelButton.SetActive(false);
        confirmButton.SetActive(false);
        attackButton.SetActive(true);
        moveButton.SetActive(true);
    }

    public void ConfirmButtonAction()
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
        playerMode = PlayerAction.normal;
        attackButton.SetActive(true);
        moveButton.SetActive(true);
    }

    public void EndTurnButtonAction()
    {
        btnAction = ButtonAction.none;
        playerMode = PlayerAction.normal;
        cancelButton.SetActive(false);
        confirmButton.SetActive(false);
        moveButton.SetActive(false);
        attackButton.SetActive(false);
        TurnController();
        GetComponent<UnitSelector>().DeselectUnitAll();
    }
    #endregion

}
