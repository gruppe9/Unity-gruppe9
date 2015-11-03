using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class UnitSelector : MonoBehaviour
{
    Player playerComponent;

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

            // brug playerComponent.SelectedUnit til at gemme referencen til den valgte unit som også har samme team som
            // playerComponent.CurrentTeam.

            // brug playerComponent.SelectedOther til at gemme referencen til den valgte unit som ikke har samme team som
            // playerComponent.CurrentTeam.


        }
    }
}
