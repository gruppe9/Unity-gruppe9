using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{

    private GameObject selectedUnit;
    private GameObject selectedOther;
    private Teams currentTeam;
    private ButtonAction btnAction;
    

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

    #endregion
    

    // Use this for initialization
    void Start()
    {
        btnAction = ButtonAction.none;
        cancelButton.SetActive(false);
        confirmButton.SetActive(false);
        
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
                foreach (GameObject item in ArmySaves.Armies[ArmySaves.team[0]])
                {
                    // reset ap til initialAP

                    currentTeam = Teams.team2;
                }
                break;
            case Teams.team2:
                foreach (GameObject item in ArmySaves.Armies[ArmySaves.team[0]])
                {
                    // reset ap til initialAP

                    currentTeam = Teams.team2;
                }
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
        if (selectedUnit != null && Vector3.Distance(selectedUnit.transform.position, selectedOther.transform.position) < selectedUnit.GetComponent<UnitProperties>().AttackRange);
        {
            // attack stuff when in attack range

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
